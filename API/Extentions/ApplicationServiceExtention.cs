using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static void IServicesLoeder(WebApplicationBuilder builder)
        {
             builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
            builder.Services.AddCors(opt=>
        {
            opt.AddPolicy("CorsPolicy",policy=>
            {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });
        });
            builder.Services.AddMediatR(typeof(List.Handler).Assembly);
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        }
    }
}