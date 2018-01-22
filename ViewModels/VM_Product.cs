using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryTrackingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryTrackingSystem.ViewModels
{
    public class ProductList
    {
        public int productId { get; set; }

        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Display(Name = "Price")]
        public double price { get; set; }
    }

    public class ProductDetails : ProductList
    {
        [Display(Name = "")]
        public string imgFile { get; set; }        
        
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Qty on hand")]
        public int quantity { get; set; }
    }

    public class ProductDetailsForUser : ProductList
    {
        [Display(Name = "")]
        public string imgFile { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }

    public class ProductEdit
    {
        public int productId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [RegularExpression(@"[\w\-\(\)\s]*", ErrorMessage = "Only these special characters are allowed: -,(,)")]
        public string productName { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(0.009, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double price { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
    }
}