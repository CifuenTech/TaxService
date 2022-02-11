using System.Collections.Generic;

namespace TaxService.Api.RemoteServices.TaxJar.Models
{
    public class CalculateSalesTaxResponse
    {
        public Tax tax { get; set; }
    }

    public class Tax
    {
        public float order_total_amount { get; set; }
        public float shipping { get; set; }
        public float taxable_amount { get; set; }
        public float amount_to_collect { get; set; }
        public float rate { get; set; }
        public bool has_nexus { get; set; }
        public bool freight_taxable { get; set; }
        public string tax_source { get; set; }
        public Jurisdictions jurisdictions { get; set; }
        public Breakdown breakdown { get; set; }
    }

    public class Jurisdictions
    {
        public string country { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string city { get; set; }
    }

    public class Breakdown
    {
        public float taxable_amount { get; set; }
        public float tax_collectable { get; set; }
        public float combined_tax_rate { get; set; }
        public float state_taxable_amount { get; set; }
        public float state_tax_rate { get; set; }
        public float state_tax_collectable { get; set; }
        public float county_taxable_amount { get; set; }
        public float county_tax_rate { get; set; }
        public float county_tax_collectable { get; set; }
        public float city_taxable_amount { get; set; }
        public float city_tax_rate { get; set; }
        public float city_tax_collectable { get; set; }
        public float special_district_taxable_amount { get; set; }
        public float special_tax_rate { get; set; }
        public float special_district_tax_collectable { get; set; }
        public List<ResponseLineItem> line_items { get; set; }
    }

    public class ResponseLineItem
    {
        //public string id { get; set; }
        public float taxable_amount { get; set; }
        public float tax_collectable { get; set; }
        public float combined_tax_rate { get; set; }
        public float state_taxable_amount { get; set; }
        public float state_sales_tax_rate { get; set; }
        public float state_amount { get; set; }
        public float county_taxable_amount { get; set; }
        public float county_tax_rate { get; set; }
        public float county_amount { get; set; }
        public float city_taxable_amount { get; set; }
        public float city_tax_rate { get; set; }
        public float city_amount { get; set; }
        public float special_district_taxable_amount { get; set; }
        public float special_tax_rate { get; set; }
        public float special_district_amount { get; set; }
    }
}
