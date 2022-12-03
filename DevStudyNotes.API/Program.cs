using DevStudyNotes.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conStr = builder.Configuration.GetConnectionString("DevStudyNotes");


builder.Services.AddDbContext<StudyNoteDbContext>(
    o => o.UseSqlServer(conStr)
    );

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(conStr,
    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions()
    {
        AutoCreateSqlTable = true,
        TableName = "Logs"
    })
    .WriteTo.Console()
    .CreateLogger();
}).UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevStudyNotes.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Felipe Mendes",
            Email = "Felipe14_mendes@hotmail.com",
            Url = new Uri("https://www.linkedin.com/in/feelipemendes/")
        }
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
