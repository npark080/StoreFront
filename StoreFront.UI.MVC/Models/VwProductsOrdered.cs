using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class VwProductsOrdered
    {
        public string ProductName { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
