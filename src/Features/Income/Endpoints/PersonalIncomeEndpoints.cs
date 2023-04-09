
using holiday_api.Constants;
using holiday_api.Features.Income.Models;
using holiday_api.Features.Income.Models.Dto;
using holiday_api.Features.Income.Services;
using Microsoft.AspNetCore.Mvc;

namespace holiday_api.Features.Income.Endpoints;

public static class PersonalIncomeEndpoints
{
    public static void MapPersonalIncomeEndpoints(this WebApplication app)
    {
        app.MapPost("/income/personal", GetPersonalIncomeOverview);
    }

    private static IResult GetPersonalIncomeOverview([FromServices] IPersonalFinanceService pfs, [FromBody] PersonalIncomeDto pid)
    {

        var generalTax = pfs.CalculateGeneralTax(pid.Salary);
        var ssTax = pfs.CalculateSocialSecurityTax(pid.Salary);
        var stepTax = pfs.CalculateStepTax(pid.Salary);
        var stepTaxVerbose = pfs.CalculateStepTaxVerbose(pid.Salary);
        var sumTax = generalTax + stepTax + ssTax;

        var tax = new PersonalIncomeTax
        {
            GeneralIncomeTax = generalTax,
            SocialSecurityTax = ssTax,
            StepTax = stepTax,
            StepTaxVerbose = stepTaxVerbose,
            SumTax = sumTax,
            SumMonthlyTax = sumTax / 12,
            TaxPercentage = sumTax / pid.Salary,
        };


        var personalIncome = new PersonalIncome
        {
            MonthlySalary = pid.Salary / 12,
            YearlySalary = pid.Salary,
            PersonalIncomeTax = tax,
            Dividend = pid.Dividend,
            DividendTax = pid.Dividend * TaxConstants.DIVIDEND_TAX,
        };


        return Results.Ok(personalIncome);
    }


}
