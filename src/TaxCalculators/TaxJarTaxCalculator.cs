using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.RemoteServices.TaxJar;
using TaxService.Api.RemoteServices.TaxJar.Models;
using System.Linq;
using Refit;
using TaxService.Api.Models;

namespace TaxService.Api.TaxCalculators
{
    public class TaxJarTaxCalculator : ITaxCalculator
    {
        private readonly ISalesTaxApi _salesTaxApi;

        public TaxJarTaxCalculator(ISalesTaxApi salesTaxApi)
        {
            _salesTaxApi = salesTaxApi;
        }

        public async Task<TaxRate> GetTaxRateAsync(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new ArgumentException("ZipCode is a required field.");

            var request = new GetTaxRatesRequest
            {
                zip = zipCode
            };

            GetTaxRatesResponse response = null;

            try
            {
                response = await _salesTaxApi.GetTaxRatesForLocation(request);
            }
            catch (ApiException ex)
            {
                throw new RemoteException("Could not retrieve tax rate.", ex);
            }

            if (response == null || response.rate == null)
                throw new RemoteException("Could not retrieve tax rate.");

            var taxRate = ParseTaxRates(response.rate);

            return taxRate;
        }

        public async Task<OrderTaxes> CalculateTaxesAsync(Order order)
        {
            if (order == null)
                throw new ArgumentException("Order is a required field.");

            var request = new CalculateSalesTaxRequest
            {
                amount = (float)order.OrderAmount,
                shipping = (float)order.ShippingAmount,
                from_country = order.FromAddress.Country,
                from_state = order.FromAddress.State,
                from_zip = order.FromAddress.ZipCode,
                to_country = order.ToAddress.Country,
                to_state = order.ToAddress.State,
                to_zip = order.ToAddress.ZipCode
            };

            CalculateSalesTaxResponse response = null;

            try
            {
                response = await _salesTaxApi.CalculateSalesTax(request);
            }
            catch (ApiException ex)
            {
                throw new RemoteException("Could not calculate taxes for order.", ex);
            }

            if (response == null || response.tax == null)
                throw new RemoteException("Could not calculate taxes for order.");

            var salesTax = ParseOrderTaxes(response.tax);

            return salesTax;
        }

        private TaxRate ParseTaxRates(Rate rate)
        {
            var taxRate = new TaxRate { ZipCode = rate.zip, CombinedRate = (decimal)rate.combined_rate };
            return taxRate;
        }

        private OrderTaxes ParseOrderTaxes(Tax tax)
        {
            var orderTaxes = new OrderTaxes
            {
                TotalTax = (decimal)tax.amount_to_collect,
                Rate = (decimal)tax.rate
            };

            return orderTaxes;
        }
    }
}
