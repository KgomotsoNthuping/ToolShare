using Api.Data;
using Api.Models;
using Api.Services;   

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<
        IToolShare,
        InMemoryToolShare>();

builder.Services.AddSingleton<
        ILoanService,
        LoanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();

app.UseAuthorization();

app.MapControllers();

app.Run();
