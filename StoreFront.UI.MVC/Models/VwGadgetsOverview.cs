using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class VwGadgetsOverview
    {
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public string CategoryName { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
    }
}
