// Program.cs
using FluentValidation;
using Microsoft.OpenApi.Models;
using Phone.Application.Mappings;           // ContactMappingProfile
using Phone.Application.Queries.GetAllContacts;
using Phone.Behaviors;          // ValidationBehavior
using Phone.Infrastructure;     // AddInfrastructure
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ──────────── Serilog ────────────
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// ──────────── Serviços ────────────

// 1) MediatR – varre API + Application
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),          // Phone.Api
        typeof(GetAllContactsQuery).Assembly      // Phone.Application
    );

    // adiciona o pipeline de validação uma única vez
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// 2) Camada Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// 3) AutoMapper
builder.Services.AddAutoMapper(typeof(ContactMappingProfile));

// 4) FluentValidation (scaneia onde estão os validators)
builder.Services.AddValidatorsFromAssemblyContaining<ContactMappingProfile>();

// 5) MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "Phone API", Version = "v1" });
});

// ──────────── Pipeline HTTP ────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
