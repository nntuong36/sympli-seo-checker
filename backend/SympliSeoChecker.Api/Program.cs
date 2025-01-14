using MediatR;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using SympliSeoChecker.Api.Middlewares;
using SympliSeoChecker.Application.DependencyRegisters;
using SympliSeoChecker.Application.Validators;
using SympliSeoChecker.Service.Caching;
using SympliSeoChecker.Service.Factories;
using SympliSeoChecker.Service.Interfaces;
using SympliSeoChecker.Service.SearchEngines;

var builder = WebApplication.CreateBuilder(args);

#region service configure
// Register Cache service
builder.Services.AddSingleton<ICachingService, MemoryCacheService>();
builder.Services.AddMemoryCache();

// Register HttpClient
builder.Services.AddHttpClient<GoogleSearchEngineService>();
builder.Services.AddHttpClient<BingSearchEngineService>();

// Register service
builder.Services.AddTransient<IGoogleSearchEngineService, GoogleSearchEngineService>();
builder.Services.AddTransient<IBingSearchEngineService, BingSearchEngineService>();
builder.Services.AddSingleton<ISearchEngineFactory, SearchEngineFactory>();
#endregion

// Register MediatR
builder.Services.RegisterMediatRDependencies();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Register FluentValidation
builder.Services.RegisterFluentValidationDependencies();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Log file
builder.Services.AddSerilog((services, lc) =>
{
    lc.ReadFrom.Configuration(builder.Configuration)
       .ReadFrom.Services(services)
       .Enrich.FromLogContext()
       .Enrich.WithExceptionDetails(
           new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            .WithDestructuringDepth(5)
            .WithDestructurers([new DbUpdateExceptionDestructurer()])
       )
       .WriteTo.File(
           outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u5}] {Message:lj}{NewLine}{Exception}",
           path: "ApplicationLogs\\api-.log",
           rollingInterval: RollingInterval.Day)
       .WriteTo.Console();
});

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Log.Information("Application is started");
app.Run();
Log.Information("Application is stopped");