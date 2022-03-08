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
    public class OrdersController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var orders = db.Orders.Include(o => o.Account);
                return View(orders.ToList());
            }
            return View("Error");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] != null)
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