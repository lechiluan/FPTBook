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
    public class FeedbacksController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Feedbacks.ToList().OrderByDescending(o => o.DateSend));
            }
            return View("Error");
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Feedback feedback = db.Feedbacks.Find(id);

                if (feedback == null)
                {
                    return HttpNotFound();
                }
                
                return View(feedback);
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
                Feedback feedback = db.Feedbacks.Find(id);
                if (feedback == null)
                {
                    return HttpNotFound();
                }
                return View(feedback);
            }
            return View("Error");
        }

        // POST: Feedbacks/Delete/FeedbackID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
