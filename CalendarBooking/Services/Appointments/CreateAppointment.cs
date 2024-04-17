using CalendarBooking.Extensions;
using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public class CreateAppointment
{
    private readonly IAppointmentService _appointmentService;
    
    public CreateAppointment(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public Appointment? Create(string[] input)
    {
        var date = input[1];
        var time = input[2];
                
        var dateString = date + "/2024 " + time;
        var dateValue = DateHelper.ValidateDateTime(dateString);

        if (dateValue is null) return null;
                
        var existingAppointment = _appointmentService.GetAppointment(dateValue.Value.Day, dateValue.Value.Month, dateValue.Value.Hour, dateValue.Value.Minute);

        if (existingAppointment is not null)
        {
            Console.WriteLine("That appointment slot is already taken");
            return null;
        }
        
        var draftAppointment = new Appointment()
        {
            Day = dateValue.Value.Day,
            Month = dateValue.Value.Month,
            Hour = dateValue.Value.Hour,
            Min = dateValue.Value.Minute
        };
                
        _appointmentService.CreateAppointment(draftAppointment);
        Console.WriteLine("Successfully added appointment");
        return draftAppointment;
    }
}