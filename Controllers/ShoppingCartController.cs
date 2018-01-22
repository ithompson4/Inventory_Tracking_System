using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.ViewModels;

namespace InventoryTrackingSystem.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        Repo_Product repo_product = new Repo_Product();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            ViewBag.EmptyCart = "Your cart is empty right now.";

            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new VM_ShoppingCart
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateCart(int id, int updateQty)
        {
            // Remove Whole Product from Shopping Cart if User enters zero or less
            if (updateQty <= 0)
            {
                return RemoveFromCart(id, true);
            }

            else
            {
                var addedProduct = context.Products
                    .Single(product => product.productId == id);

                var cart = ShoppingCart.GetCart(this.HttpContext);

                string productName = context.Carts
                    .Single(item => item.ProductId == id).Product.productName;

                // Update Quantity equals to total quantity in stock 
                //  if user enters higher amount
                if (updateQty > addedProduct.quantity)
                {
                    updateQty = addedProduct.quantity;
                }

                bool updated = cart.UpdateCart(id, updateQty);

                string updateMessage = (updated == true)
                    ? Server.HtmlEncode(productName) +
                        " quantity has been updated"
                    : "";

                ViewData["Message"] = updateMessage;
                //ViewData["CartTotal"] = cart.GetTotal();
                //ViewData["CartCount"] = cart.GetCount();
                //ViewData["ItemCount"] = updateQty;
                //ViewData["ProductId"] = id;
                
                var results = new VM_ShoppingCartUpdate
                {
                    Message = updateMessage,
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = updateQty,
                    ProductId = id
                };

                //return View(Json(results));
                return Json(results);
            }
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            var addedProduct = context.Products
                .Single(product => product.productId == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct, 1);

            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id, bool removeAll)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string productName = context.Carts
                .Single(item => item.ProductId == id).Product.productName;

            int itemCount;

            if (removeAll)
            {

                itemCount = cart.RemoveFromCart(id, removeAll);
            }
            else
            {
                itemCount = cart.RemoveFromCart(id, removeAll);
            }            

            var results = new VM_ShoppingCartUpdate()
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                ProductId = id
            };
            
            Response.AddHeader("Refresh", "5");

            return Json(results);
        }
        //
        // GET: /Store/Checkout/5
        //[HttpPost]
        public ActionResult Checkout()
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            List<Cart> cartItems = cart.GetCartItems();

            List<Product> product_list = new List<Product>();
            
            TempData["productsOrdered"] = repo_product.getOrderProducts(cartItems);

            return RedirectToAction("Create", "Order");
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
	}
}