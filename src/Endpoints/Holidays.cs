
using holiday_api.Calculators;
using holiday_api.Models;
using Microsoft.AspNetCore.Mvc;
using PublicHoliday;

namespace holiday_api.Endpoints;

public static class HolidayEndpoints
{
    public static void MapHolidayEndpoints(this WebApplication app)
    {
        app.MapGet("/holidays", GetNorwegianHolidays);
        app.MapGet("/businessdays", GetWorkDaysAndHolidays);
        app.MapGet("/businessdaysMonthly", GetWorkDaysPerMonth);
        app.MapGet("/timeline", GetTimelineWithVacation);
    }

    private static async Task<IResult> GetNorwegianHolidays([FromQuery] int year)
    {
        var result = DateCalculator.GetHolidaysAndNameForYear(year);
        return Results.Ok(result);
    }


    private static async Task<IResult> GetWorkDaysAndHolidays([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var businessDays = DateCalculator.WorkDaysBetween(fromDate, toDate);
        var holidaysBetween = DateCalculator.GetHolidaysBetweenDates(fromDate, toDate);
        businessDays -= holidaysBetween.Count();

        var result = new DaysAndHolidays
        {
            NumberOfBusinessDays = businessDays,
            HolidaysAndNames = DateCalculator.GetHolidaysAndNamesBetweenDates(fromDate, toDate),
        };
        
        return Results.Ok(result);
    }


    private static async Task<IResult> GetWorkDaysPerMonth([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var test = DateCalculator.GetWorkdaysPerMonth(fromDate, toDate);
        return Results.Ok(test);
    }


    private static async Task<IResult> GetTimelineWithVacation([FromQuery] int year, [FromQuery] DateTime vacationStart,
        [FromQuery] DateTime vacationEnd)
    {
        var timeline = DateCalculator.GetTimelineWithVacation(year, vacationStart, vacationEnd);
        return Results.Ok(timeline);
    }
}
