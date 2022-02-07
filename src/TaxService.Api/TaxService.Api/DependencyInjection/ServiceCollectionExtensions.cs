using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxService.Api.RemoteServices.TaxJar;
using Refit;
using System;
using TaxService.Api.Models;

namespace TaxService.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRemoteServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddRefitClient<ISalesTaxApi>().ConfigureHttpClient(
                c =>
                {
                    var restfulServiceOptions = new RestfulServicesOptions();
                    configuration.GetSection(RestfulServicesOptions.RestfulServicesConfiguration).Bind(restfulServiceOptions);
                    c.BaseAddress = new Uri(restfulServiceOptions.BaseAddress);
                    c.Timeout = restfulServiceOptions.Timeout;
                });

            return services;
        }
    }
}
