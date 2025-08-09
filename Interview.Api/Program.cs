using Microsoft.EntityFrameworkCore;
using Interview.Infrastructure.Data;
using Interview.Application.Repositories;
using Interview.Infrastructure.Repositories;
using MediatR;
using System.Reflection;
using Interview.Api.Middleware;

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
    cfg.RegisterServicesFromAssembly(typeof(Interview.Application.Commands.Dictionary.CreateDictionaryCommand).Assembly);
});

// Add Repository Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
