using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                       builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH")
                      );
});
// Add your EmployeeService with appropriate lifetime (consider using AddTransient or AddSingleton if needed)

builder.Services.AddScoped<IEmployee, EmployeeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
