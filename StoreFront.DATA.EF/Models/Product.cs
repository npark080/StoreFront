using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ProductDescription { get; set; }
        public short UnitsInStock { get; set; }
        public string Season { get; set; } = null!;
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string? ImageName { get; set; }

        public virtual Category? Category { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
