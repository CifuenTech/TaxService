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
        private readonly ILogger<TaxCalculationController> _logger;

        public TaxCalculationController(ITaxCalculator taxCalculator, ILogger<TaxCalculationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Customer GetRates(Address address)
        {
            var taxCalculator = 
            return new Customer();
        }
    }
}
