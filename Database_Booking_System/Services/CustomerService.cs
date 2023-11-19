using Database_Booking_System.Context;
using Database_Booking_System.Models.Entities;
using Database_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Services
{
    internal class CustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }
        public async Task<CustomerEntity> AddCustomer(Customer customer)
        {
            try
            {
                var entity = new CustomerEntity
                {
                    
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    AddressId = customer.Address.Id,
                    AnimalId = customer.Animal.Id,

                };

                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
        public Customer ConvertCustomer(CustomerEntity customer)
        {
            try
            {
                var convert = new Customer
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Animal = new Animal
                    {
                        Id = customer.Animal.Id,
                        AnimalType = customer.Animal.AnimalType,
                        Cause = customer.Animal.Cause
                    },
                    Address = new CustomerAddress
                    {
                        Id = customer.Address.Id,
                        StreetName = customer.Address.StreetName,
                        PostalCode = customer.Address.PostalCode,
                        City = customer.Address.City
                    },

                };
                return convert;
            }
            catch (Exception ex)
            {
                return null!;
            }

        }
        public async Task<bool> UpdateBookingId(int customerId, int bookingId)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);

                if (customer != null)
                {
                    customer.BookingId = bookingId;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle the exception (log, throw, etc.)
                return false;
            }
        }
        
    }
}
