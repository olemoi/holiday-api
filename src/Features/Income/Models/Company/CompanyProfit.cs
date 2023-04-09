namespace holiday_api.Features.Income.Models.Company;


public class CompanyProfit
{
    public decimal Pension { get; init; }
    public decimal EmployerTaxPension { get; init; }
    public decimal Salary { get; init; }
    public decimal EmployerTaxSalary { get; init; }
    public decimal Expenses { get; init; }
    public decimal Profit { get; init; }
}

