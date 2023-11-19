using Database_Booking_System.Context;
using Database_Booking_System.Models.Entities;
using Database_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database_Booking_System.Services
{
    internal class VeterinaryService
    {
        private readonly DataContext _context;

        public VeterinaryService(DataContext context)
        {
            _context = context;
        }
        public async Task AddVeterinary(Veterinary veterinary)
        {
            try
            {
                var entity = new VeterinaryEntity
                {
                   
                    FirstName = veterinary.FirstName,
                    LastName = veterinary.LastName,

                };
                _context.Veterinaries.Add(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex) { }
        }
        public Veterinary ConvertVeterinary(VeterinaryEntity veterinary)
        {
            try
            {
                var convert = new Veterinary
                {
                    Id = veterinary.Id,
                    FirstName = veterinary.FirstName,
                    LastName = veterinary.LastName,
                    BookingId = new Booking
                    {
                        Id = veterinary.Booking.Id,
                        Date = veterinary.Booking.Date,
                        Time = veterinary.Booking.Time,
                    }
                };
                return convert;
            }
            catch (Exception ex)
            {
                return null!;
            }

        }
        public async Task<IEnumerable<Veterinary>> GetAllVetrinaryAsync()
        {
            var veterinaryList = new List<Veterinary>();
            try
            {
                var veterinaries = await _context.Set<VeterinaryEntity>()
                    .Where(veterinary => veterinary.BookingId == null)
                    .ToListAsync();

                foreach (var veterinary in veterinaries)
                {
                    var veterinaryInfo = new Veterinary
                    {
                        Id = veterinary.Id,
                        FirstName = veterinary.FirstName,
                        LastName = veterinary.LastName,
                        BookingId = null

                    };
                    veterinaryList.Add(veterinaryInfo);
                }

                return veterinaryList;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
        public async Task<bool> UpdateBookingId(int veterinaryId, int bookingId)
        {
            try
            {
                var veterniary = await _context.Veterinaries.FindAsync(veterinaryId);

                if (veterniary != null)
                {
                    veterniary.BookingId = bookingId;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }


}

        
    

