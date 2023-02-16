namespace holiday_api.Models;

public class TimelineWithVacation
{
   public IDictionary<DateTime, string> HolidaysAndNames { get; set; }
   public double NumberOfBusinessDays { get; set; }
   public double NumberOfVacationDays { get; set; }
   public double NumberOfHolidays { get; set; }
}