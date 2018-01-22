using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using InventoryTrackingSystem.Models;
using InventoryTrackingSystem.ViewModels;

namespace InventoryTrackingSystem.Controllers
{
    public class UserProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Repo_UserProfile repo_userprofile = new Repo_UserProfile();

        // GET: /UserProfile/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(repo_userprofile.getUserList());
        }

        // GET: /UserProfile/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            //Debug.WriteLine("User name: " + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(repo_userprofile.getUserDetails(id));
            }
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
