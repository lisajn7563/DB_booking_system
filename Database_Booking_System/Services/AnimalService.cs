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
    internal class AnimalService
    {
        private readonly DataContext _context;

        public AnimalService(DataContext context)
        {
            _context = context;
        }
        public async Task<AnimalEntity> AddAnimal(Animal animal)
        {
            try
            {
                var entity = new AnimalEntity
                {
                    
                    AnimalType = animal.AnimalType,
                    Cause = animal.Cause
                };
                
                _context.Animals.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
        public Animal ConvertAnimal(AnimalEntity animal)
        {
            try
            {
                var convert = new Animal
                {
                    Id = animal.Id,
                    AnimalType = animal.AnimalType,
                    Cause = animal.Cause
                };
                return convert;
            }
            catch
            { 
                return null!;
            }

        }
    }
}
