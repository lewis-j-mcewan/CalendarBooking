using System.Globalization;
using CalendarBooking.Models;

namespace CalendarBooking.Context;

public static class SeedData
{
    public static void Seed(CalendarContext context)
    {
        if (context.Appointments.Any())
        {
            return;
        }
        
        const int weekInMonth = 3;
        const int dayInWeek = 2;
        
        for (var i = 1; i <= 12; i++)
        {
            var dt = new DateTime(2024, i, 1, new GregorianCalendar());
            var cal = CultureInfo.InvariantCulture.Calendar;
            
            var firstDayInMonth = dt.DayOfWeek;
            var diff = dayInWeek - (int)firstDayInMonth;

            dt = cal.AddWeeks(dt, weekInMonth-1);
            dt = cal.AddDays(dt, diff);
            
            var blockedTime = new Appointment[]
            {
                new(){Day = dt.Day, Month = dt.Month, Hour = 16, Min = 0},
                new(){Day = dt.Day, Month = dt.Month, Hour = 16, Min = 30}
            };
            context.Appointments.AddRange(blockedTime);
        }

        context.SaveChanges();

    }
}