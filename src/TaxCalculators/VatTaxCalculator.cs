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
        public async Task<OrderTaxes> CalculateTaxes(Order order)
        {
            var vatTax = (order.OrderAmount * 0.10m);

            var orderTaxes = new OrderTaxes
            {
                TotalTax = vatTax
            };

            return orderTaxes;
        }

        public async Task<TaxRate> GetTaxRate(string zipCode)
        {
            var taxRate = new TaxRate
            {
                CombinedRate = 0.10m,
                Zip = zipCode
            };

            return taxRate;
        }
    }
}
