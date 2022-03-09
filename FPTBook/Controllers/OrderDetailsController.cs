using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class OrderDetailsController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var orderDetails = db.OrderDetails.Include(o => o.Order);
                return View(orderDetails.ToList());
            }
            return View("Error");
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
