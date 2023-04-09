

using holiday_api.Constants;
using holiday_api.Features.Income.Models.Company;
using holiday_api.Features.Income.Models.Dto;

namespace holiday_api.Features.Income.Services;

public class CompanyFinanceService : ICompanyFinanceService
{
    public CompanyFinance CalculateAllFinancials(CompanyIncomeDto cid)
    {
        return new CompanyFinance
        {
            CompanyProfit = CalculateProfit(cid),
            CompanyTurnover = CalculateTurnover(cid),
            CompanyResult = CalculateResult(cid),
        };
    }

    public CompanyProfit CalculateProfit(CompanyIncomeDto cid)
    {
        var turnover = CalculateTurnover(cid);
        var pension = cid.Salary * cid.PensionPercentage;
        var employerTaxPension = CalculateEmployerTaxPension(pension);
        var employerTaxSalary = CalculateEmployerTaxSalary(cid.Salary);

        var profit = turnover.NetTurnover - cid.Salary - pension - employerTaxPension - employerTaxSalary - cid.GeneralExpenses;

        return new CompanyProfit()
        {
            Pension = pension,
            Salary = cid.Salary,
            EmployerTaxPension = employerTaxPension,
            EmployerTaxSalary = employerTaxSalary,
            Expenses = cid.GeneralExpenses,
            Profit = profit,
        };

    }

    public CompanyResult CalculateResult(CompanyIncomeDto cid)
    {
        var profit = CalculateProfit(cid);
        var taxOnProfit = profit.Profit * TaxConstants.PROFIT_TAX;
        var profitAfterTax = profit.Profit - taxOnProfit;
        return new CompanyResult()
        {
            Profit = profit.Profit,
            ProfitTax = taxOnProfit,
            ProfitAfterTax = profitAfterTax,
            ProfitAfterDividend = profitAfterTax - cid.Dividend,
        };
    }

    public CompanyTurnover CalculateTurnover(CompanyIncomeDto cid)
    {
        var grossTurnover = cid.HourlyRate * cid.YearlyWorkHours;
        var commission = cid.BrokerCommissionPercentage * grossTurnover;
        var netTurnover = grossTurnover - commission;
        var turnover = new CompanyTurnover()
        {
            GrossTurnover = grossTurnover,
            BrokerCommission = commission,
            NetTurnover = netTurnover,
        };


        return turnover;
    }





    private decimal CalculateEmployerTaxSalary(decimal salary)
    {
        return salary > 750_000
          ? (750_000 * TaxConstants.EMPLOYER_TAX_PERCENTAGE) + (salary - 750_000) * TaxConstants.EMPLOYER_TAX_PERCENTAGE_UPPER
          : salary * TaxConstants.EMPLOYER_TAX_PERCENTAGE;
    }

    private decimal CalculateEmployerTaxPension(decimal pension)
    {
        return pension > 750_000
          ? (750_000 * TaxConstants.EMPLOYER_TAX_PERCENTAGE) + (pension - 750_000) * TaxConstants.EMPLOYER_TAX_PERCENTAGE_UPPER
          : pension * TaxConstants.EMPLOYER_TAX_PERCENTAGE;
    }
}




