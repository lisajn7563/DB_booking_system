using Database_Booking_System.Models;
using Database_Booking_System.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Booking_System.Services
{
    internal class MenuService
    {
        private readonly CustomerService _customerService;
        private readonly AnimalService _animalService;
        private readonly CustomerAddressService _customerAddressService;
        private readonly BookingService _bookingService;
        private readonly VeterinaryService _veterinaryService;

        public MenuService(CustomerService customerService, AnimalService animalService, CustomerAddressService customerAddressService, BookingService bookingService, VeterinaryService veterinaryService)
        {
            _customerService = customerService;
            _animalService = animalService;
            _customerAddressService = customerAddressService;
            _bookingService = bookingService;
            _veterinaryService = veterinaryService;
        }
        public async Task MainMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("boknings system");
                Console.WriteLine();
                Console.WriteLine("1. Skapa bokning");
                Console.WriteLine();
                Console.WriteLine("2. Visa bokningar");
                Console.WriteLine();
                Console.WriteLine("3. Avsluta");
                Console.WriteLine();

                int option;
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
                {
                    Console.Write("Något fel uppstod, välj från 1-3");
                }

                switch (option)
                {
                    case 1:
                        await Create();
                        break;
                    case 2:
                        await Read();
                        break;
                    case 3:
                        Environment.Exit(3);
                        break;
                    default:
                        break;


                }

            }
            catch (Exception ex) { }
        }
        public async Task<IEnumerable<Veterinary>> GetVeterinariesAsync()
        {
            return await _veterinaryService.GetAllVetrinaryAsync();
        }
        public async Task Create()
        {
            //var veterinary = new Veterinary
            //{
            //    FirstName = "Kalle",
            //    LastName = "Johansson",

            //};
            //await _veterinaryService.AddVeterinary(veterinary);

            //var vet = new Veterinary
            //{
            //    FirstName = "Sandra",
            //    LastName = "´Svensson",

            //};
            //await _veterinaryService.AddVeterinary(vet);

            //var vete = new Veterinary
            //{
            //    FirstName = "Stina",
            //    LastName = "Ek",

            //};
            //await _veterinaryService.AddVeterinary(vete);
            //var veteri = new Veterinary
            //{
            //    FirstName = "Johanna",
            //    LastName = "Nordgren",

            //};
            //await _veterinaryService.AddVeterinary(veteri);

            //await _veterinaryService.AddVeterinary(vete);
            //var veterin = new Veterinary
            //{
            //    FirstName = "Johan",
            //    LastName = "Fors",

            //};
            //await _veterinaryService.AddVeterinary(veterin);

            var customer = new Customer();

            Console.Clear();

            Console.WriteLine("Skapa bokning");
            Console.WriteLine();

            Console.WriteLine("Förnamn: ");
            customer.FirstName = Console.ReadLine();

            Console.WriteLine("Efternamn: ");
            customer.LastName = Console.ReadLine();

            Console.WriteLine("Telefonummer: ");
            customer.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Email: ");
            customer.Email = Console.ReadLine();

            var address = new CustomerAddress();

            Console.Clear();
            Console.WriteLine("Lägg till adress");
            Console.WriteLine();

            Console.WriteLine("Adress: ");
            address.StreetName = Console.ReadLine();

            Console.WriteLine("Postnummer: ");
            address.PostalCode = Console.ReadLine();

            Console.WriteLine("Stad: ");
            address.City = Console.ReadLine();

            var addressObject = await _customerAddressService.AddCustomerAddress(address);
            var x = _customerAddressService.ConvertAddress(addressObject);
            customer.Address = x;


            var animal = new Animal();

            Console.Clear();
            Console.WriteLine("Lägg till djuret");
            Console.WriteLine();

            Console.WriteLine("Djurtyp: ");
            animal.AnimalType = Console.ReadLine();

            Console.WriteLine("Orsak till besöket: ");
            animal.Cause = Console.ReadLine();

            var animalObject = await _animalService.AddAnimal(animal);
            var y = _animalService.ConvertAnimal(animalObject);
            customer.Animal = y;

            Console.Clear();
            Console.WriteLine("Dessa Vetrinärer är tillgängliga");
            Console.WriteLine();

            var veterinaryList = await GetVeterinariesAsync();
            var indexList = veterinaryList.ToList();

            var chosenVeterinary = new Veterinary();

            if (veterinaryList !=null)
            {
                if(veterinaryList.Count() > 0)
                {
                    Console.WriteLine("Välj Vetrinär");
                    for (int i = 0; i < veterinaryList.Count(); i++)
                    {
                        Console.WriteLine(" (" + i + ") " + indexList[i].FirstName + " " + indexList[i].LastName);
                    }

                    var indexVetrinary = Console.ReadLine();
                    chosenVeterinary = veterinaryList.ElementAt(int.Parse(indexVetrinary));
                }

            }

            var customerObject = await _customerService.AddCustomer(customer);
            var chosenCustomer = _customerService.ConvertCustomer(customerObject);
            

            var booking = new Booking();

            Console.WriteLine();
            Console.WriteLine("Kund är nu skapad");
            Console.WriteLine();
            Console.WriteLine("dataum för besöket: ");
            booking.Date = Console.ReadLine();
            Console.WriteLine("Tid för besöket: ");
            booking.Time = Console.ReadLine();

            booking.Veterinary = chosenVeterinary;
            booking.Customer = chosenCustomer;
            booking.Veterinary = chosenVeterinary;

            
            
            var bookingObject = await _bookingService.AddBooking(booking);

            var d = _bookingService.ConvertBooking(bookingObject);
            customer.Booking = d;
            await _customerService.UpdateBookingId(chosenCustomer.Id, d.Id);

            var c = _bookingService.ConvertBooking(bookingObject);
            chosenVeterinary.BookingId = c;
            await _veterinaryService.UpdateBookingId(chosenVeterinary.Id, c.Id);
            
            // ta bort allt i databasen kolumnsen(utan veterinary) och skapa nya kunder.
            // städa upp och snygga till i koden
            
            Console.Clear();

            await MainMenu();

        }
        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _bookingService.GetAllBookingsAsync();
        }

        public async Task Read()
        {

            Console.Clear();

            Console.WriteLine("Alla bokningar");
            Console.WriteLine();

            var bookingList = await GetBookingsAsync();

            if (bookingList.Any())
            {
                foreach(var booking in bookingList)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Tidpunkt: {booking.Date} {booking.Time}");
                    Console.WriteLine($"Vetrinär: {booking.Veterinary.FirstName} {booking.Veterinary.LastName}");
                    Console.WriteLine($"Kund: {booking.Customer.FirstName} {booking.Customer.LastName}");
                    Console.WriteLine($"Djur: {booking.Customer.Animal.AnimalType}");
                    Console.WriteLine($"Orsak till besökare: {booking.Customer.Animal.Cause}");

                }

            }
            else
            {
                Console.WriteLine("Det finns inga bokningar");
            }
            try
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("SS Välj mellan 1 - 3 ");
                Console.WriteLine();
                Console.WriteLine("1. Ta bort bokning");
                Console.WriteLine();
                Console.WriteLine("2. Redigera bokning");
                Console.WriteLine();
                Console.WriteLine("3. Tillbaka till menyn");
                Console.WriteLine();


                int option;
                while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3)
                {
                    Console.Write("Något fel uppstod, välj från 1-3");
                }

                switch (option)
                {
                    case 1:
                        await Remove();
                        break;
                    case 2:
                        await Update();
                        break;
                    case 3:
                        await MainMenu();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex) { }
        }
        public async Task Remove()
        {
            
            Console.Clear();
            Console.WriteLine("Alla bokningar ");
            Console.WriteLine();
           

            var bookningList = await GetBookingsAsync();
            var indexList = bookningList.ToList();

            var chosen = new Booking();

            if (bookningList != null)
            {
                if (bookningList.Count() > 0)
                {

                    Console.WriteLine("Välj en av bokningarna. Skriv in nummret för att radera bokningen");
                    Console.WriteLine();
                    for (int i = 0; i < bookningList.Count(); i++)
                    {
                        Console.WriteLine(" (" + i + ") " + indexList[i].Veterinary.FirstName + " " + indexList[i].Veterinary.LastName);
                        Console.WriteLine(indexList[i].Time + " " + indexList[i].Date);
                        Console.WriteLine(indexList[i].Customer.FirstName + " " + indexList[i].Customer.LastName);
                        Console.WriteLine(indexList[i].Customer.Animal.AnimalType + " " + indexList[i].Customer.Animal.Cause);
                        Console.WriteLine();

                    }

                    var indexBooking = Console.ReadLine();
                    chosen = bookningList.ElementAt(int.Parse(indexBooking));

                    await _bookingService.DeleteAsync(chosen.Id, chosen.Customer.Id, chosen.Veterinary.Id); 
                }
                
            }
            Console.WriteLine("bokningen är borttagen");
            Console.ReadKey();
            await MainMenu();

        }
        public async Task Update()
        {
            Console.Clear();
            Console.WriteLine("Alla bokningar ");
            Console.WriteLine();
            var bookningList = await GetBookingsAsync();
            var indexList = bookningList.ToList();

            //var chosen = new Booking();

            if (bookningList != null && bookningList.Any())
            {
                //if (bookningList.Count() > 0)
                {

                    Console.WriteLine("Välj en av bokningarna. Skriv in nummret för att redigera bokningen");
                    Console.WriteLine();
                    for (int i = 0; i < bookningList.Count(); i++)
                    {
                        Console.WriteLine(" (" + i + ") " + indexList[i].Veterinary.FirstName + " " + indexList[i].Veterinary.LastName);
                        Console.WriteLine(indexList[i].Time + " " + indexList[i].Date);
                        Console.WriteLine(indexList[i].Customer.FirstName + " " + indexList[i].Customer.LastName);
                        Console.WriteLine(indexList[i].Customer.Animal.AnimalType + " " + indexList[i].Customer.Animal.Cause);
                        Console.WriteLine();

                    }

                    var indexBooking = Console.ReadLine();
                    int chosenIndex = int.Parse(indexBooking);

                    if(chosenIndex >= 0 && chosenIndex < bookningList.Count()) 
                    {
                        var chosenBooking = bookningList.ElementAt(chosenIndex);

                        Console.WriteLine("lägg till ny tid");
                        string newTime = Console.ReadLine();

                        Console.WriteLine(" lägg till nytt datum");
                        string newDate = Console.ReadLine();

                        var updatedBooking = new BookingEntity
                        {
                            Id = chosenBooking.Id,
                            Time = newTime,
                            Date = newDate,

                        };

                        await _bookingService.UpdateAsync(chosenBooking.Id, updatedBooking);

                        Console.WriteLine("bokninig är redigerad"); ;
                    }
                    else
                    {
                        Console.WriteLine("ogiltigt val");
                    }


                    //await _bookingService.UpdateAsync(editBooking);
                    //bool updateResult = await _bookingService.UpdateAsync(chosenBookingId, new BookingEntity
                }
      

            }
            else
            {
                Console.WriteLine("Ingen bokning är redigerad");
            }
            Console.ReadKey();
            await MainMenu();
        }

    }
}
    

