using Database_Booking_System.Context;
using Database_Booking_System.Models.Entities;
using Database_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database_Booking_System.Services
{
    internal class BookingService
    {
        private readonly DataContext _context;

        public BookingService(DataContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task<BookingEntity> AddBooking(Booking booking)
        {
            try
            {
                var entity = new BookingEntity
                {

                    Time = booking.Time,
                    Date = booking.Date,
                    CustomerId = booking.Customer.Id,
                    VeterinaryId = booking.Veterinary.Id

                };
                _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
        public Booking ConvertBooking(BookingEntity booking)
        {
            try
            {
                var convert = new Booking
                {
                    Id = booking.Id,
                    Time = booking.Time,
                    Date = booking.Date,
                    Customer = new Customer
                    {
                        Id = booking.Customer.Id,
                        FirstName = booking.Customer.FirstName,
                        LastName = booking.Customer.LastName,
                        PhoneNumber = booking.Customer.PhoneNumber,
                        Email = booking.Customer.Email,
                    },
                    Veterinary = new Veterinary
                    {
                        Id = booking.Veterinary.Id,
                        FirstName = booking.Veterinary.FirstName,
                        LastName = booking.Veterinary.LastName,
                    }

                };
                return convert;
            }
            catch (Exception ex)
            {
                return null!;
            }
            
        }
        //READ
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingList = new List<Booking>();
            try
            {
                var bookings = await _context.Set<BookingEntity>()
                    .Include(booking => booking.Customer)
                    .ThenInclude(customer => customer.Address)
                        .Include(booking => booking.Customer.Animal)  // Include AnimalEntity for the Customer
                        .Include(booking => booking.Veterinary)
                        .ToListAsync();



                foreach (var booking in bookings)
                {
                    var bookingInfo = new Booking
                    {
                        Id = booking.Id,
                        Time = booking.Time,
                        Date = booking.Date,
                        Customer = new Customer
                        {
                            Id = booking.Customer.Id,
                            FirstName = booking.Customer.FirstName,
                            LastName = booking.Customer.LastName,
                            PhoneNumber = booking.Customer.PhoneNumber,
                            Email = booking.Customer.Email,
                            Animal = new Animal
                            {
                                Id = booking.Customer.Animal.Id,
                                AnimalType = booking.Customer.Animal.AnimalType,
                                Cause = booking.Customer.Animal.Cause,
                            },
                            Address = new CustomerAddress
                            {
                                Id = booking.Customer.Address.Id,
                                StreetName = booking.Customer.Address.StreetName,
                                PostalCode = booking.Customer.Address.PostalCode,
                                City = booking.Customer.Address.City,
                            }

                        },
                        Veterinary = new Veterinary
                        {
                            Id = booking.Veterinary.Id,
                            FirstName = booking.Veterinary.FirstName,
                            LastName = booking.Veterinary.LastName,
                        }
                    };
                    bookingList.Add(bookingInfo);
                }
                return bookingList;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
        //UPDATE
        public async Task<bool>UpdateAsync(int id, BookingEntity editBooking)
        {
            try
            {
                BookingEntity existingBookning = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
                if (existingBookning != null)
                {
                    existingBookning.Time = editBooking.Time;
                    existingBookning.Date = editBooking.Date;

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

        //DELETE
        public async Task DeleteAsync(int id, int customerId, int veterinaryId)
        {
            try
            {
                BookingEntity booking = await _context.Bookings.FirstAsync(x => x.Id == id);
                CustomerEntity customer = await _context.Customers.FirstAsync(y => y.Id == customerId);
                var animalId = customer.AnimalId;
                AnimalEntity animal = await _context.Animals.FirstAsync(x => x.Id == animalId); 
                VeterinaryEntity veterinary = await _context.Veterinaries.FirstAsync(x => x.Id == veterinaryId);
               if (veterinary != null)
                {
                    veterinary.BookingId = null;
                }

                _context.Customers.Remove(customer);
                _context.Animals.Remove(animal);
                _context.Bookings.Remove(booking);
                
                
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return;
            }
        }
        
    }
}
