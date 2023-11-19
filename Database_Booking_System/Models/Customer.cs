using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public CustomerAddress? Address { get; set; } = null!;
        public Animal? Animal { get; set; } = null!;
        public Booking? Booking { get; set; } = null!;
    }
}
