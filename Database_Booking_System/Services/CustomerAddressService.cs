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
    internal class CustomerAddressService
    {
        private readonly DataContext _context;

        public CustomerAddressService(DataContext context)
        {
            _context = context;
        }
        public async Task<CustomerAddressEntity> AddCustomerAddress(CustomerAddress customerAddress)
        {
            try
            {
                var entity = new CustomerAddressEntity
                {
                    StreetName = customerAddress.StreetName,
                    City = customerAddress.City,
                    PostalCode = customerAddress.PostalCode,
                };
                
                _context.CustomerAddresses.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null!;
            } 
        }
        public CustomerAddress ConvertAddress(CustomerAddressEntity customerAddress)
        {
            try
            {

                var convert = new CustomerAddress
                {
                    Id = customerAddress.Id,
                    StreetName = customerAddress.StreetName,
                    City = customerAddress.City,
                    PostalCode = customerAddress.PostalCode,
                };

                return convert;
            }
            catch
            (Exception ex)
            {
                return null!;
            }

        }
    }
}
