

using holiday_api.Constants;

namespace holiday_api.Features.Income.Models.Company;


public class CompanyResult
{
    public decimal Profit { get; init; }
    public decimal ProfitTax { get; init; }
    public decimal ProfitAfterTax { get; init; }
    public decimal ProfitAfterDividend { get; init; }
    public decimal Dividend { get; init; }
}
