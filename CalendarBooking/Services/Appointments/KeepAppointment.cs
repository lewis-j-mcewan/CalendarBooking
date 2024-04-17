using CalendarBooking.Extensions;
using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public class KeepAppointment
{
    private readonly IAppointmentService _appointmentService;

    public KeepAppointment(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public bool? Keep(string[] input)
    {
        var time = input[1];
        var dateValue = DateHelper.ValidateDateTime(time);
        
        if (dateValue is null) return null;
        
        var isTimeAvailable = _appointmentService.IsTimeAvailable(dateValue.Value.Hour, dateValue.Value.Minute);

        if (!isTimeAvailable) return false;
        
        var dt = new DateTime(2024, 1, 1, dateValue.Value.Hour, dateValue.Value.Minute, 0);
        var appointmentList = new List<Appointment>();
        while (dt.Year != 2025)
        {
            var appointment = new Appointment() { Day = dt.Day, Month = dt.Month, Hour = dt.Hour, Min = dt.Minute };
            appointmentList.Add(appointment);
            dt = dt.AddDays(1);
        }
        
        _appointmentService.CreateAppointments(appointmentList);
        
        Console.WriteLine("Successfully blocked out time {0:HH:mm} everyday", dateValue.Value);
        return true;
    }
}