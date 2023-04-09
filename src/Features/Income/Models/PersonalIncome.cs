
using holiday_api.Features.Income.Services;

namespace holiday_api.Features.Income.Models;

public class PersonalIncome
{
    public decimal MonthlySalary { get; init; }
    public decimal YearlySalary { get; init; }
    public PersonalIncomeTax PersonalIncomeTax { get; init; }
    public decimal Dividend { get; init; }
    public decimal DividendTax { get; init; }
}
