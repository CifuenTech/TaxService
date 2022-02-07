using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;

namespace TaxService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TaxCalculationController : ControllerBase
    {
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;
        private readonly ILogger<TaxCalculationController> _logger;

        public TaxCalculationController(ITaxCalculatorFactory taxCalculatorFactory, ILogger<TaxCalculationController> logger)
        {
            _taxCalculatorFactory = taxCalculatorFactory; 
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<TaxRates>> GetTaxRates(string customerType, Address address)
        {
            var taxCalculator = _taxCalculatorFactory.CreateTaxCalculator(customerType);
            var rates = taxCalculator.GetTaxRates(address);
            
            return rates;
        }
    }
}
