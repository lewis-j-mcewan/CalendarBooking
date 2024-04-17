using CalendarBooking.Extensions;

namespace Tests;

public class DateHelperTests
{
    [Fact]
    public void ValidateDateTime_ShouldReturnDateTime_WhenValidDateTime()
    {
        //Arrange
        var input = "20/12/2024 16:30";
        var expected = new DateTime(2024, 12, 20, 16, 30, 0);
        
        //Act
        var result = DateHelper.ValidateDateTime(input);

        //Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void ValidateDateTime_ShouldReturnNull_WhenTimeBefore9am()
    {
        //Arrange
        var input = "1/1/2024 08:30";
        
        //Act
        var result = DateHelper.ValidateDateTime(input);

        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void ValidateDateTime_ShouldReturnNull_WhenTimeAfter5pm()
    {
        //Arrange
        var input = "1/1/2024 17:30";
        
        //Act
        var result = DateHelper.ValidateDateTime(input);

        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void ValidateDateTime_ShouldReturnNull_WhenTimeNot30minIncrement()
    {
        //Arrange
        var input = "1/1/2024 13:01";
        
        //Act
        var result = DateHelper.ValidateDateTime(input);

        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void ValidateDateTime_ShouldReturnDateTime_WhenValidTime()
    {
        //Arrange
        var input = "16:30";
        
        //Act
        var result = DateHelper.ValidateDateTime(input);

        //Assert
        Assert.Equal(16, result.Value.Hour);
        Assert.Equal(30, result.Value.Minute);
    }
    
    [Fact]
    public void ValidateDate_ShouldReturnDateTime_WhenValidDate()
    {
        //Arrange
        var input = "20/12/2024";
        var expected = new DateTime(2024, 12, 20, 0, 0, 0);
        
        //Act
        var result = DateHelper.ValidateDate(input);

        //Assert
        Assert.Equal(expected, result);
    }
}