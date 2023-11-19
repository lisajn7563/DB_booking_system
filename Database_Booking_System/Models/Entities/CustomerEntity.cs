using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models.Entities
{
    internal class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = null!;

        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public CustomerAddressEntity Address { get; set; } = null!;

        public int AnimalId { get; set; }

        [ForeignKey("AnimalId")]
        public AnimalEntity Animal { get; set; } = null!;

        public int BookingId { get; set; }

        [InverseProperty("Customer")]
        public BookingEntity Booking { get; set; } = null!;
    }
}
