using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryTrackingSystem.Models;
using AutoMapper;

namespace InventoryTrackingSystem.ViewModels
{
    public class Repo_Product : RepositoryBase
    {
        /// <summary>
        /// Get Product list for Admin
        /// </summary>
        /// <returns></returns>
        public Product getProduct(int? pid)
        {
            var product = context.Products.FirstOrDefault(p => p.productId == pid);

            return product;
        }

        /// <summary>
        /// Get Product list for Admin
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductList> getProductList()
        {
            var pl = context.Products.OrderBy(p => p.productId).AsEnumerable();

            return Mapper.Map<IEnumerable<ProductList>>(pl);
        }

        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ProductDetails getProductDetails(int? pid)
        {
            var pd = context.Products.FirstOrDefault(p => p.productId == pid);

            return Mapper.Map<ProductDetails>(pd);
        }

        /// <summary>
        /// Get Product details for User Role
        /// </summary>
        /// <returns></returns>
        public ProductDetailsForUser getProductDetailsForUser(int? pid)
        {
            var pl = context.Products.FirstOrDefault(p => p.productId == pid);

            return Mapper.Map<ProductDetailsForUser>(pl);
        }

        /// <summary>
        /// Get Product for Edit view
        /// </summary>
        /// <returns></returns>
        public ProductEdit getProductForEdit(int? pid)
        {
            var pl = context.Products.FirstOrDefault(p => p.productId == pid);

            return Mapper.Map<ProductEdit>(pl);
        }

        /// <summary>
        /// Get Product for Edit view
        /// </summary>
        /// <returns></returns>
        public List<Product> getOrderProducts(List<Cart> cartItems)
        {
            List<Product> pl = new List<Product>();

            foreach (Cart cartItem in cartItems)
            {
                var product = context.Products.FirstOrDefault(p => p.productId == cartItem.ProductId);
                product.qtyToAdd = cartItem.Count;
                pl.Add(product);
            }

            return pl;
        }
    }
}