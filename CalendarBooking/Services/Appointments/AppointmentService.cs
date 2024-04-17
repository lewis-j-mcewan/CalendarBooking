using CalendarBooking.Context;
using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public class AppointmentService : IAppointmentService
{
    private readonly CalendarContext _context;
    
    public AppointmentService(CalendarContext context)
    {
        _context = context;
    }
    
    public Appointment? GetAppointment(int day, int month, int hour, int minute)
    {
        var result = _context.Appointments
            .SingleOrDefault(a => a.Day == day
                                  && a.Month == month
                                  && a.Hour == hour
                                  && a.Min == minute);
        return result;
    }

    public void CreateAppointment(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
    }
    
    public void CreateAppointments(List<Appointment> appointments)
    {
        _context.Appointments.AddRange(appointments);
        _context.SaveChanges();
    }

    public void DeleteAppointment(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
    }

    public List<Appointment> ListAppointmentsForDay(int day, int month)
    {
        var result = _context.Appointments
            .Where(a => a.Day == day && a.Month == month)
            .OrderBy(a => a.Hour)
            .ThenBy(a => a.Min)
            .ToList();
        
        return result;
    }

    public bool IsTimeAvailable(int hour, int minute)
    {
        var result = _context.Appointments.FirstOrDefault(a => a.Hour == hour && a.Min == minute);
        if (result is null) return true;
        
        Console.WriteLine("That time is not available on day {0}/{1}", result.Day, result.Month);
        return false;
    }
}