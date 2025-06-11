var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
using var context = new AppDbContext();

Console.WriteLine("View 1: Service Assignment Stats");
var stats = context.vw_ServiceAssignmentStats.ToList();
foreach (var s in stats)
{
    Console.WriteLine($"{s.ServiceName}: {s.TotalThisWeek} assigned this week");
}

Console.WriteLine("\nView 2: Used vs Unused");
var usedStats = context.vw_UsedUnusedAssignments.ToList();
foreach (var u in usedStats)
{
    Console.WriteLine($"{u.ServiceName} - Used: {u.Used}, Unused: {u.Unused}");
}

Console.WriteLine("\nView 3: Assignments per Kiosk");
var kioskStats = context.vw_KioskAssignmentCount.ToList();
foreach (var k in kioskStats)
{
    Console.WriteLine($"{k.DeviceName}: {k.TotalAssignments} assignments");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
