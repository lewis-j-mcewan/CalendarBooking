using CalendarBooking.Models;
using CalendarBooking.Services.Appointments;
using NSubstitute;

namespace Tests;

public class FindAppointmentTests
{
    private readonly IAppointmentService _appointmentService = Substitute.For<IAppointmentService>();
    private readonly FindAppointment _sut;

    public FindAppointmentTests()
    {
        _sut = new FindAppointment(_appointmentService);
    }
    
    [Fact]
    public void Find_ShouldReturn9amTimeslot_WhenDateHasNoBookings()
    {
        //Arrange
        var input = "FIND 1/1".Split(" ");
        var expected = new Appointment(){Day = 1, Month = 1, Hour = 9, Min = 0};

        _appointmentService.ListAppointmentsForDay(
            Arg.Any<int>(), Arg.Any<int>()
        ).Returns([]);
        
        //Act
        var result = _sut.Find(input);

        //Assert
        Assert.Equal(expected.Day, result.Day);
        Assert.Equal(expected.Month, result.Month);
        Assert.Equal(expected.Hour, result.Hour);
        Assert.Equal(expected.Min, result.Min);
    }

    [Fact]
    public void Find_ShouldReturnFirstTimeslot_WhenDateHasMultipleSpaces()
    {
        //Arrange
        var input = "FIND 1/1".Split(" ");
        var expected = new Appointment(){Day = 1, Month = 1, Hour = 9, Min = 30};
        
        //this creates available timeslots at 9:30, 10:30 and every time from 11:30
        var appointmentsBooked = new List<Appointment>
        {
            new() { Day = 1, Month = 1, Hour = 9, Min = 0},
            new() { Day = 1, Month = 1, Hour = 10, Min = 0},
            new() { Day = 1, Month = 1, Hour = 11, Min = 0},
        };

        _appointmentService.ListAppointmentsForDay(
            Arg.Any<int>(), Arg.Any<int>()
        ).Returns(appointmentsBooked);
        
        //Act
        var result = _sut.Find(input);

        //Assert
        Assert.Equal(expected.Day, result.Day);
        Assert.Equal(expected.Month, result.Month);
        Assert.Equal(expected.Hour, result.Hour);
        Assert.Equal(expected.Min, result.Min);
    }
    
    [Fact]
    public void Find_ShouldReturnNull_WhenDateFullyBooked()
    {
        //Arrange
        var input = "FIND 1/1".Split(" ");
        
        var appointmentsBooked = new List<Appointment>();
        var hour = 9;
        var min = 0;
        for (var i = 0; i <= 16; i++)
        {
            appointmentsBooked.Add(new Appointment {Day = 1, Month = 1, Hour = hour, Min = min});
            if (min == 0)
            {
                min = 30;
            }
            else
            {
                min = 0;
                hour += 1;
            }
        }

        _appointmentService.ListAppointmentsForDay(
            Arg.Any<int>(), Arg.Any<int>()
        ).Returns(appointmentsBooked);
        
        //Act
        var result = _sut.Find(input);

        //Assert
        Assert.Null(result);
    }
}