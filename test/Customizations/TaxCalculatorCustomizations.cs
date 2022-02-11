using AutoFixture;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using TaxService.Api.Controllers.DataContracts;
using TaxService.Api.Models;
using TaxService.Api.TaxCalculators;

namespace TaxService.Api.Tests.Customizations
{
    class SuccessGetTaxRateCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var taxRate = fixture.Create<TaxRate>();

                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.GetTaxRateAsync(Arg.Any<string>()).Returns(taxRate);

                return taxCalculator;
            });

        }
    }

    public class ArgumentExceptionGetTaxRateCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.WhenForAnyArgs(x => x.GetTaxRateAsync(Arg.Any<string>()))
                    .Do(x => { throw new ArgumentException(); });

                return taxCalculator;
            });

        }
    }

    public class RemoteExceptionGetTaxRateCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.WhenForAnyArgs(x => x.GetTaxRateAsync(Arg.Any<string>()))
                    .Do(x => { throw new RemoteException(); });

                return taxCalculator;
            });
        }
    }

    class SuccessCalculateTaxesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {

            fixture.Register(() =>
            {
                var orderTaxes = fixture.Create<OrderTaxes>();

                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.CalculateTaxesAsync(Arg.Any<Order>()).Returns(orderTaxes);

                return taxCalculator;
            });
        }
    }

    public class ArgumentExceptionCalculateTaxesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.WhenForAnyArgs(x => x.CalculateTaxesAsync(Arg.Any<Order>()))
                    .Do(x => { throw new ArgumentException(); });

                return taxCalculator;
            });
        }
    }

    public class RemoteExceptionCalculateTaxesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() =>
            {
                var taxCalculator = Substitute.For<ITaxCalculator>();
                taxCalculator.WhenForAnyArgs(x => x.CalculateTaxesAsync(Arg.Any<Order>()))
                    .Do(x => { throw new RemoteException(); });

                return taxCalculator;
            });
        }
    }
}
