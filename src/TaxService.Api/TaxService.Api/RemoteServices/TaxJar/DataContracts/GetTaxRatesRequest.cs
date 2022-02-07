namespace TaxService.Api.RemoteServices.TaxJar.DataContracts
{
    public class GetTaxRatesRequest
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }
}
