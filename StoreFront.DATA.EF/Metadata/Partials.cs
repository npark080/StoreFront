using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF//.Metadata
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }
    
    [ModelMetadataType(typeof(OrderDetailMetadata))]
    public partial class OrderDetail { }

    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier { }

    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User { }
}
