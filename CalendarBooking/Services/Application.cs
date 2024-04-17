using CalendarBooking.Models;
using CalendarBooking.Services.Appointments;

namespace CalendarBooking.Services;

public class Application : IApplication
{
    private readonly IAppointmentService _appointmentService;

    public Application(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public void Run(string input)
    {
        var inputSplit = input.Split(" ");
        var operation = inputSplit[0];
        Appointment? result;
        bool? keepResult;
        
        switch (operation)
        {
            case "ADD": // DD/MM hh:mm
                result = new CreateAppointment(_appointmentService).Create(inputSplit);
                break;
            case "DELETE": // DD/MM hh:mm
                result = new DeleteAppointment(_appointmentService).Delete(inputSplit);
                break;
            case "FIND": // DD/MM
                result = new FindAppointment(_appointmentService).Find(inputSplit);
                break;
            case "KEEP": // hh:mm
                keepResult = new KeepAppointment(_appointmentService).Keep(inputSplit);
                break;
            default:
                Console.WriteLine("Input was invalid");
                break;
        }
    }
}