using CalendarBooking.Models;
using CalendarBooking.Services.Appointments;
using NSubstitute;

namespace Tests;

public class KeepAppointmentTests
{
    private readonly IAppointmentService _appointmentService = Substitute.For<IAppointmentService>();
    private readonly KeepAppointment _sut;

    public KeepAppointmentTests()
    {
        _sut = new KeepAppointment(_appointmentService);
    }
    
    [Fact]
    public void Keep_ShouldReturnTrue_WhenTimeIsAvailable()
    {
        //Arrange
        var input = "KEEP 09:00".Split(" ");

        _appointmentService.IsTimeAvailable(
            Arg.Any<int>(), Arg.Any<int>()
        ).Returns(true);
        
        //Act
        var result = _sut.Keep(input);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Keep_ShouldReturnFalse_WhenTimeIsTakenOnDay()
    {
        //Arrange
        var input = "KEEP 09:00".Split(" ");

        _appointmentService.IsTimeAvailable(
            Arg.Any<int>(), Arg.Any<int>()
        ).Returns(false);
        
        //Act
        var result = _sut.Keep(input);

        //Assert
        Assert.False(result);
    }
}