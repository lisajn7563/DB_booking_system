using Database_Booking_System.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Context
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AnimalEntity> Animals { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<VeterinaryEntity> Veterinaries { get; set; }
    }
}
