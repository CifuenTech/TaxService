using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;
using TaxService.Api.TaxCalculators;

namespace TaxService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/{customerTaxType}")]
    [Produces("application/json")]
    public class TaxCalculationController : ControllerBase
    {
        private readonly ITaxCalculator _taxCalculator;
        private readonly ILogger<TaxCalculationController> _logger;

        public TaxCalculationController(ILogger<TaxCalculationController> logger, ITaxCalculator taxCalculator)
        {
            _logger = logger;
            _taxCalculator = taxCalculator;
        }

        [HttpGet("rates/{zipCode}")]
        [ProducesResponseType(typeof(TaxRate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaxRateAsync(string zipCode)
        {
            try
            {
                var rates = await _taxCalculator.GetTaxRateAsync(zipCode);

                return Ok(rates);
            }
            catch (RemoteException ex)
            {
                //Stack trace for remote errors logged internally and not returned to client for security purposes
                _logger.LogError(ex, ex.Message);
                //TODO Handle via middlewear, map type to ProblemDetail, and return stacktrace as detail if running on Development environment
                return UnprocessableEntity(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: ex.Message);
            }
        }

        [HttpPost("taxes")]
        [ProducesResponseType(typeof(OrderTaxes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CalculateTaxesAsync(Order order)
        {
            try
            {
                var orderTaxes = await _taxCalculator.CalculateTaxesAsync(order);

                return Ok(orderTaxes);
            }
            catch (RemoteException ex)
            {
                _logger.LogError(ex, ex.Message);
                return UnprocessableEntity(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: ex.Message);
            }
        }

        [HttpGet("ping")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PingAsync()
        {
            if (_taxCalculator == null)
                return BadRequest("CustomerTaxType provided is not supported or not configured properly.");//TODO Implement as a guard

            return Ok("TaxService is up." );
        }
    }
}
