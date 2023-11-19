using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models
{
    internal class CustomerAddress
    {
        public int Id { get; set; }

        public string? StreetName { get; set; } = null!;

        public string? PostalCode { get; set; } = null!;

        public string? City { get; set; } = null!;

    }
}
