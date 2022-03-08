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

        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var orederdetail = db.OrderDetail.Include(o => o.Order).Include(o => o.Book);
                return View(orederdetail.ToList());
            }
            return View("Error");
        }

        public ActionResult Details(int? id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderDetail orderDetail = db.OrderDetail.Find(id);
                if (orderDetail == null)
                {
                    return HttpNotFound();
                }
                return View(orderDetail);
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