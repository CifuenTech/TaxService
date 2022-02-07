using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;

namespace TaxService.Api.Models
{
    public interface ITaxCalculator
    {
        public Task<IEnumerable<TaxRates>> GetTaxRates(Address address);
        public Task<OrderTaxes> CalculateTaxes(Customer customer);
    }
}
