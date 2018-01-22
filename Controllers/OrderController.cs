using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using InventoryTrackingSystem.Models;
//using Beanstream.Api.SDK;
//using Beanstream.Api.SDK.Domain;
//using Beanstream.Api.SDK.Exceptions;
//using Beanstream.Api.SDK.Requests;

namespace InventoryTrackingSystem.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Order/
        [Authorize(Roles = "Admin, User")]
        public ActionResult Index()
        {
            if (User.IsInRole("User")) 
            {
                List<Order> customerOrders = db.Orders.Where(s => s.CustomerName == User.Identity.Name).ToList();
                return View(customerOrders);
            }
            // else return all orders
            return View(db.Orders.ToList());
        }

        // GET: /Order/Details/5
        [Authorize(Roles = "Admin, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ProductsOrdered> productsOrdered = db.ProductsOrdered.Where(s => s.OrderID == (id.HasValue ? id.Value : -1)).ToList();

            Order order = db.Orders.Find(id);

            List<Product> allProducts = db.Products.ToList();
            List<Product> productsOrderedDetails = new List<Product>();

            foreach (var po in productsOrdered)
            {
                Product prod = allProducts.Find(i => i.productId == po.ProductID);
                prod.qtyToAdd = po.QtyToAdd;
                productsOrderedDetails.Add(prod);
            }

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.order = order;
            orderDetail.ProductsOrdered = productsOrderedDetails;

            if (orderDetail == null || orderDetail.order == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: /Order/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            List<Product> productsOrdered = (List<Product>)TempData["productsOrdered"];
            
            if (productsOrdered != null && productsOrdered.Count > 0)
            {
                int orderID = db.Orders.Max(n => n.OrderID) + 1;

                if (orderID > 0)
                {
                    List<ProductsOrdered> prodsOrdered = new List<ProductsOrdered>();

                    foreach (var item in productsOrdered)
                    {
                        prodsOrdered.Add(new ProductsOrdered(orderID, item.productId, item.qtyToAdd));
                    }

                    if (prodsOrdered != null && prodsOrdered.Count > 0)
                    {
                        Order o = new Order();
                        o.OrderID = orderID;
                        o.CustomerName = User.Identity.Name;
                        o.ProductsOrdered = prodsOrdered;

                        // Empty cart once order is complete
                        var cart = ShoppingCart.GetCart(this.HttpContext);
                        cart.EmptyCart();

                        return View(o);
                    }
                }
            }
            return View("Error");
        }

        // POST: /Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "ID,OrderID,CustomerName,OrderDate,OrderCompleted,UnitNumber,Address,City,Province,PostalCode,PhoneNumber,NameOnCard,CardNumber,ExpiryDate,CID")] Order order)
        {
            List<ProductsOrdered> productsOrdered = (List<ProductsOrdered>)TempData["productsOrderedFromView"];

            if (ModelState.IsValid && productsOrdered != null && productsOrdered.Count > 0)
            {
                order.OrderDate = DateTime.Now;
                order.OrderCompleted = false;
                order.ProductsOrdered = productsOrdered;

                //// Payment Gateway
                //string personName = order.CustomerName ?? string.Empty;
                ////double paymentAmount = order. < 0 ? 0 : paymentAmount;
                //string creditCardNum = order.CardNumber ?? string.Empty;
                //string strExpirtyMonth = order.ExpiryDate.Substring(0, 2);
                //string strExpiryYear = DateTime.Now.Year.ToString();
                //string strExpiryYearShort = strExpiryYear.Substring(strExpiryYear.Length - 2, 2);

                //// Authenticates with the payment gateway using these values
                //Gateway beanstreamGateway = new Gateway()
                //{
                //    // DO NOT change these values or else your payment details will not be communicated to the payment gateway
                //    MerchantId = 300202407,
                //    PaymentsApiKey = "09d174b5797D4970833Ea316ed07DC6D",
                //    ApiVersion = "1"
                //};

                //// These are the payment values being provided to the gateway. 
                //CardPaymentRequest paymentRequest = new CardPaymentRequest
                //{
                //    //Amount = paymentAmount,
                //    Amount = 1,
                //    OrderNumber = string.Format("{0}", Guid.NewGuid().ToString()),
                //    Card = new Card
                //    {
                //        Name = personName,
                //        Number = creditCardNum, // ex. 5100000010001004 for a specific Mastercard (success), 4242424242424242 (fail)
                //        ExpiryMonth = strExpirtyMonth,
                //        ExpiryYear = strExpiryYearShort,
                //        Cvd = order.CID
                //    }
                //};

                //try
                //{
                //    PaymentResponse pymtResponse = beanstreamGateway.Payments.MakePayment(paymentRequest);
                //    db.Orders.Add(order);
                //    db.SaveChanges();

                //    return RedirectToAction("Index");
                //}
                //catch
                //{
                //    return View(order);
                //}

                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(order);
            }

        }

        // GET: /Order/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,OrderID,CustomerName,OrderDate,OrderCompleted,UnitNumber,Address,City,Province,PostalCode,PhoneNumber,NameOnCard,CardNumber,ExpiryDate,CID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: /Order/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
