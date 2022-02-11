//using System;
//using TaxService.Api.Controllers.DataContracts;
//using TaxService.Api.RemoteServices.TaxJar;
//using TaxService.Api.TaxCalculators;

//namespace TaxService.Api.TaxCalculators
//{
//    public class TaxCalculatorFactory : ITaxCalculatorFactory
//    {
//        private readonly IServiceProvider serviceProvider;

//        public TaxCalculatorFactory(IServiceProvider serviceProvider)
//        {
//            this.serviceProvider = serviceProvider;
//        }
//        public ITaxCalculator CreateTaxCalculator(string customerType)
//        {
//            switch (customerType)
//            {
//                case "NationalRetailer":
//                    var taxJarCalculator = (ITaxCalculator)serviceProvider.GetService(typeof(TaxJarTaxCalculator));
//                    return taxJarCalculator;
//                case "InternationalSupplier":
//                    var vatCalculator = (ITaxCalculator)serviceProvider.GetService(typeof(VatTaxCalculator));
//                    return vatCalculator;
//                default:
//                    throw new Exception("Invalid CustomerType.");

//            }
//        }
//    }
//}
