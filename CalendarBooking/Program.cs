using CalendarBooking.Context;
using CalendarBooking.Services;
using CalendarBooking.Services.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services =>
    {
        services.AddDbContext<CalendarContext>(
            options =>
                options.UseSqlServer(
                    "Data Source=localhost; Initial Catalog=calendar;User Id=sa; Password=YourStrong@Passw0rd; MultiSubnetFailover=True; Encrypt=False"));
        services.AddSingleton<IApplication, Application>();
        services.AddScoped<IAppointmentService, AppointmentService>();
    }).Build();

var calendarContext = _host.Services.GetRequiredService<CalendarContext>();
calendarContext.Database.Migrate();
SeedData.Seed(calendarContext);

var app = _host.Services.GetRequiredService<IApplication>();

Console.WriteLine("Please enter an operator and date");
var input = Console.ReadLine();

app.Run(input);