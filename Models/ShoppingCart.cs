using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using InventoryTrackingSystem.ViewModels;

namespace InventoryTrackingSystem.Models
{
    public class ShoppingCart : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "cartId";
        int cartItemNo { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        private Repo_UserProfile repo_userprofile = new Repo_UserProfile();

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public bool UpdateCart(int productId, int updateQty)
        {
            bool updated;

            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.ShoppingCartId == ShoppingCartId
                && c.ProductId == productId);

            if (cartItem == null || cartItem.Count == updateQty)
            {
                updated = false;
            }
            else
            {
                cartItem.Count = updateQty;
                updated = true;
            }
            try
            {
                storeDB.SaveChanges();
            }
            catch
            {

            }

            return updated;
        }

        public void AddToCart(Product product, int addQty)
        {
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.ShoppingCartId == ShoppingCartId
                && c.ProductId == product.productId);

            if (cartItem == null)
            {
                cartItemNo++;

                cartItem = new Cart
                {
                    ProductId = product.productId,
                    CartId = ShoppingCartId + ' ' + product.productId,
                    ShoppingCartId = ShoppingCartId,
                    Count = addQty,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else if ((addQty + cartItem.Count) <= product.quantity)
            {
                cartItem.Count++;
            }
            else
            {
                return;
            }
            try
            {
                storeDB.SaveChanges();
            }
            catch
            {
                
            }
        }

        public int RemoveFromCart(int id, bool removeAll)
        {
            var cartItem = storeDB.Carts.Single(
                cart => cart.ShoppingCartId == ShoppingCartId
                && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1 && !removeAll)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.ShoppingCartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.ShoppingCartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.ShoppingCartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }
        public decimal GetTotal()
        {
            double? total = (from cartItems in storeDB.Carts
                             where cartItems.ShoppingCartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Product.price).Sum();

            return total > 0 ? (decimal)total : decimal.Zero;
        }

        public string GetCartId(HttpContextBase storeDB)
        {
            if (storeDB.Session[CartSessionKey] == null)
            {
                Guid tempcartId = Guid.NewGuid();

                storeDB.Session[CartSessionKey] = tempcartId.ToString();
            }
            return storeDB.Session[CartSessionKey].ToString();
        }
    }
}