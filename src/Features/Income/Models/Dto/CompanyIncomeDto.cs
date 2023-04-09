

namespace holiday_api.Features.Income.Models.Dto;


public class CompanyIncomeDto
{
    public int HourlyRate { get; set; }
    public int YearlyWorkHours { get; set; }
    public bool OverrideWorkHours { get; set; } = false;
    public decimal PensionPercentage { get; set; }
    public decimal Dividend { get; set; }
    public decimal Salary { get; set; }
    public decimal BrokerCommissionPercentage { get; set; }
    public decimal GeneralExpenses { get; set; }
}
