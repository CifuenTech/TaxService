using System;

namespace TaxService.Api.Models
{
    public class RestfulServicesOptions
    {
        public const string RestfulServicesConfiguration = "RestfulServicesConfiguration";
        public string BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    }
}
