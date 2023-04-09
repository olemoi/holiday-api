
namespace holiday_api.Features.Income.Models.Dto;

public class PersonalIncomeDto
{
    public decimal Salary { get; set; }
    public decimal Dividend { get; set; }
    public decimal? SalaryTax { get; set; }
    public DateTime? VacationStart { get; set; }
    public DateTime? VacationEnd { get; set; }
    public int? Year { get; set; }
}
