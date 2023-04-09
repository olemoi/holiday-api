using holiday_api.Calculators;
using holiday_api.Constants;
using holiday_api.Models;

namespace holiday_api.Features.Income.Services;

public class PersonalIncomeTax
{




    public decimal GeneralIncomeTax { get; init; }
    public decimal SocialSecurityTax { get; init; }
    public decimal StepTax { get; init; }
    public IReadOnlyDictionary<string, decimal> StepTaxVerbose { get; init; }
    public decimal SumTax { get; init; }
    public decimal SumMonthlyTax { get; init; }
    public decimal TaxPercentage { get; init; }
    public TimelineWithVacation Timeline { get; init; }



}
