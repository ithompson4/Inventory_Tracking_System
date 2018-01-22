using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; // required for DropCreateDatabaseAlways/DropCreateDatabaseIfModelChanges
using Microsoft.AspNet.Identity; // required for <ApplicationUser> <IdentityRole>
using Microsoft.AspNet.Identity.EntityFramework; // required for UserStore, RoleStore
using InventoryTrackingSystem.ViewModels;

namespace InventoryTrackingSystem.Models
{
    public class InventoryTrackingSystemInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    //public class InventoryTrackingSystemInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            FirstTimeClassInitializer(context);
            base.Seed(context);
        }

        /// --------------------------------------------------------------------
        /// <summary> 
        /// Initialize admin user
        /// </summary>
        /// <param name="context">contains the application"s dbcontext</param>
        /// --------------------------------------------------------------------
        private void InitializeIdentityForEF(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create new AdminProfile
            var admin = new AdminProfile("admin", "adminfname", "adminlname", "adminemail");
            var admresult = UserManager.Create(admin, "adminpass"); // user, password
            admin.ConfirmedEmail = true;

            if (!RoleManager.RoleExists("Admin"))
            {
                // Create new Admin Role (we can use this later in DataAnnotations to allow specific views to be only viewable by certain Roles)
                var roleresult = RoleManager.Create(new IdentityRole("Admin"));
            }
            if (admresult.Succeeded)
            {
                var result = UserManager.AddToRole(admin.Id, "Admin"); // Assign user.Id to Admin Role
            }

            // Create new UserProfile
            var user = new UserProfile("user1", "User", "One", "user1 address", "user1 city", "4161234567", "user1@itc.com");
            var userresult = UserManager.Create(user, "userpass");
            user.ConfirmedEmail = true;

            if (!RoleManager.RoleExists("User"))
            {
                // Create new User Role
                var roleresult = RoleManager.Create(new IdentityRole("User"));
            }
            if (userresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, "User"); // Assign user.Id to User Role
            }

            user = new UserProfile("lisac", "Len", "Isac", "len isac address", "Toronto", "6478954544", "lenkei.isac@itc.com");
            userresult = UserManager.Create(user, "123456");
            user.ConfirmedEmail = true;
            UserManager.AddToRole(user.Id, "User");

            user = new UserProfile("rmartin", "Roberto", "Martin", "roberto martin address", "Toronto", "1234567890", "robertomartinm@itc.com");
            userresult = UserManager.Create(user, "123456");
            user.ConfirmedEmail = true;
            UserManager.AddToRole(user.Id, "User");

            user = new UserProfile("nnajarali", "Nabil", "Najarali", "nabil najarali address", "Toronto", "1231234560", "nabil.najarali@gmail.com");
            userresult = UserManager.Create(user, "123456");
            user.ConfirmedEmail = true;
            UserManager.AddToRole(user.Id, "User");
            
            user = new UserProfile("nabil", "Nabil", "Najarali", "nabil najarali address", "Toronto", "1231234560", "vadsariyanabil@gmail.com");
            userresult = UserManager.Create(user, "123456");
            user.ConfirmedEmail = true;
            UserManager.AddToRole(user.Id, "User");

            user = new UserProfile("ithompson", "Iryna", "Thompson", "iryna thompson address", "Toronto", "1234561230", "iraokth@itc.com");
            userresult = UserManager.Create(user, "123456");
            user.ConfirmedEmail = true;
            UserManager.AddToRole(user.Id, "User");

            // Check 'Server Explorer' > right click 'InventoryTrackingSystemDatabaseConnection'
            //  > Expand 'Tables' > right click 'AspNetUsers' > Show Table Data (make sure admin and user1 Users exist in table data)
        }
        /// --------------------------------------------------------------------
        /// <summary> 
        /// Feed test data to db</summary>
        /// <param name="context">contains the application"s dbcontext</param>
        /// --------------------------------------------------------------------
        private void FirstTimeClassInitializer(ApplicationDbContext context)
        {
            // Create Product/Accounts/Order Test Data

            string newline = System.Environment.NewLine;

            // Products
            Product newProduct = new Product();
            newProduct.productName = "Inspiron 15 3000";
            newProduct.imgFile = "~/Content/img/dell-inspiron-15-3000.jpg";
            newProduct.description = "Ports: 1 USB 3.0, 2 USB 2.0, 1 HDMI v1.4a" + newline
                                + "Slots: Kensington lock slot, Media Card (SD, SDHC, SDXC)" + newline
                                + "Keyboard: Full size, spill-resistant keyboard with 10-key numeric keypad" + newline
                                + "Touchpads: Multi-touch gesture-enabled pad with integrated scrolling" + newline
                                + "Camera: HD (720p) capable webcam, microphone" + newline
                                + "Dimensions: Height: 21.7mm (0.85\") x Width: 380mm (14.9\") x Depth: 260.3mm (10.24\"); Weight: 2.14 Kg (4.71lbs)4"
                                ;
            newProduct.price = 599.89;
            newProduct.quantity = 3;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Dell 23 Touch Monitor - P2314T";
            newProduct.imgFile = "/Content/img/Dell-23-Touch-Monitor-P2314T.jpg";
            newProduct.description = "Diagonally Viewable Size: 58.4 cm (23\")" + newline
                                + "Horizontal: 509.18 mm (20.05\")" + newline
                                + "Vertical: 286.42 mm (11.28\")" + newline
                                + "Maximum Resolution: 1920 x 1080 at 60 Hz" + newline
                                + "Aspect Ratio: 16:9" + newline
                                + "Pixel Pitch: 0.265 mm" + newline
                                + "Brightness: 270 cd/m2 (typical)" + newline
                                + "Color Support: Color Gamut (typical): 83%(CIE 1976)" + newline
                                + "Color Depth: 16.7 million colors" + newline
                                + "Contrast Ratio: 1,000:1 (typical)" + newline
                                + "8 million:1 (Dynamic Contrast Ratio)" + newline
                                + "Max Viewing Angle: (typical) (178° vertical / 178° horizontal)" + newline
                                + "Response Time: (typical) 8ms (gray to gray)" + newline
                                + "Panel Type: In-plane switching" + newline
                                + "Panel Backlight: LED"
                ;
            newProduct.price = 529.99;
            newProduct.quantity = 7;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Kingston 4GB microSDHC Class 4 Flash Memory Card";
            newProduct.imgFile = "/Content/img/Kingston-4GB-microSDHC.jpg";
            newProduct.description = "Dimensions & Weight" + newline
                + "Width: 11 mm; Depth: 15 mm; Thickness: 1 mm, Weight: 1.4 g"
                ;

            //newProduct.description = "microSDHC cards offer higher storage for more music, more videos, more pictures, more games - more of everything you need in today's mobile world. The microSDHC card allows you to maximize today's revolutionary mobile devices. Kingston's microSDHC cards use the new speed \"class\" rating of Class 4 that guarantee a minimum data transfer rate of 4MB/sec. for optimum performance with devices that use microSDHC." + newline
            //    + "Identical in physical size to today's standard microSD card, the microSDHC cards are designed to comply with SD Specification Version 2.00 and are only recognized by microSDHC host devices. They can be used as full-size SDHC cards when used with the included adapter. To ensure compatibility, look for the microSDHC or SDHC logo on host devices (e.g. phones, PDAs, and cameras)." + newline
            //    + "Wherever you find yourself in the mobile world, you can trust and rely on Kingston's microSDHC cards." + newline
            //    ;
            newProduct.price = 10.99;
            newProduct.quantity = 80;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Dell Precision Tower 5810 Workstation";
            newProduct.imgFile = "/Content/img/Dell-Precision-Tower-5810-Workstation.jpg";
            newProduct.description = "The latest NVIDIA® Quadro® and AMD FirePro™ graphics deliver the muscle you need to run the most demanding software applications. New Quadro and FirePro graphics offer larger dedicated graphics memory for your large data sets. System memory is expandable up to 256GB using the latest DDR4 ECC memory technology. Experience up to 26%* increased graphics performance with Solidworks and up to 34%* improvement with Maya. (*Internal SPECView perf 12.0.1 benchmark testing)";
            newProduct.price = 500.25;
            newProduct.quantity = 25;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Surface Pro 4";
            newProduct.imgFile = "/Content/img/Surface-4-blue.jpg";
            newProduct.description = "Exceptional performance: Surface Pro 4 features the latest 6th generation Intel Core processor and great battery life so you can get work done." + newline
                + "True versatility: Surface Pro 4 was designed to adapt to you. Work with touch and Surface Pen in tablet mode. Or fold out the optional Type Cover 1 and integrated kickstand when you need a laptop." + newline
                + "Surface Pen writes and erases naturally: As smooth as using your favorite pen, Surface Pen can even markup web pages. And Palm Block technology means you can rest your hand on the screen while you write." + newline
                + "Windows 10 Pro enables a powerful, personalized experience: Surface Pro 4 makes your work life easier in incredible new ways. It runs Windows 10 Pro and the desktop software you rely on. Advanced technology from Microsoft, such as Cortana2, Windows Hello, and an improved Surface Pen puts you in control."
                ;
            newProduct.price = 88.97;
            newProduct.quantity = 7;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "D-Link Systems D-Link Wireless AC1900 Dual Band Gigabit Router (DIR-880L)";
            newProduct.imgFile = "/Content/img/DIR-880L_Side-Right-black.png";
            newProduct.description = "Advanced wireless AC beamforming dramatically enhances wireless signal strength and throughput" + newline
                + "802.11 a/b/g/n/ac wireless LAN for a complete range of wireless compatibility" + newline
                + "Gigabit WAN and LAN ports for high-speed wired connections" + newline
                + "Two USB ports to connect storage drives and printers for sharing" + newline
                + "Band steering efficiently balances the data load between the two wireless bands" + newline
                + "Airtime fairness optimally adjusts the data rate of wireless clients for the best performance" + newline
                + "WPA and WPA2 wireless encryption protects the network from intruders" + newline
                + "Wi-Fi Protected Setup (WPS) securely adds devices to your network at the push of a button" + newline
                + "Web configuration interface"
                ;
            newProduct.price = 189.99;
            newProduct.quantity = 9;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Dell SFP (mini-GBIC) transceiver module - Gigabit Ethernet";
            newProduct.imgFile = "/Content/img/Dell_SFP.jpg";
            newProduct.description = "The Dell SFP transceiver delivers fiber connectivity to extend the range of your network. This hot-pluggable transceiver with SFP (Small Form Factor Pluggable) footprint provides a full-duplex mode with up to 1.25 Gbps bi-directional data transfer rate. The transceiver complies with Gigabit Ethernet and 1000Base-T standards such as IEEE STD 802.3z and IEEE STD 802.3ab." + newline
                + "Compatibility" + newline
                + "This product is compatible with the following systems:" + newline
                + "DELL NETWORKING N4000 N4032" + newline
                + "DELL NETWORKING N4000 N4032F" + newline
                + "DELL NETWORKING N4000 N4064" + newline
                + "DELL NETWORKING N4000 N4064F" + newline
                + "PowerConnect M8024" + newline
                + "PowerEdge C6320" + newline                
                ;
            newProduct.price = 320.20;
            newProduct.quantity = 12;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Linksys PowerLine PLEK500 1 port Gigabit Ethernet Adapter";
            newProduct.imgFile = "/Content/img/Linksys_PowerLine.jpg";
            newProduct.description = "Highlights" + newline
                + " HomePlug AV2 technology" + newline
                + " Data rates up to 500 Mbps" + newline
                + " Compatible with HomePlug Green PHY devices" + newline
                + " Includes 2 single-port adapters to connect one wired Ethernet device to your Powerline network" + newline
                + "Overview" + newline
                + " Linksys Powerline network adapter kit. Theseadapters provide easy setup and let you connect wired network devices to your home network." + newline
                + " The PLEK500 kit includes two PLE500 single-port Powerline adapters." + newline
                + " You need at least two Powerline adapters to create a Powerline network. Connect one Powerline adapter to your network router, and connect the other adapter to a computer or other network device in any room of the house."
                ;
            newProduct.price = 129.99;
            newProduct.quantity = 76;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Linksys E2500 Advanced Dual-Band N Router";
            newProduct.imgFile = "/Content/img/Linksys_E2500.jpg";
            newProduct.description = "Highlights" + newline
                + " Provides good speed to connect your computers, wireless printers, game consoles, and other Wi-Fi devices" + newline
                + " Gives you expanded coverage and reliability so you can enjoy your wireless network from anywhere in your home" + newline
                + "Overview" + newline
                + " Superior Wireless Speed:-" + newline
                + " The Linksys E2500 offers fast speed to connect your computers, wireless printers, game consoles, and other Wi-Fi devices at transfer rates up to 300 + 300 Mbps speed for an optimal home network experience." + newline
                + " Optimal wirelss Coverage:-" + newline
                + "Built with leading 802.11n wireless technology, the Linksys E2500 offers superior range to create a powerful wireless network. MIMO antenna array boosts signal strength to provide expanded coverage and reliability so you can enjoy your wireless network from anywhere in your home."
                ;
            newProduct.price = 69.99;
            newProduct.quantity = 20;
            context.Products.Add(newProduct);

            newProduct = new Product();
            newProduct.productName = "Dell Chromebook 11";
            newProduct.imgFile = "/Content/img/Dell_Chromebook_11.jpg";
            newProduct.description = "Processor: Chrome OS, Intel® Celeron-N2840 Proc, 2GB RAM DDR3L Memory, 16GB eMMC SSD Storage Wifi" + newline
                + "Display: 11.6” Anti-Glare HD Non-Touch LCD" + newline
                + "Battery Life: Up to 10 hours of battery life2" + newline
                + "Case Color: Non- Touch LCD Back Cover ( Black )" + newline
                + "Warranty: 1 Year Ltd Hware Warranty: Mail-in; Customer supplies box, Dell pays shipping, Accidental Damage Service" + newline
                + "Ports: 3.0 USB with BC1.2 charging, 2.0 USB, HDMI 1.4" + newline
                + "Slots: SD/Multi Card Slot (push-Push type), Kensington Lock Slot, Chassis" + newline
                + "Interactive Light: 7 color LED light will accommodate software apps" + newline
                + "Keyboard: Chrome OS layout with spill protection" + newline
                + "Touchpad: 100mm x 56mm with spill protection"
                ;
            newProduct.price = 319.75;
            newProduct.quantity = 5;
            context.Products.Add(newProduct);

            // Orders
            List<ProductsOrdered> prods = new List<ProductsOrdered>();
            prods.Add(new ProductsOrdered(1, 1, 2));
            prods.Add(new ProductsOrdered(1, 2, 4));

            // Users   TODO: Users should have shipping info in profile.
            //               Option to use existing shipping info or new
            //Repo_UserProfile repo_u = new Repo_UserProfile();
            //List<UserProfile> users = new List<UserProfile>();
            //users = repo_u.getUserList();

            Order o = new Order();
            o.OrderID = 1;
            o.CustomerName = "nnajarali";
            o.OrderCompleted = false;
            o.OrderDate = DateTime.Now;
            o.UnitNumber = "70";
            o.Address = "Pond Road";
            o.City = "Toronto";
            o.Province = "Ontario";
            o.PostalCode = "A1B2C3";
            o.PhoneNumber = "6476476470";
            o.NameOnCard = "Nabil Vadsariya";
            o.CardNumber = "1234567890123456";
            o.ExpiryDate = "0120";
            o.CID = "321";
            o.ProductsOrdered = prods;
            context.Orders.Add(o);

            prods = new List<ProductsOrdered>();
            prods.Add(new ProductsOrdered(2, 3, 5));
            prods.Add(new ProductsOrdered(2, 4, 7));
            prods.Add(new ProductsOrdered(2, 5, 1));

            o = new Order();
            o.OrderID = 2;
            o.CustomerName = "lisac";
            o.OrderCompleted = false;
            o.OrderDate = DateTime.Now;
            o.UnitNumber = "10";
            o.Address = "Finch Ave.";
            o.City = "Toronto";
            o.Province = "Ontario";
            o.PostalCode = "P4Q5R6";
            o.PhoneNumber = "4164164160";
            o.NameOnCard = "Len Isac";
            o.CardNumber = "4321876509901234";
            o.ExpiryDate = "7890";
            o.CID = "765";
            o.ProductsOrdered = prods;
            context.Orders.Add(o);

            context.SaveChanges();
        }
    }
}