using System;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.RemoteServices.TaxJar;

namespace TaxService.Api.Models
{
    public class TaxCalculatorFactory : ITaxCalculatorFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TaxCalculatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ITaxCalculator CreateTaxCalculator(string CustomerType)
        {
            switch (CustomerType)
            {
                case "TaxJar":
                    return (ITaxCalculator)serviceProvider.GetService(typeof(TaxJarCalculator));
                default:
                    throw new Exception("Invalid CustomerType.  Possible Values : { \"TaxJar\" }");

            }
        }
    }
}
