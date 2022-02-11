using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;

namespace TaxService.Api.TaxCalculators
{
    public interface ITaxCalculator
    {
        public Task<TaxRate> GetTaxRateAsync(string zipCode);
        public Task<OrderTaxes> CalculateTaxesAsync(Order order);
    }
}
