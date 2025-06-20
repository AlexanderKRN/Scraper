using Scraper.API.Common;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Scraper.Infrastructure;
using Scraper.Application;
using Hangfire;
using Scraper.Infrastructure.Jobs;

namespace Scraper.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddFluentValidationAutoValidation(configuration =>
        configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>());

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services
            .AddApplication()
            .AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseHangfireDashboard();
        app.MapHangfireDashboard();
        HangfireWorker.StartRecurringJobs();

        app.Run();
    }
}
