﻿namespace TaxService.Api.Controllers.DataContracts
{
    public class OrderTaxes
    {
        public decimal TotalTax { get; set; }
        public decimal Rate { get; set; }
    }
}
