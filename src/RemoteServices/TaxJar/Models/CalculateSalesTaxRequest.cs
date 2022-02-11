using System.Collections.Generic;

namespace TaxService.Api.RemoteServices.TaxJar.Models
{
    public class CalculateSalesTaxRequest
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public float amount { get; set; }
        public float shipping { get; set; }
        public List<NexusAddress> nexus_addresses { get; set; }
        public List<RequestLineItem> line_items { get; set; }
    }
    public class NexusAddress
    {
        public string id { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }

    public class RequestLineItem
    {
        public string id { get; set; }
        public int quantity { get; set; }
        public string product_tax_code { get; set; }
        public float unit_price { get; set; }
        public float discount { get; set; }
    }
}