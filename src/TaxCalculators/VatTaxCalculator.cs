using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;

namespace TaxService.Api.TaxCalculators
{
    //Sample alternative calculator based on Value Added Tax
    public class VatTaxCalculator : ITaxCalculator
    {
        public async Task<OrderTaxes> CalculateTaxesAsync(Order order)
        {
            var vatTax = (order.OrderAmount * 0.10m);

            var orderTaxes = new OrderTaxes
            {
                TotalTax = vatTax
            };

            return orderTaxes;
        }

        public async Task<TaxRate> GetTaxRateAsync(string zipCode)
        {
            var taxRate = new TaxRate
            {
                CombinedRate = 0.10m,
                ZipCode = zipCode
            };

            return taxRate;
        }
    }
}
