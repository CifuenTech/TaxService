using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Api.RemoteServices.TaxJar.DataContracts;

namespace TaxService.Api.RemoteServices.TaxJar
{
    public interface ISalesTaxApi
    {
        [Post("/v2/rates")]
        Task<GetTaxRatesResponse> GetTaxRatesForLocation(GetTaxRatesRequest request);

        [Post("/v2/taxes")]
        Task<CalculateSalesTaxResponse> CalculateSalesTax(CalculateSalesTaxRequest request);
    }
}
