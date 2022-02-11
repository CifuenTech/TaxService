using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;

namespace TaxService.Api.TaxCalculators
{
    public class DefaultTaxCalculator : ITaxCalculator
    {
        public Task<OrderTaxes> CalculateTaxesAsync(Order order)
        {
            throw new ArgumentException("CustomerTaxType provided is not supported or not configured properly.");
        }

        public Task<TaxRate> GetTaxRateAsync(string zipCode)
        {
            throw new ArgumentException("CustomerTaxType provided is not supported or not configured properly.");
        }
    }
}
