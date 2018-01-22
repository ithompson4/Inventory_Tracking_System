using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryTrackingSystem.Models;

namespace InventoryTrackingSystem.ViewModels
{
    public class RepositoryBase
    {
        protected ApplicationDbContext context;

        public RepositoryBase()
        {
            context = new ApplicationDbContext();
            //context.Database.Initialize(true);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            context.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            context.Configuration.LazyLoadingEnabled = false;
        }
    }
}