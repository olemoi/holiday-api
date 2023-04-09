
using holiday_api.Features.Income.Models.Company;
using holiday_api.Features.Income.Models.Dto;

namespace holiday_api.Features.Income.Services;

public interface ICompanyFinanceService
{
    CompanyProfit CalculateProfit(CompanyIncomeDto cid);
    CompanyTurnover CalculateTurnover(CompanyIncomeDto cid);
    CompanyResult CalculateResult(CompanyIncomeDto cid);
    CompanyFinance CalculateAllFinancials(CompanyIncomeDto cid);
}
