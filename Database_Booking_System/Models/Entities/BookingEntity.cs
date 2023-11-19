using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models.Entities
{
    internal class BookingEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Date { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Time { get; set; } = null!;

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Booking")]
        public CustomerEntity Customer { get; set; } = null!;

        public int VeterinaryId { get; set; }

        [ForeignKey("VeterinaryId")]
        [InverseProperty("Booking")]
        public VeterinaryEntity Veterinary { get; set; } = null!;
    }
}
