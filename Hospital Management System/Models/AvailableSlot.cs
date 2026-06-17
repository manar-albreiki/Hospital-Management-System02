using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Models
{
    public class AvailableSlot
    {
        public int slotId { get; set; }
        public int doctorId { get; set; }
        public string slotDate { get; set; }
        public string slotTime { get; set; }
      
        public bool isBooked { get; set; } = false;
        public void convertDataToStringAvailableSlot()
        {
            Console.WriteLine($"{slotId} | {doctorId} | {doctorId} | {slotDate} | {slotTime} | {isBooked} ");
        }

    }
}
