using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Models.Entities
{
    internal class AnimalEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string AnimalType { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Cause { get; set; } = null!;
    }
}
