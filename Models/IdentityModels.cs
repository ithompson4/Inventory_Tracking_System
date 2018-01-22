using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace InventoryTrackingSystem.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("InventoryTrackingSystemDatabaseConnection")
        {
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<AdminProfile> AdminProfiles { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<InventoryTrackingSystem.ViewModels.VM_ShoppingCart> VM_ShoppingCart { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsOrdered> ProductsOrdered { get; set; }
    }

    //--------------
    // Users
    //--------------

    // All Users inherit from ApplicationUsers( : IdentityUser) which contains the Users unique id key
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public ApplicationUser(string username)
            : base(username)
        {
            this.ApplicationUserId = username;
        }
        [Key]
        public string ApplicationUserId { get; set; }

        public string UserEmail { get; set; }
        public bool ConfirmedEmail { get; set; }
    }

    // Admin
    public class AdminProfile : ApplicationUser
    {
        public AdminProfile()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
        }
        public AdminProfile(string username, string fname, string lname, string email)
            : base(username)
        {
            this.AdminProfileId = username;
            this.FirstName = fname;
            this.LastName = lname;
            this.Email = email;
        }

        [Key]
        public string AdminProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }

    // User
    public class UserProfile : ApplicationUser
    {
        public UserProfile()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
        }
        public UserProfile(string username, string fname, string lname, string address,
            string city, string phone, string email) : base(username)
        {
            this.FirstName = fname;
            this.LastName = lname;
            this.Address = address;
            this.City = city;
            this.Phone = phone;
            this.Email = email;
        }

        [Key]
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Cart Cart { get; set; }
    }
}