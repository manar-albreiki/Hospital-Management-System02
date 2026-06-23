using Hospital_Management_System.Models;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace Hospital_Management_System
{
    internal class Program
    {
        static void PrintPatients(List<Patient> patientsList)
        {
            foreach (Patient p in patientsList)
            {
                p.convertDataToStringPatient();
            }
        }


        static void PrintDoctors(List<Doctor> DoctorsList)
        {
            foreach (Doctor p in DoctorsList)
            {
                p.convertDataToStringDoctor();
            }
        }


        static void PrintSlots(List<AvailableSlot> AvailableSlotsList)
        {
            foreach (AvailableSlot p in AvailableSlotsList)
            {
                p.convertDataToStringAvailableSlot();
            }
        }

        static void PrintAppointment(List<Appointment> AppointmentsList)
        {
            foreach (Appointment p in AppointmentsList)
            {
                p.convertDataToStringAppointment();
            }
        }
        //01 Patient Registration
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
        
        //02 Add a New Doctor
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
        
        
        //03 View All Patients
        public static void ViewPatients(List<Patient> viewPatients)
        {
            //if (context.Patients != null)
            //{
            //    foreach (var p in context.Patients)
            //    {
            //        Console.WriteLine("Patient details:");
            //        Console.WriteLine("patient Id:" + p.patientId);
            //        Console.WriteLine("patient Name:" + p.patientName);
            //        Console.WriteLine("patient Phone:" + p.patientPhone);
            //        Console.WriteLine("patient Blood Type:" + p.patientBloodType);

            //    }
            //} else
            //{
            //    Console.WriteLine("No Patient have been registered yet!");
            //}

            //var ViewAllPatients = viewPatients.Where(x => x.patientId > 0).ToList();
            //if (ViewAllPatients.Count > 0)
            //{
            //    PrintPatients(ViewAllPatients);
            //}
            //else
            //{
            //    Console.WriteLine("no available patients");
            //}
            if (viewPatients.Count == 0)
            {
                Console.WriteLine("no available patients");
            }
            foreach (Patient p in viewPatients)
            {
                p.convertDataToStringPatient(); 
            }
        }
        
        
        //04 View All Doctors by Specialization
        public static void ViewDoctors(List<Doctor> ViewDoctors)
        {

            Console.WriteLine("Enter doctors Specialization");
            string enterdSpecialization = Console.ReadLine();


            //foreach (var c in context.Doctors)
            //{
            //    if (enterdSpecialization == c.doctorSpecialization)
            //    {
            //        Console.WriteLine("doctor Id: " + c.doctorId);
            //        Console.WriteLine("doctor Name: " + c.doctorName);
            //        Console.WriteLine("doctor Specialization: " + c.doctorSpecialization);
            //        Console.WriteLine("doctor Phone: " + c.doctorPhone);
            //        Console.WriteLine("doctor Email: " + c.doctorEmail);
            //        Console.WriteLine("consultationFee: " + c.consultationFee);
            //    } else
            //    {
            //        Console.WriteLine("No available doctors");
            //    }
            //}

            List<Doctor> Specialization = ViewDoctors.Where(x => x.doctorSpecialization==enterdSpecialization).ToList();


            if (Specialization.Count > 0)
            {
                PrintDoctors(Specialization);
            }

            if (Specialization.Count == 0)
            {
                Console.WriteLine("no available doctors in this Specialization");
                return;
            }
            //another way
            //List<Doctor> Specialization = ViewDoctors.Where(x => x.doctorSpecialization == enterdSpecialization).ToList();


            //if (Specialization.Count == 0)
            //{
            //    Console.WriteLine("no available doctors in this Specialization");
            //    return;
            //}


            //foreach (Doctor p in ViewDoctors)
            //{
            //    p.convertDataToStringDoctor();
            //}




        }

        
        //05 Add an Available Time Slot for a Doctor
        public static void AddSlot(HospitalContext context)
        {
            if (context.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors in the system yet. Please add a doctor first.");
                return;
            }

            //  print all doctors
            context.Doctors.ForEach(d =>
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  ({d.doctorSpecialization})")
            );


            Console.WriteLine("Enter doctor Id:");
            int doctorId = int.Parse(Console.ReadLine());

            bool result = context.Doctors.Any(d => d.doctorId == doctorId);

            if (result == false)
            {
                Console.WriteLine("no doctor found with id");
                return;
            }

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

        //06 Book an Appointment
        public static void BookAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter patient Id");
            int patientId = int.Parse(Console.ReadLine());
            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }
            //display all doctors
            ViewDoctors(context.Doctors);

            Console.WriteLine("Enter doctor Id");
            int doctorId = int.Parse(Console.ReadLine());
            Doctor doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            //bool foundAvailableSlot = false;

            //foreach (var s in context.AvailableSlots)
            //{
            //    if (s.isBooked==false&&s.doctorId== doctorId)
            //    {
            //         foundAvailableSlot = true;
            //        Console.WriteLine("Date: "+s.slotDate+"  Time :"+s.slotTime);
            //    }else
            //    {
            //        foundAvailableSlot = false;
            //        Console.WriteLine("no available slots for doctor ="+ s.doctorId);
            //        return;
            //    }
            //}

            List<AvailableSlot> availableSlot = context.AvailableSlots.Where(x => x.isBooked == false && x.doctorId == doctorId).ToList();
            if (availableSlot.Count > 0)
            {
                PrintSlots(availableSlot);
            }
            else
            {
                Console.WriteLine("no available slots");
                return;
            }
            //-------
            Console.WriteLine("Enter a slot id");
            int enteredSlotId = int.Parse(Console.ReadLine());

            //bool slotFound = false;

            //foreach (var s in context.AvailableSlots)
            //{
            //    if (s.slotId == enteredSlotId &&
            //        s.doctorId == doctorId &&
            //        s.isBooked == false)
            //    {
            //        slotFound = true;

            //        s.isBooked = true;

            //    } 

            //} if (slotFound == false)
            //{ 
            //    Console.WriteLine("Slot not found");

            //}

            AvailableSlot BookedSlots = context.AvailableSlots.First(x => x.slotId == enteredSlotId );
                                                  

            if (BookedSlots ==null)
            {
           
                Console.WriteLine(" slots not found");
                return;
            }

            //-----

            int appointmentId = (context.Appointments.Count) + 1;

            context.Appointments.Add(new Appointment
            {
                appointmentId= appointmentId,
                patientId= patientId,
                doctorId= doctorId,
                status="Booked"
            });
            BookedSlots.isBooked = true;

            Console.WriteLine("Appointment booked successfully.");
        }


        //07 Cancel an Appointment
        public static void CancelAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter appointment Id");
            int enteredAppointmentId = int.Parse(Console.ReadLine());

            //bool found = false;

            //foreach (Appointment a in CancelledAppointment)
            //{
            //    if (a.appointmentId == appointmentId)
            //    {
            //        found = true;

            //        if (a.status == "cancelled")
            //        {
            //            Console.WriteLine("Appointment already cancelled");
            //        }
            //        else
            //        {
            //            a.status = "cancelled";
            //            Console.WriteLine("Appointment cancelled successfully");
            //        }
            //    }
            //}

            //if (found == false)
            //{
            //    Console.WriteLine("Appointment not found");
            //}

            Appointment foundAppointment = context.Appointments.FirstOrDefault(x => x.appointmentId == enteredAppointmentId);
                                                                     
            if (foundAppointment==null)
            {
           
                Console.WriteLine("Appointment not found");
            }
            if (foundAppointment.status == "Cancelled")
            {
                Console.WriteLine("This appointment is already cancelled.");
                return;
            }

            if (foundAppointment.status == "Completed")
            {
                Console.WriteLine("Cannot cancel a completed appointment.");
                return;
            }

            //  find the matching slot 
            AvailableSlot slot = context.AvailableSlots.FirstOrDefault(s =>
                s.doctorId == foundAppointment.doctorId &&
                s.slotDate == foundAppointment.appointmentDate &&
                s.slotTime == foundAppointment.appointmentTime
            );

            if (slot != null)
                slot.isBooked = false;

            foundAppointment.status = "Cancelled";
            Console.WriteLine($"Appointment  has been cancelled and the time slot is now available again.");




        }

        //08 Create a Medical Record After a Visit
        public static void MedicalRecord(HospitalContext context)
        {
            int recordId = (context.MedicalRecords.Count) + 1;

            Console.WriteLine("Enter appointment Id");
            int appointmentId = int.Parse(Console.ReadLine());

            //  to find the appointment
            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("Cannot create a medical record for a cancelled appointment.");
                return;
            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("A medical record already exists for this appointment.");
                return;
            }

            //  the doctor's consultation fee
            decimal fee = context.Doctors
                .Where(d => d.doctorId == appointment.doctorId)
                .Select(d => d.consultationFee)
                .FirstOrDefault();

            Console.WriteLine("Enter diagnosis");
            string diagnosis = Console.ReadLine();

            Console.WriteLine("Enter prescription");
            string prescription = Console.ReadLine();

            Console.WriteLine("Enter visitDate");
            string visitDate = Console.ReadLine();

            Console.WriteLine("Enter appointment Id");
            decimal visitFee = decimal.Parse(Console.ReadLine());

            context.MedicalRecords.Add(new MedicalRecord
            {
                recordId= recordId,
                appointmentId= appointmentId,
                diagnosis= diagnosis,
                prescription= prescription,
                visitDate= visitDate,
                visitFee= visitFee
            });
            appointment.status = "Completed";

            Console.WriteLine("The medical record has been created successfully");

        }


        //09 Generate a Patient Medical History Report
        public static void MedicalHistoryReport(HospitalContext context)
        {
            Console.WriteLine("enter patientId");
            int patientId = int.Parse(Console.ReadLine());

            //  find the patient
            Patient Patients = context.Patients.FirstOrDefault(x => x.patientId == patientId);


            if (Patients == null)
            {
                Console.WriteLine("patient not found");
                return;
            }
            // get all records for this patient
            List<MedicalRecord> PatientRecords = context.MedicalRecords.Where(x => x.patientId == patientId).ToList();


            if (PatientRecords.Count == 0)
            {
                Console.WriteLine("No Medical Records for this patient");
                return;
            }

            foreach (MedicalRecord p in PatientRecords)
            {
                p.convertDataToStringMedicalRecord();
            }
            Console.WriteLine($"\n--- Medical History for {Patients.patientName} (ID: {patientId}) ---");
            foreach (var r in PatientRecords)
            {
                //to know doctor name
                string doctorName = context.Doctors.Where(d => d.doctorId == r.doctorId)
                                                   .Select(d => d.doctorName)
                                                   .FirstOrDefault();

                Console.WriteLine($"\n  Record ID   : {r.recordId}");
                Console.WriteLine($"  Visit Date  : {r.visitDate}");
                Console.WriteLine($"  Doctor      : {doctorName}");
                Console.WriteLine($"  Diagnosis   : {r.diagnosis}");
                Console.WriteLine($"  Prescription: {r.prescription}");
                Console.WriteLine($"  Fee Charged : {r.visitFee:C}");
                Console.WriteLine("-------------------------------------" );


                //  total all fees
                decimal totalCharged = PatientRecords.Sum(r => r.visitFee);
                Console.WriteLine($"\n  TOTAL AMOUNT CHARGED: {totalCharged:C}");

            }
        }
        
        
        //10 Doctor Workload and Revenue Summary
        public static void RevenueSummary(HospitalContext context)
        {
            Console.WriteLine("=== Doctor Workload & Revenue Summary ===");
            
            if (context.Appointments.Count == 0)
            {
                Console.WriteLine("no apppintments has been recorded");
            }

            var summary = context.Doctors
                .Select(d => new
                {
                    d.doctorId,
                    d.doctorName,
                    d.doctorSpecialization,
                    // Count() with predicate to count completed appointments
                    completed = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Completed"),
                    // Count() with predicate to count cancelled appointments
                    cancelled = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Cancelled"),
                    // Sum() to total revenue from medical records
                    totalRevenue = context.MedicalRecords
                        .Where(r => r.doctorId == d.doctorId)
                        .Sum(r => r.visitFee)
                })
                .OrderByDescending(x => x.totalRevenue)
                .ToList();

            Console.WriteLine("\n  Rank  | Doctor Name               | Specialization       | Completed | Cancelled | Total Revenue");
            Console.WriteLine("  " + new string('-', 95));

            for (int i = 0; i < summary.Count; i++)
            {
                var x = summary[i];
                Console.WriteLine($"  #{i + 1,-5} | {x.doctorName,-25} | {x.doctorSpecialization,-20} |" +
                                  $" {x.completed,-9} | {x.cancelled,-9} | {x.totalRevenue:C}");
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
                    Console.WriteLine("0 Exit");



                    Console.WriteLine("Please enter an option :");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 01: PatientRegistration(mainContext);break;

                        case 02:AddDoctor(mainContext);break;

                        case 03:ViewPatients(mainContext.Patients); break;

                        case 04:ViewDoctors(mainContext.Doctors);break;

                        case 05:AddSlot(mainContext);break;

                        case 06:BookAppointment(mainContext);break;

                        case 07:CancelAppointment(mainContext);break;

                        case 08:MedicalHistoryReport(mainContext);break;

                        case 09: MedicalHistoryReport(mainContext); break;

                    case 10:RevenueSummary(mainContext);break;

                    case 0:exit = true;break;

                        default:Console.WriteLine("Invalid option. Please try again.");break;


                    }
                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }



        }
        
    }
}
