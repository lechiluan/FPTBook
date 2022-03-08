﻿using System;
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
                var orderDetail = db.OrderDetails.Include(o => o.Book).Include(o => o.Order);
                return View(orderDetail.ToList());
            }
            return View("Error");
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderDetail orderDetail = db.OrderDetails.Find(id);
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
