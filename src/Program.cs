using holiday_api.Endpoints;
using holiday_api.Features.Income.Endpoints;
using holiday_api.Features.Income.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICompanyFinanceService, CompanyFinanceService>();
builder.Services.AddSingleton<IPersonalFinanceService, PersonalFinanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    // Do work that can write to the Response
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});
app.UseHttpsRedirection();
app.MapHolidayEndpoints();
app.MapPersonalIncomeEndpoints();
app.MapCompanyIncomeEndpoints();
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
});

// app.UseAuthorization();

// app.MapControllers();

app.Run();
