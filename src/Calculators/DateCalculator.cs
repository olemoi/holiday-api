using holiday_api.Models;
using PublicHoliday;

namespace holiday_api.Calculators;

public static class DateCalculator
{
    
    static string[] MonthsInYear = new string[] { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember" }; 
    public static IEnumerable<DateTime> GetHolidaysBetweenDates(DateTime fromDate, DateTime toDate)
    {
        var holidays = GetHolidaysForYear(fromDate.Year);

        if (fromDate.Year != toDate.Year)
        {
            holidays.ToList().AddRange(GetHolidaysForYear(toDate.Year));
        }

        var holidaysBetween = holidays.Where(x => 
            fromDate.Ticks <= x.Ticks && 
            x.Ticks <= toDate.Ticks &&
            x.DayOfWeek != DayOfWeek.Saturday &&
            x.DayOfWeek != DayOfWeek.Sunday
            );
        return holidaysBetween;
    }

    public static double WorkDaysBetween(DateTime fromDate, DateTime toDate)
    {
        var businessDays = 1 + ((toDate - fromDate).TotalDays * 5 - (fromDate.DayOfWeek - toDate.DayOfWeek) * 2) / 7;
 
        if (toDate.DayOfWeek == DayOfWeek.Saturday) businessDays--;
        if (fromDate.DayOfWeek == DayOfWeek.Sunday) businessDays--;
        
        // Subtract 1 days. the day before enddate is the last working date.
        businessDays -= 1;
        if (businessDays < 0) businessDays = 0;
        return businessDays;
    }


    private static IList<DateTime> GetHolidaysForYear(int year)
    {
        return new NorwayPublicHoliday().PublicHolidays(year);
    }

    public static IDictionary<DateTime, string> GetHolidaysAndNameForYear(int year)
    {
        return new NorwayPublicHoliday().PublicHolidayNames(year);
    }

    public static IDictionary<DateTime, string> GetHolidaysAndNamesBetweenDates(DateTime fromDate, DateTime toDate)
    {
        var holidays = GetHolidaysAndNameForYear(fromDate.Year);

        if (fromDate.Year != toDate.Year)
        {
            var nextYearHolidays = GetHolidaysAndNameForYear(toDate.Year);
            foreach (var pair in nextYearHolidays)
            {
                holidays.Add(pair.Key, pair.Value);
            }
        }

        Dictionary<DateTime, string> holidaysBetween = new Dictionary<DateTime, string>();
        foreach (var holiday in holidays)
        {
            if (fromDate.Ticks <= holiday.Key.Ticks && holiday.Key.Ticks <= toDate.Ticks)
            {
               holidaysBetween.Add(holiday.Key, holiday.Value); 
            }
        }

        return holidaysBetween;
    }

    public static IDictionary<string, double> GetWorkdaysPerMonth(DateTime fromDate, DateTime toDate)
    {
        var months = new string[] { "Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember" }; 
        var workDaysPerMonth = new Dictionary<string, double>();
        var test = new List<(DateTime, DateTime)>();
        var lastDayInFirstMonth = new DateTime(fromDate.Year, fromDate.Month + 1, 1).AddTicks(-1);
        test.Add((fromDate, lastDayInFirstMonth));
        var tempDate = new DateTime(fromDate.Year, fromDate.Month + 1, 1);
        while (tempDate< toDate && (tempDate.Month != toDate.Month || tempDate.Year != toDate.Year))
        {
            var lastDayOfMonth = tempDate.AddMonths(1).AddTicks(-1);
            test.Add((tempDate, lastDayOfMonth));
            tempDate = tempDate.AddMonths(1);
        }

        foreach (var dateRange in test)
        {
            var businessDays = Math.Ceiling(WorkDaysBetween(dateRange.Item1, dateRange.Item2)); 
            workDaysPerMonth.Add($"{months[dateRange.Item1.Month - 1]} {dateRange.Item1.Year}", businessDays);    
        }

        var firstDayOfLastMonth = new DateTime(toDate.Year, toDate.Month, 1);
        var businessDaysInLastMonth = Math.Ceiling(WorkDaysBetween(firstDayOfLastMonth, toDate));
        workDaysPerMonth.Add($"{months[toDate.Month - 1]} {toDate.Year}", businessDaysInLastMonth);
        return workDaysPerMonth;
    }


    public static TimelineWithVacation GetTimelineWithVacation(int year, DateTime vacationStart,
        DateTime vacationEnd)
    {
        var today = DateTime.Today;
        var endOfYear = new DateTime(today.Year, 12, 31);
        if (year != today.Year)
        {
            today = new DateTime(year, 1, 1);
            endOfYear = new DateTime(year, 12, 31);
        }

        double businessDaysDuringVacation = 0;
        IEnumerable<DateTime> holidaysDuringVacation = new List<DateTime>();
        if (!vacationEnd.Equals(vacationStart))
        {
            businessDaysDuringVacation = WorkDaysBetween(vacationStart, vacationEnd);
            holidaysDuringVacation = GetHolidaysBetweenDates(vacationStart, vacationEnd);
        }

        var businessDaysLeft = WorkDaysBetween(today, endOfYear);
        var holidaysAndNamesForYear = GetHolidaysAndNameForYear(year);
        var vacationDays = businessDaysDuringVacation - holidaysDuringVacation.Count();


        businessDaysLeft -= (businessDaysDuringVacation - holidaysDuringVacation.Count());
        businessDaysLeft -= holidaysAndNamesForYear.Count();

        if (!vacationEnd.Equals(vacationStart))
        {
            holidaysAndNamesForYear.Add(vacationStart, "Ferie start");
            holidaysAndNamesForYear.Add(vacationEnd, "Ferie slutt");
        }


        SortedDictionary<DateTime, string> holidaysBetween = new SortedDictionary<DateTime, string>();
        foreach (var holiday in holidaysAndNamesForYear)
        {
            if (today.Ticks <= holiday.Key.Ticks && holiday.Key.Ticks <= endOfYear.Ticks)
            {
               holidaysBetween.Add(holiday.Key, holiday.Value); 
            }
        }


        return new TimelineWithVacation()
        {
            HolidaysAndNames = holidaysBetween,
            NumberOfBusinessDays = businessDaysLeft,
            NumberOfVacationDays = vacationDays,
            NumberOfHolidays = holidaysBetween.Count(),
        };
    }
}