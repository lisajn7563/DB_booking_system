using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models
{
    internal class Booking
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }

        public Customer? Customer { get; set; }
        public Veterinary? Veterinary { get; set; }
    }
}
