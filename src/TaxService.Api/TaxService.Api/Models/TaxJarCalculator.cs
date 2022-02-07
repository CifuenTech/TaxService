using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.RemoteServices.TaxJar;
using TaxService.Api.RemoteServices.TaxJar.DataContracts;
using System.Linq;

namespace TaxService.Api.Models
{
    public class TaxJarCalculator : ITaxCalculator
    {
        private readonly ISalesTaxApi _salesTaxApi;

        public TaxJarCalculator(ISalesTaxApi salesTaxApi)
        {
            _salesTaxApi = salesTaxApi;
        }

        public async Task<OrderTaxes> CalculateTaxes(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Customer is a required field.");

            var request = new CalculateSalesTaxRequest
            {

            };

            var response = await _salesTaxApi.CalculateSalesTax(request);

            var salesTax = ParseOrderTaxes(response);

            return salesTax;
        }

        public async Task<IEnumerable<TaxRates>> GetTaxRates(Address address)
        {
            var request = new GetTaxRatesRequest
            {

            };

            var response = await _salesTaxApi.GetTaxRatesForLocation(request);

            var taxRates = ParseTaxRates(response);

            return taxRates;
        }

        private OrderTaxes ParseOrderTaxes(CalculateSalesTaxResponse response)
        {
            var orderTaxes = new OrderTaxes
            {
                TotalTax = (decimal)response.tax.amount_to_collect
            };

            return orderTaxes;
        }

        private IEnumerable<TaxRates> ParseTaxRates(GetTaxRatesResponse response)
        {
            var taxRates = response.rates.Select(
                r => new TaxRates { Zip = r.zip, CombinedRate = (decimal)r.combined_rate });

            return taxRates;
        }
    }
}
