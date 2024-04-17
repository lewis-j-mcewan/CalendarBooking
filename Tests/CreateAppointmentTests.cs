using CalendarBooking.Models;
using CalendarBooking.Services.Appointments;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Tests;

public class CreateAppointmentTests
{
    private readonly IAppointmentService _appointmentService = Substitute.For<IAppointmentService>();
    private readonly CreateAppointment _sut;

    public CreateAppointmentTests()
    {
        _sut = new CreateAppointment(_appointmentService);
    }
    
    [Fact]
    public void Create_ShouldCreateAppointment_WhenAppointmentNotFound()
    {
        //Arrange
        var input = "ADD 1/1 12:00".Split(" ");
        var expected = new Appointment(){Day = 1, Month = 1, Hour = 12, Min = 0};
        
        _appointmentService.GetAppointment(
            Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()
        ).ReturnsNull();
        
        //Act
        var result = _sut.Create(input);

        //Assert
        Assert.Equal(expected.Day, result.Day);
        Assert.Equal(expected.Month, result.Month);
        Assert.Equal(expected.Hour, result.Hour);
        Assert.Equal(expected.Min, result.Min);
    }

    [Fact]
    public void Create_ShouldReturnNull_WhenAppointmentExists()
    {
        //Arrange
        var input = "ADD 1/1 12:00".Split(" ");
        var appointment = new Appointment {Day = 1, Month = 1, Hour = 12, Min = 0};

        _appointmentService.GetAppointment(
            Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()
        ).Returns(appointment);
        
        //Act
        var result = _sut.Create(input);

        //Assert
        Assert.Null(result);
    }
}