

namespace holiday_api.Features.Income.Services;

public interface IPersonalFinanceService
{
    decimal CalculateSocialSecurityTax(decimal salary);
    decimal CalculateGeneralTax(decimal salary);
    decimal CalculateStepTax(decimal salary);
    IReadOnlyDictionary<string, decimal> CalculateStepTaxVerbose(decimal salary);
}
