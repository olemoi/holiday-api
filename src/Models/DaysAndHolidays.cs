namespace holiday_api.Models;

public class DaysAndHolidays
{
   public IDictionary<DateTime, string> HolidaysAndNames { get; set; }
   public double NumberOfBusinessDays { get; set; }
}