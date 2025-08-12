using Microsoft.EntityFrameworkCore;
using Interview.Infrastructure.Data;
using Interview.Application.Repositories;
using Interview.Infrastructure.Repositories;
using Interview.Application.Services;
using Interview.Infrastructure.Services;
using Interview.Application.Repositories.Person;
using Interview.Infrastructure.Repositories.Person;
using MediatR;
using System.Reflection;
using Interview.Api.Middleware;
using Interview.Api.Extensions;
using Interview.Application.Dictionaries.Commands.Create;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Accept-Language", new OpenApiSecurityScheme
    {
        Name = "Accept-Language",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Culture code, e.g. en-US or ka-GE"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Accept-Language"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add DbContext
builder.Services.AddDbContext<Interview.Infrastructure.Data.InterviewDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR for CQRS
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateDictionaryCommand).Assembly);
});

// Add Repository Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Specialized Person Repository Services (optional - for direct injection)
builder.Services.AddScoped<IPersonSearchRepository, PersonSearchRepository>();
builder.Services.AddScoped<IPersonPhoneRepository, PersonPhoneRepository>();
builder.Services.AddScoped<IPersonRelationRepository, PersonRelationRepository>();
builder.Services.AddScoped<IPersonReportRepository, PersonReportRepository>();

// Add Image Services
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

// Add Localization Services
builder.Services.AddCustomLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom localization middleware
app.UseCustomLocalization();

// Global exception handling middleware
app.UseGlobalExceptionHandler();

app.UseRouting();
app.MapControllers();

app.Run();
