namespace CalendarBooking.Extensions;

public static class DateHelper
{
    public static DateTime? ValidateDateTime(string dateString)
    {
        var dateValue = ValidateDate(dateString);
        if (dateValue is null) return null;
        
        if (dateValue.Value.Hour < 9 || dateValue is { Hour: >= 17, Minute: > 0 })
        {
            Console.WriteLine("Appointments can only be booked between 0900-1700");
            return null;
        }
        if (dateValue.Value.Minute is not (0 or 30))
        {
            Console.WriteLine("Appointments are 30mins long, and can only begin every half hour");
            return null;
        }

        return dateValue;
    }

    public static DateTime? ValidateDate(string dateString)
    {
        if (DateTime.TryParse(dateString, out var dateValue)) return dateValue;
        
        Console.WriteLine("Input is not a valid date");
        return null;

    }
}