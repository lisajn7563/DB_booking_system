using Database_Booking_System.Context;
using Database_Booking_System.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Database_Booking_System;

internal class Program
{
    static async Task Main(string[] args)
    {
      
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\skola\Database_Booking_System\Database_Booking_System\Context\database.mdf;Integrated Security=True;Connect Timeout=30"));
                services.AddScoped<MenuService> ();
                services.AddScoped<AnimalService> ();
                services.AddScoped<BookingService> ();
                services.AddScoped<CustomerAddressService> ();
                services.AddScoped<CustomerService> ();
                services.AddScoped<VeterinaryService> ();
                services.AddLogging(builder =>
                {
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                    builder.AddFilter("System", LogLevel.Error);
                });
            })
            .Build();
        using ( var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var menuService = services.GetRequiredService<MenuService> ();
            await menuService.MainMenu();
        }

        await host.RunAsync();
    }
}