using CalendarBooking.Extensions;
using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public class DeleteAppointment
{
    private readonly IAppointmentService _appointmentService;
    
    public DeleteAppointment(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public Appointment? Delete(string[] input)
    {
        var date = input[1];
        var time = input[2];
        
        var dateString = date + "/2024 " + time;
        var dateValue = DateHelper.ValidateDateTime(dateString);

        if (dateValue is null) return null;
                
        var toDelete = _appointmentService.GetAppointment(dateValue.Value.Day, dateValue.Value.Month, dateValue.Value.Hour, dateValue.Value.Minute);
            
        if (toDelete is null)
        {
            Console.WriteLine("That appointment could not be found");
            return null;
        }
                
        _appointmentService.DeleteAppointment(toDelete);
        Console.WriteLine("Successfully deleted appointment");
        return toDelete;
    }
}