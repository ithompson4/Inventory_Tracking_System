using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Data.SqlClient;
using AutoMapper;
using InventoryTrackingSystem.Models;

namespace InventoryTrackingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Sets our Initializer to InventoryTrackingSystemInitializer class
            Database.SetInitializer<ApplicationDbContext>(new InventoryTrackingSystem.Models.InventoryTrackingSystemInitializer());
            
            // Force call to Seed method
            ApplicationDbContext db = new ApplicationDbContext();
            //db.Database.Initialize(true);

            // Mappers
            Mapper.CreateMap<Models.Product, ViewModels.ProductList>();
            Mapper.CreateMap<Models.Product, ViewModels.ProductDetails>();
            Mapper.CreateMap<Models.Product, ViewModels.ProductDetailsForUser>();
            Mapper.CreateMap<Models.Product, ViewModels.ProductEdit>();
            Mapper.CreateMap<Models.UserProfile, ViewModels.UserProfileList>();
            Mapper.CreateMap<Models.UserProfile, ViewModels.UserProfileDetails>();
            Mapper.CreateMap<Models.ShoppingCart, ViewModels.VM_ShoppingCart>();
        }
    }
}
