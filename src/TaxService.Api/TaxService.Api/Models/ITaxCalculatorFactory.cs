namespace TaxService.Api.Models
{
    public interface ITaxCalculatorFactory
    {
        public ITaxCalculator CreateTaxCalculator(string CustomerType);
    }
}
