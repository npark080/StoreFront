﻿using System;
using System.Collections.Generic;

namespace StoreFront.UI.MVC.Models
{
    public partial class OrderProduct
    {
        public int OrderProductId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public short? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
