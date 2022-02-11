namespace TaxService.Api.Controllers.DataContracts
{
    public class TaxRate
    {
        public string Zip { get; set; }
        public decimal CombinedRate { get; set; }
    }
}
