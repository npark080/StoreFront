using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order 
    {
        [NotMapped]
        public decimal? Total => OrderDetails.Sum(x => x.ItemTotal);
    }
    
    [ModelMetadataType(typeof(OrderDetailMetadata))]
    public partial class OrderDetail 
    {
        [NotMapped]
        [Display(Name = "ItemTotal")]
        public decimal? ItemTotal => Quantity * Price;
    }

    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier { }

    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User 
    {
        [NotMapped]
        [Display(Name = "Full Name")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
