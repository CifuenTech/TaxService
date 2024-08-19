using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
            .AddApiDocumentation();

            builder.Services.AddSwaggerGen();

            //TODO Implement Exception Handler

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddRemoteServices(builder.Configuration);

            builder.Services.AddTaxCalculators();

            var connection = String.Empty;
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
                connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            }
            else
            {
                connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            }

            builder.Services.AddDbContext<TaxDbContext>(options =>
                options.UseSqlServer(connection));


            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.MapControllers();

            app.MapGet("/TaxRecord", (TaxDbContext context) =>
            {
                return context.TaxRates.ToList();
            })
            .WithName("GetTaxRecords")
            .WithOpenApi();

            //app.MapPost("/Person", (Person person, PersonDbContext context) =>
            //{
            //    context.Add(person);
            //    context.SaveChanges();
            //})
            //.WithName("CreatePerson")
            //.WithOpenApi();




            app.Run();
        }

        public class TaxRates
        {
            [Key]
            public string ZipCode { get; set; }
            public decimal Rate { get; set; }
        }

        public class TaxDbContext : DbContext
        {
            public TaxDbContext(DbContextOptions<TaxDbContext> options)
                : base(options)
            {
            }

            public DbSet<TaxRates> TaxRates { get; set; }
        }
    }
}
