namespace TaxService.Api.Controllers.DataContracts
{
    public class TaxRates
    {
        public string Zip { get; set; }
        public decimal CombinedRate { get; set; }
    }
}
