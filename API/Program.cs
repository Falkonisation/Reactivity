using API.Extentions;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        ApplicationServiceExtention.IServicesLoeder(builder);
        var app = builder.Build();
        using var scope =app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context);
        }
        catch(Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex,"An error occurd during migration");
        }
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseHttpsRedirection();
       // app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}