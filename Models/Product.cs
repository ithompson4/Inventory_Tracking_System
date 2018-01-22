using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace InventoryTrackingSystem.Models
{
    // Product
    public class Product
    {
        public Product()
        {

        }

        public Product(int pid, string pname, string desc, double p, int q)
        {
            this.productId = pid;
            this.productName = pname;
            this.description = desc;
            this.price = p;
            this.quantity = q;
        }

        [Key]
        public int productId { get; set; }
        
        [Required]
        [Display(Name = "Product Name")]
        [RegularExpression(@"[\w\-\(\)\s]*", ErrorMessage = "Only these special characters are allowed: -,(,)")]
        public string productName { get; set; }

        [Display(Name = "")]
        public string imgFile { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(0.009, double.MaxValue, ErrorMessage="Price must be greater than 0")]
        public double price { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue, ErrorMessage="Quantity must be greater than zero")]
        public int quantity { get; set; }

        public int qtyToAdd { get; set; }
    }
}