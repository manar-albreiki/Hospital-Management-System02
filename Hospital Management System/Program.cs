using Hospital_Management_System.Models;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Hospital_Management_System
{
    internal class Program
    {
        public static void PatientRegistration(HospitalContext context)
        {
            Console.WriteLine("Enter Patient Name :");
            string patientName = Console.ReadLine();

            Console.WriteLine("Enter Patient age :");
            int patientAge = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Patient gender :");
            string patientGender = Console.ReadLine();

            Console.WriteLine("Enter Patient phone :");
            string patientPhone = Console.ReadLine();

            Console.WriteLine("Enter Patient email :");
            string patientEmail = Console.ReadLine();

            Console.WriteLine("Enter Patient Blood Type :");
            string patientBloodType = Console.ReadLine();

            int patientId = (context.Patients.Count) + 1;
            //add patient 
            context.Patients.Add(new Patient
            {
                patientName = patientName,
                patientAge = patientAge,
                patientGender = patientGender,
                patientPhone = patientPhone,
                patientEmail = patientEmail,
                patientBloodType = patientBloodType,
                patientId = patientId

            });
            Console.WriteLine("Patient Added Successfully with ID " + patientId);
        }

        public static void AddDoctor(HospitalContext context)
        {
            Console.WriteLine("Enter doctor Name :");
            string doctorName = Console.ReadLine();

            Console.WriteLine("Enter doctor Specialization :");
            string doctorSpecialization = Console.ReadLine();

            Console.WriteLine("Enter doctor Phone :");
            string doctorPhone = Console.ReadLine();

            Console.WriteLine("Enter doctor Email :");
            string doctorEmail = Console.ReadLine();


            Console.WriteLine("Enter consultation Fee :");
            decimal consultationFee = decimal.Parse(Console.ReadLine());

            int doctorId = (context.Doctors.Count) + 1;

            context.Doctors.Add(new Doctor
            {
                doctorName = doctorName,
                doctorSpecialization = doctorSpecialization,
                doctorPhone = doctorPhone,
                doctorEmail = doctorEmail,
                consultationFee = consultationFee,
                doctorId = doctorId

            });
        }

        public static void ViewPatients(HospitalContext context)
        {
            if (context.Patients != null)
            {
                foreach (var p in context.Patients)
                {
                    Console.WriteLine("Patient details:");
                    Console.WriteLine("patient Id:" + p.patientId);
                    Console.WriteLine("patient Name:" + p.patientName);
                    Console.WriteLine("patient Phone:" + p.patientPhone);
                    Console.WriteLine("patient Blood Type:" + p.patientBloodType);

                }
            } else
            {
                Console.WriteLine("No Patient have been registered yet!");
            }
        }

        public static void ViewDoctors(HospitalContext context)
        {

            Console.WriteLine("Enter doctors Specialization");
            string enterdSpecialization = Console.ReadLine();


            foreach (var c in context.Doctors)
            {
                if (enterdSpecialization == c.doctorSpecialization)
                {
                    Console.WriteLine("doctor Id: " + c.doctorId);
                    Console.WriteLine("doctor Name: " + c.doctorName);
                    Console.WriteLine("doctor Specialization: " + c.doctorSpecialization);
                    Console.WriteLine("doctor Phone: " + c.doctorPhone);
                    Console.WriteLine("doctor Email: " + c.doctorEmail);
                    Console.WriteLine("consultationFee: " + c.consultationFee);
                } else
                {
                    Console.WriteLine("No available doctors");
                }
            }
        }

        public static void AddSlot(HospitalContext context)
        {
            Console.WriteLine("Enter doctor Id:");
            int doctorId = int.Parse(Console.ReadLine());

            //Console.WriteLine("Enter slot Id:");
            //int slotId = int.Parse(Console.ReadLine());

            int slotId = (context.AvailableSlots.Count) + 1;

            Console.WriteLine("Enter slot Date:");
            string slotDate = Console.ReadLine();

            Console.WriteLine("Enter slot Time:");
            string slotTime = Console.ReadLine();

            context.AvailableSlots.Add(new AvailableSlot
            {
                slotId = slotId,
                doctorId = doctorId,
                slotDate = slotDate,
                slotTime = slotTime,
                isBooked = false
            });
            Console.WriteLine($"The Slot with id = {slotId} has been added to doctor with id = {doctorId}");
        }

        public static void BookAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter patient Id");
            int patientId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter doctor Id");
            int enteredDoctorId = int.Parse(Console.ReadLine());



            foreach (var a in context.AvailableSlots)
            {

                if (a.isBooked == false && enteredDoctorId == a.doctorId)
                {


                    Console.WriteLine("slotId: " + a.slotId);
                    Console.WriteLine("doctorId: " + a.doctorId);
                    Console.WriteLine("slotDat:  " + a.slotDate + " slotTim: " + a.slotTime);

                }

                else
                {
                    Console.WriteLine(" No available slots for this  doctor id =" + a.doctorId);
                    return;
                }

            }

            Console.WriteLine("Enter the selected slot id ");
            int enteredSlotId = int.Parse(Console.ReadLine());
            foreach (var s in context.AvailableSlots)
            {
                if (enteredSlotId != s.slotId)
                {
                    Console.WriteLine("not available slot");
                    return;
                } else
                {
                    s.isBooked = true;
                    Console.WriteLine(" available slot");
                }
            }


            int appointmentId = (context.Appointments.Count) + 1;

            context.Appointments.Add(new Appointment
            {
                appointmentId = appointmentId,
                patientId = patientId,
                doctorId = enteredDoctorId,
                status = "Booked",


            });

            //foreach (var s in context.AvailableSlots)
            //{
            //    if (s.doctorId == s.doctorId &&
            //        s.slotDate == appointment.appointmentDate &&
            //        s.slotTime == appointment.appointmentTime)
            //    {
            //        s.isBooked = false;
            //    }
            //}
            Console.WriteLine("Appointment booked successfully.");

        }


        public static void CancelAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter appointment Id that you want to cancel");
            int appointmentId = int.Parse(Console.ReadLine());

            bool cancelled = false;

            foreach(var x in context.Appointments)
            {
                 if (appointmentId != x.appointmentId)
                {
                    cancelled = false;
                    Console.WriteLine("Appointment not found");


                    if (x.status == "Cancelled")
                    {
                        cancelled = false;
                        Console.WriteLine("Appointment already cancelled");

                        return;
                    }
                } 
            } 
            
            
            
            
            if (!cancelled)
            {
                Console.WriteLine("Appointment cancelled successfully");
                
            }


        }





            static void Main(string[] args)
            {
                HospitalContext mainContext = new HospitalContext();
                mainContext.Appointments = new List<Appointment>();
                mainContext.Patients = new List<Patient>();
                mainContext.Doctors = new List<Doctor>();
                mainContext.AvailableSlots = new List<AvailableSlot>();
                mainContext.MedicalRecords = new List<MedicalRecord>();

                bool exit = false;
                while (exit == false)
                {
                    Console.WriteLine("======= Welcome to Hospital system =======");
                    Console.WriteLine("01 Patient Registration");
                    Console.WriteLine("02 Add a New Doctor");
                    Console.WriteLine("03 View All Patients");
                    Console.WriteLine("04 View All Doctors by Specialization");
                    Console.WriteLine("05 Add an Available Time Slot for a Doctor");
                    Console.WriteLine("06 Book an Appointment");
                    Console.WriteLine("07 Cancel an Appointment");
                    Console.WriteLine("08 Create a Medical Record After a Visit");
                    Console.WriteLine("09 Generate a Patient Medical History Report");
                    Console.WriteLine("10 Doctor Workload and Revenue Summary");
                    Console.WriteLine("10 Exit");



                    Console.WriteLine("Please enter an option :");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 01:
                            PatientRegistration(mainContext);
                            break;

                        case 02:
                            AddDoctor(mainContext);
                            break;

                        case 03:
                            ViewPatients(mainContext);
                            break;

                        case 04:
                            ViewDoctors(mainContext);
                            break;

                        case 05:
                            AddSlot(mainContext);
                            break;

                        case 06:
                            BookAppointment(mainContext);
                            break;

                        //case 07:
                        //    break;

                        //case 08:
                        //    break;

                        //case 09:
                        //    break;

                        //case 10:
                        //    break;

                        case 0:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;


                    }
                }
            }
        
    }
}
