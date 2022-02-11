using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxService.Api.RemoteServices.TaxJar;
using Refit;
using System;
using TaxService.Api.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TaxService.Api.TaxCalculators;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRemoteServices(this IServiceCollection services, IConfiguration configuration)
        {
            var refitSettings = new Refit.RefitSettings
            { ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()) };

            services.AddRefitClient<ISalesTaxApi>(refitSettings)
                .ConfigureHttpClient(
                c =>
                {
                    var taxJarSettings = new RestfulServicesOptions();
                    configuration.GetSection("RestfulServicesConfiguration:TaxJarSalesTaxApi").Bind(taxJarSettings);
                    c.BaseAddress = new Uri(taxJarSettings.BaseAddress);
                    c.Timeout = taxJarSettings.Timeout;
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", taxJarSettings.ApiKey);
                });

            return services;
        }

        public static IServiceCollection AddTaxCalculators(this IServiceCollection services)
        {
            //Use reflection to register all implementations of ITaxCalculator automatically without changing 
            var taxCalculators = GetImplementions(typeof(ITaxCalculator), "TaxCalculator");
            foreach (var type in taxCalculators)
            {
                services.TryAddTransient(type.Value);
            }

            //Register the corresponding implementation of ITaxCalculator based on "customer" route paramter
            services.AddScoped<ITaxCalculator>(sp =>
            {
                var context = sp.GetService<IHttpContextAccessor>().HttpContext;
                var customerType = ((string)context.Request.RouteValues["customerTaxType"]).ToLower();

                var taxCalculatorType = taxCalculators.FirstOrDefault(ccp => customerType == ccp.Key);

                var instance = (taxCalculatorType.Value != null) ?
                    (ITaxCalculator)sp.GetService(taxCalculatorType.Value) : (ITaxCalculator)sp.GetService(typeof(DefaultTaxCalculator)); //TODO Implement guard instead of default;

                return instance;
            });

            return services;
        }

        private static Dictionary<string, Type> GetImplementions(Type interfaceType, string classSuffix)
        {
            //TODO Put this in a singleton
            var derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p != interfaceType)
                .ToDictionary(t => t.Name.Replace(classSuffix, "").ToLower());

            return derivedTypes;
        }
    }
}
