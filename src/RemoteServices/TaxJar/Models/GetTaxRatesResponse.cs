using System.Collections.Generic;

namespace TaxService.Api.RemoteServices.TaxJar.Models
{
    public class GetTaxRatesResponse
    {
        public Rate rate { get; set; }
    }

    public class Rate
    {
        public string zip { get; set; }
        public string country { get; set; }
        public float country_rate { get; set; }
        public string state { get; set; }
        public float state_rate { get; set; }
        public string county { get; set; }
        public float county_rate { get; set; }
        public string city { get; set; }
        public float city_rate { get; set; }
        public float combined_district_rate { get; set; }
        public float combined_rate { get; set; }
        public bool freight_taxable { get; set; }
    }
}
