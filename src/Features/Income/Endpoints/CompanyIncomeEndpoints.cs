
using holiday_api.Features.Income.Models;
using holiday_api.Features.Income.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using holiday_api.Features.Income.Models;
using holiday_api.Features.Income.Models.Company;
using holiday_api.Features.Income.Services;

namespace holiday_api.Features.Income.Endpoints;

public static class CompanyIncomeEndpoints
{
    public static void MapCompanyIncomeEndpoints(this WebApplication app)
    {
        app.MapPost("/income/company", GetCompanyIncomeOverview);
    }

    private static IResult GetCompanyIncomeOverview([FromServices] ICompanyFinanceService cfs, [FromBody] CompanyIncomeDto cid)
    {
        var companyFinances = cfs.CalculateAllFinancials(cid);
        return Results.Ok(companyFinances);
    }


}
