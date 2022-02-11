namespace TaxService.Api.RemoteServices.TaxJar.Models
{
    public class GetTaxRatesRequest
    {
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }
}
