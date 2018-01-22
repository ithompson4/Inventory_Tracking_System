using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace InventoryTrackingSystem.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Order Id")]
        public int OrderID { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DisplayName("Order date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Order Completed")]
        public bool OrderCompleted { get; set; }

        public List<ProductsOrdered> ProductsOrdered { get; set; }

        // Shipping address
        [Required]
        [DisplayName("Unit #")]
        [StringLength(5, ErrorMessage = "Unit number should not be less than 5 characters")]
        public string UnitNumber { get; set; }
        [Required]
        [DisplayName("Address")]
        [StringLength(20, ErrorMessage = "Address number should not be less than 20 characters")]
        public string Address { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("Province")]
        public string Province { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        [RegularExpression(@"^[A-Za-z][0-9][A-Za-z][0-9][A-Za-z][0-9]$", ErrorMessage = "Postal code should be in A1A1A1 format")]
        [StringLength(6)]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Phone #")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number should be 10 digits")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        // Payment information
        [Required]
        [DisplayName("Name on card")]
        public string NameOnCard { get; set; }
        [Required]
        [DisplayName("Card Number")]
        [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Card number should be 16 digits")]
        [StringLength(16)]
        public string CardNumber { get; set; }
        [Required]
        [DisplayName("Expiry Date")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Expiry date should be in MMYY format")]
        [StringLength(4)]
        public string ExpiryDate { get; set; }
        [Required]
        [DisplayName("CID")]
        [RegularExpression(@"^[0-9]{3}$", ErrorMessage = "CID code should be 3 digits")]
        [StringLength(3)]
        public string CID { get; set; }

        public Order()
        {
            ProductsOrdered = new List<ProductsOrdered>();
        }
    }

    public class ProductsOrdered
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int QtyToAdd { get; set; }

        public ProductsOrdered() { }

        public ProductsOrdered(int orderId, int prodId, int quan)
        {
            ProductID = prodId;
            OrderID = orderId;
            QtyToAdd = quan;
        }
    }

    public class OrderDetail
    {
        public Order order;

        public List<Product> ProductsOrdered;

        public OrderDetail()
        {
            ProductsOrdered = new List<Product>();
        }

    }

}