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
    public class PaymentsController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Payments.ToList().OrderBy(a => a.PaymentID));
            }
            return View("Error");

        }

        // GET: Payments/Details/PaymentID
        public ActionResult Details(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Payment payment = db.Payments.Find(id);
                if (payment == null)
                {
                    return HttpNotFound();
                }
                return View(payment);
            }
            return View("Error");
        }

        // GET: Payments/Add
        public ActionResult Add()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            return View("Error");
        }

        // POST: Payments/Add        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "PaymentID,PaymentName")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payments/Update/PaymentID
        public ActionResult Update(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Payment payment = db.Payments.Find(id);
                if (payment == null)
                {
                    return HttpNotFound();
                }
                return View(payment);
            }
            return View("Error");

        }

        // POST: Payments/Update/PaymentID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "PaymentID,PaymentName")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/PaymentID
        public ActionResult Delete(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Payment payment = db.Payments.Find(id);
                if (payment == null)
                {
                    return HttpNotFound();
                }
                return View(payment);
            }
            return View("Error");
        }

        // POST: Payments/Delete/PaymentID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
