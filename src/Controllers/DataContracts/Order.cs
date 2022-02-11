using System.ComponentModel.DataAnnotations;

namespace TaxService.Api.Controllers.DataContracts
{
    public class Order
    {
        [Required]
        public decimal OrderAmount { get; set; }
        [Required]
        public decimal ShippingAmount { get; set; }
        [Required]
        public Address FromAddress { get; set; }
        [Required]
        public Address ToAddress { get; set; }
    }
}
