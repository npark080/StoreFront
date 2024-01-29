using System.ComponentModel.DataAnnotations;

namespace StoreFront.UI.MVC.Models
{
    public class CheckoutViewModel
    {
        [StringLength(100)]
        [Display(Name = "Recipient")]
        public string ShipToName { get; set; } = null!;

        [StringLength(500)]
        [Display(Name = "Region")]
        public string? ShipRegion { get; set; }

        [StringLength(150)]
        [Display(Name = "Address")]
        public string Address { get; set; } = null!;

        [StringLength(50)]
        [Display(Name = "City")]
        public string ShipCity { get; set; } = null!;

        [StringLength(2, MinimumLength = 2)]
        [Display(Name = "State")]
        public string? ShipState { get; set; }

        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string ShipZip { get; set; } = null!;

    }
}
