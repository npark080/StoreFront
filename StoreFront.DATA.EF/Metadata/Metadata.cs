using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models//.Metadata
{
    public class CategoryMetadata 
    {
        [Required(ErrorMessage = "*Please enter a name for the category")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = null!;

        [DisplayFormat(NullDisplayText = "No description")]
        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        public string? CategoryDescription { get; set; }
    }

    public class OrderMetadata 
    {
        [Display(Name = "User")]
        public string UserId { get; set; } = null!;

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Recipient")]
        [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
        [DisplayFormat(NullDisplayText = "Not Given")]
        public string? ShipToName { get; set; }

        [Display(Name = "Region")]
        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DisplayFormat(NullDisplayText = "Not Given")]
        public string? ShipRegion { get; set; }

        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        [DisplayFormat(NullDisplayText = "Not Given")]
        public string? ShipCity { get; set; }

        [Display(Name = "State")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "*Please enter state abbreviation")]
        [DisplayFormat(NullDisplayText = "Not Given")]
        public string? ShipState { get; set; }

        [Display(Name = "Zip")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "*Please enter 5 digit zip code")]
        [DisplayFormat(NullDisplayText = "Not Given")]
        [DataType(DataType.PostalCode)]
        public string? ShipZip { get; set; }
    }

    public class OrderDetailMetadata 
    {
        [Key]

        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        public short? Quantity { get; set; }

        [Required(ErrorMessage = "*Please enter a price for this order")]
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
    }

    public class ProductMetadata 
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "*Please enter a name for the Product")]
        [Display(Name = "Product")]
        [StringLength(200, ErrorMessage = "*Cannot exceed 200 characters")]
        public string ProductName { get; set; } = null!;

        [Required]
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "No description")]
        public string? ProductDescription { get; set; }

        [Required(ErrorMessage = "*Please enter the quantity currently in stock")]
        [Display(Name = "In Stock")]
        [Range(0, short.MaxValue)]
        public short UnitsInStock { get; set; }

        [Required(ErrorMessage = "*Please enter the season the product is needed for")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        public string Season { get; set; } = null!;

        [Required(ErrorMessage = "*Please enter the category the product falls under")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [StringLength(75, ErrorMessage = "*Cannot exceed 75 characters")]
        [Display(Name = "Image")]
        public string? ImageName { get; set; }
    }

    public class SupplierMetadata 
    {
        [Key]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "*Please enter the Supplier Name")]
        [Display(Name = "Supplier")]
        [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
        public string SupplierName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Region { get; set; }

        [StringLength(150, ErrorMessage = "*Cannot exceed 150 characters")]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? City { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "*Please enter state abbreviation")]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? State { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "*Please enter 5 digit zip code")]
        [DataType(DataType.PostalCode)]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Zip { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Phone { get; set; }
    }

    public class UserMetadata 
    {
        public string UserID { get; set; } = null!;

        [Required(ErrorMessage = "*Please enter your first name")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "*Please enter your last name")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
        public string LastName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
        public string? Region { get; set; }

        [StringLength(150, ErrorMessage = "*Cannot exceed 150 characters")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
        public string? City { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "*Please enter state abbreviation")]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? State { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "*Please enter 5 digit zip code")]
        [DataType(DataType.PostalCode)]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Zip { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(NullDisplayText = "Not given")]
        public string? Phone { get; set; }
    }
}
