
namespace holiday_api.Constants;

public static class TaxConstants
{
    public const decimal EMPLOYER_TAX_PERCENTAGE = 0.141M;
    public const decimal EMPLOYER_TAX_PERCENTAGE_UPPER = 0.191M;
    public const decimal PROFIT_TAX = 0.22M;
    public const decimal DIVIDEND_TAX = 0.378M;


    public const decimal SOCIAL_SECURITY_TAX = 0.079M;

    public const decimal GENERAL_INCOME_TAX = 0.22M;
    public static decimal MINIMAL_DEDUCTION_2023(decimal salary)
    {
        return salary * 0.46M;
    }

    public const decimal MAXIMAL_DEDUCTION_2023 = 104_450;
    public const decimal PERSONAL_DEDUCTION_2023 = 79_600;

    // Trinnskatt
    public static Dictionary<decimal, decimal> STEP_TAX = new()
    {
      { 198_350, 0.017M },
      { 279_150, 0.04M },
      { 642_950, 0.135M },
      { 926_800, 0.165M },
      { 1_500_000, 0.175M },
    };
}
