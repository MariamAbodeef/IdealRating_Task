
using IdealRatingTechnicalTask.Application.Services.PersonService;
using IdealRatingTechnicalTask.Domain.Abstraction;
using IdealRatingTechnicalTask.Infrastructure.CompositeDataService;
using IdealRatingTechnicalTask.Infrastructure.Context;
using IdealRatingTechnicalTask.Infrastructure.ExcelReader;
using IdealRatingTechnicalTask.Infrastructure.Reporsitories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString")));

builder.Services.AddScoped<IPersonRepo, PersonRepo>();
builder.Services.AddScoped<IPersonExcelReader, PersonExcelReader>();
builder.Services.AddScoped<ICompositePersonService, CompositePersonService>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
