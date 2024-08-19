using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Api.Controllers;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;
using TaxService.Api.RemoteServices.TaxJar;
using TaxService.Api.RemoteServices.TaxJar.Models;
using TaxService.Api.TaxCalculators;
using Xunit;

namespace TaxService.Api.Tests
{
    public class TaxJarCalculatorTests
    {
        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task GetTaxRateAsyncWhenValidParametersShouldReturnTaxRate()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization.
            var response = fixture.Create<GetTaxRatesResponse>();

            var salesTaxApi = Substitute.For<ISalesTaxApi>();
            salesTaxApi.GetTaxRatesForLocation(Arg.Any<GetTaxRatesRequest>()).Returns(response);

            var sut = new TaxJarTaxCalculator(salesTaxApi);

            //Act
            var act = await sut.GetTaxRateAsync("33023");

            //Assert
            act.Should().BeOfType<TaxRate>();
            act.Should().NotBeNull();
            act.CombinedRate.Should().Be((decimal)response.rate.combined_rate);
            act.ZipCode.Should().Be(response.rate.zip);
        }

        [Fact]
        [Trait("Category", "Unit Test")]
        public async Task CalculateTaxesAsyncWhenValidParametersShouldReturnOrderTaxes()
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CompositeCustomization(new AutoNSubstituteCustomization()));
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

            //TODO Put this in a customization
            var order = fixture.Create<Order>();
            var response = fixture.Create<CalculateSalesTaxResponse>();

            var salesTaxApi = Substitute.For<ISalesTaxApi>();
            salesTaxApi.CalculateSalesTax(Arg.Any<CalculateSalesTaxRequest>()).Returns(response);

            var sut = new TaxJarTaxCalculator(salesTaxApi);

            //Act
            var act = await sut.CalculateTaxesAsync(order);

            //Assert
            act.Should().BeOfType<OrderTaxes>();
            act.Should().NotBeNull();
            act.Rate.Should().Be((decimal)response.tax.rate);
            act.TotalTax.Should().Be((decimal)response.tax.amount_to_collect);
        }
    }
}
