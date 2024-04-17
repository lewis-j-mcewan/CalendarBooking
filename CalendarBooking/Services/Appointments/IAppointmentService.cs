using CalendarBooking.Models;

namespace CalendarBooking.Services.Appointments;

public interface IAppointmentService
{
    Appointment? GetAppointment(int day, int month, int hour, int minute);
    void CreateAppointment(Appointment appointment);
    void CreateAppointments(List<Appointment> appointments);
    void DeleteAppointment(Appointment appointment);
    List<Appointment> ListAppointmentsForDay(int day, int month);
    bool IsTimeAvailable(int hour, int minute);
}