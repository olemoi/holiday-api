
using holiday_api.Constants;

namespace holiday_api.Features.Income.Services;

public class PersonalFinanceService : IPersonalFinanceService
{
    public decimal CalculateGeneralTax(decimal salary)
    {
        var minDeduction = TaxConstants.MINIMAL_DEDUCTION_2023(salary);
        minDeduction = minDeduction > TaxConstants.MAXIMAL_DEDUCTION_2023 ? TaxConstants.MAXIMAL_DEDUCTION_2023 : minDeduction;

        var generalTaxBasis = salary - minDeduction - TaxConstants.PERSONAL_DEDUCTION_2023;

        return generalTaxBasis * TaxConstants.GENERAL_INCOME_TAX;
    }

    public decimal CalculateSocialSecurityTax(decimal salary)
    {
        return salary * TaxConstants.SOCIAL_SECURITY_TAX;
    }

    public decimal CalculateStepTax(decimal salary)
    {
        var accumelatedTax = 0M;
        KeyValuePair<decimal, decimal> previousStep = new(0, 0);
        foreach (var step in TaxConstants.STEP_TAX)
        {
            var taxBasis = salary <= step.Key ? salary - previousStep.Key : step.Key - previousStep.Key;
            accumelatedTax += taxBasis * previousStep.Value;
            if (salary <= step.Key) break;
            previousStep = step;
        }

        return accumelatedTax;
    }

    public IReadOnlyDictionary<string, decimal> CalculateStepTaxVerbose(decimal salary)
    {
        var verboseTax = new Dictionary<string, decimal>();
        KeyValuePair<decimal, decimal> previousStep = new(0, 0);
        foreach (var step in TaxConstants.STEP_TAX)
        {
            var taxBasis = salary <= step.Key ? salary - previousStep.Key : step.Key - previousStep.Key;
            verboseTax.Add($"Trinn {Math.Round(previousStep.Value * 100, 1)}%", taxBasis * previousStep.Value);
            if (salary <= step.Key) break;
            previousStep = step;
        }

        return verboseTax;
    }
}
