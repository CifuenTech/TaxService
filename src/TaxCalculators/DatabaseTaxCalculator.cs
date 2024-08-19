using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;

namespace TaxService.Api.TaxCalculators
{
    public class DatabaseTaxCalculator : ITaxCalculator
    {
        public Task<OrderTaxes> CalculateTaxesAsync(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task<TaxRate> GetTaxRateAsync(string zipCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
