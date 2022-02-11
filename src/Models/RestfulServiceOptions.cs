using System;

namespace TaxService.Api.Models
{
    public class RestfulServicesOptions
    {
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    }
}
