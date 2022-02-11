using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Xunit;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Api.Controllers;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;
using TaxService.Api.TaxCalculators;
using TaxService.Api.Tests.Customizations;

namespace TaxService.Api.Tests
{
    public class TaxCalculationControllerTests
    {
        [Fact]
        [Trait("Category","Unit Test")]
        public async Task GetTaxRateWhenValidParametersShouldReturnStatusOkAndTaxRate()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new SuccessGetTaxRateCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.GetTaxRateAsync("33023");

            //Assert
            act.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)act).StatusCode.Should().Be(200);
            ((OkObjectResult)act).Value.Should().BeOfType<TaxRate>();
            ((OkObjectResult)act).Value.Should().NotBeNull();
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task GetTaxRateWhenArgumentExceptionShouldReturnBadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new ArgumentExceptionGetTaxRateCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.GetTaxRateAsync("33023");

            //Assert
            act.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)act).StatusCode.Should().Be(400);
        }


        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task GetTaxRateWhenRemoteExceptionShouldReturnUnprocessableEntity()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new RemoteExceptionGetTaxRateCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.GetTaxRateAsync("33023");

            //Assert
            act.Should().BeOfType<UnprocessableEntityObjectResult>();
            ((UnprocessableEntityObjectResult)act).StatusCode.Should().Be(422);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task CalculateTaxesAsyncWhenValidParametersShouldReturnStatusOkAndTaxRate()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new SuccessCalculateTaxesCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var order = fixture.Create<Order>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.CalculateTaxesAsync(order);

            //Assert
            act.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)act).StatusCode.Should().Be(200);
            ((OkObjectResult)act).Value.Should().BeOfType<OrderTaxes>();
            ((OkObjectResult)act).Value.Should().NotBeNull();
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task CalculateTaxesAsyncWhenArgumentExceptionShouldReturnBadRequest()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new ArgumentExceptionCalculateTaxesCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var order = fixture.Create<Order>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.CalculateTaxesAsync(order);

            //Assert
            act.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)act).StatusCode.Should().Be(400);
        }


        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task CalculateTaxesAsyncWhenRemoteExceptionShouldReturnUnprocessableEntity()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization(), new RemoteExceptionCalculateTaxesCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var logger = fixture.Freeze<ILogger<TaxCalculationController>>();
            var order = fixture.Create<Order>();
            var taxCalculator = fixture.Create<ITaxCalculator>();

            var sut = new TaxCalculationController(logger, taxCalculator);

            //Act
            var act = await sut.CalculateTaxesAsync(order);

            //Assert
            act.Should().BeOfType<UnprocessableEntityObjectResult>();
            ((UnprocessableEntityObjectResult)act).StatusCode.Should().Be(422);
        }
    }
}

