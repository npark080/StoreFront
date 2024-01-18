using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string? ShipToName { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipState { get; set; }
        public string? ShipZip { get; set; }

        public virtual OrderDetail OrderNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
