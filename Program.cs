using DayTypeService.DbContext;
using DayTypeService.Repositories;
using DayTypeService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure Entity Framework and Postgre
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<CalendarService>();
builder.Services.AddScoped<CalendarRepository>();

var app = builder.Build();

// Заполнение базы данных при старте
using (var scope = app.Services.CreateScope())
{
    var calendarService = scope.ServiceProvider.GetRequiredService<CalendarService>();
    var calendarRepository = scope.ServiceProvider.GetRequiredService<CalendarRepository>();

    var year = DateTime.Now.Year;
    var dayInfos = await calendarService.GetCalendarDataAsync(year);
    await calendarRepository.SaveCalendarDataAsync(dayInfos);
}

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
