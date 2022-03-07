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
    public class PublishersController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();
        // GET: Publishers
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Publishers.ToList().OrderBy(a => a.PublisherID));
            }
            return View("Error");

        }

        // GET: Publishers/Details/PublisherID
        public ActionResult Details(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publisher publisher = db.Publishers.Find(id);
                if (publisher == null)
                {
                    return HttpNotFound();
                }
                return View(publisher);
            }
            return View("Error");


        }

        // GET: Publishers/Add
        public ActionResult Add()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            return View("Error");
        }

        // POST: Publishers/Add        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "PublisherID,PublisherName,Description")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Publishers/Update/PublisherID
        public ActionResult Update(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publisher publisher = db.Publishers.Find(id);
                if (publisher == null)
                {
                    return HttpNotFound();
                }
                return View(publisher);
            }
            return View("Error");

        }

        // POST: Publishers/Update/PublisherID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "PublisherID,PublisherName,Description")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/PublisherID
        public ActionResult Delete(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publisher publisher = db.Publishers.Find(id);
                if (publisher == null)
                {
                    return HttpNotFound();
                }
                return View(publisher);
            }
            return View("Error");

        }

        // POST: Publisher/Delete/PublisherID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
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
