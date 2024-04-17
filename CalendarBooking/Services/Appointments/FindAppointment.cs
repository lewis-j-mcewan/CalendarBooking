using CalendarBooking.Extensions;
using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public class FindAppointment
{
    private readonly IAppointmentService _appointmentService;

    public FindAppointment(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public Appointment? Find(string[] input)
    {
        var date = input[1];
        var dateString = date + "/2024";

        var dateValue = DateHelper.ValidateDate(dateString);
        
        if (dateValue is null) return null;

        var appointments = _appointmentService.ListAppointmentsForDay(dateValue.Value.Day, dateValue.Value.Month);

        var timeslot = new DateTime(dateValue.Value.Year, dateValue.Value.Month, dateValue.Value.Day, 9, 0,0);
        foreach (var appointment in appointments)
        {
            if (timeslot.Hour == appointment.Hour && timeslot.Minute == appointment.Min)
            {
                timeslot = timeslot.AddMinutes(30);
            }
            else
            {
                break;
            }
        }

        if (timeslot is { Hour: 17, Minute: > 0 })
        {
            Console.WriteLine("There are no available timeslots for day {0:dd/MM}", timeslot);
            return null;
        }
        
        Console.WriteLine("There is an available time slot at {0:HH:mm}", timeslot);
        return new Appointment(){Day = timeslot.Day, Month = timeslot.Month, Hour = timeslot.Hour, Min = timeslot.Minute};
    }
}