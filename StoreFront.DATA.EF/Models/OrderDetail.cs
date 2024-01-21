using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public short? Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
