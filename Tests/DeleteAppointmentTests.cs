using CalendarBooking.Models;
using CalendarBooking.Services.Appointments;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Tests;

public class DeleteAppointmentTests
{
    private readonly IAppointmentService _appointmentService = Substitute.For<IAppointmentService>();
    private readonly DeleteAppointment _sut;

    public DeleteAppointmentTests()
    {
        _sut = new DeleteAppointment(_appointmentService);
    }
    
    [Fact]
    public void Delete_ShouldDeleteAppointment_WhenAppointmentExists()
    {
        //Arrange
        var input = "DELETE 1/1 12:00".Split(" ");
        var expected = new Appointment(){Day = 1, Month = 1, Hour = 12, Min = 0};

        _appointmentService.GetAppointment(
            Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()
        ).Returns(expected);
        
        //Act
        var result = _sut.Delete(input);

        //Assert
        Assert.Equal(expected.Day, result.Day);
        Assert.Equal(expected.Month, result.Month);
        Assert.Equal(expected.Hour, result.Hour);
        Assert.Equal(expected.Min, result.Min);
    }
    
    [Fact]
    public void Delete_ShouldReturnNull_WhenAppointmentNotFound()
    {
        //Arrange
        var input = "DELETE 1/1 12:00".Split(" ");

        _appointmentService.GetAppointment(
            Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()
        ).ReturnsNull();
        
        //Act
        var result = _sut.Delete(input);

        //Assert
        Assert.Null(result);
    }
}