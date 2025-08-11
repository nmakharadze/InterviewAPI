using Microsoft.EntityFrameworkCore;
using Interview.Infrastructure.Data;
using Interview.Application.Repositories;
using Interview.Infrastructure.Repositories;
using Interview.Application.Services;
using Interview.Infrastructure.Services;
using MediatR;
using System.Reflection;
using Interview.Api.Middleware;
using Interview.Application.Dictionaries.Commands.Create;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Add Image Services
builder.Services.AddScoped<IFileStorageService, FileStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global exception handling middleware
app.UseGlobalExceptionHandler();

app.UseRouting();
app.MapControllers();

app.Run();
