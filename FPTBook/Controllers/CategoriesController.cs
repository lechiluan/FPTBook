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
    public class CategoriesController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();
        // GET: Categories
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Categories.ToList().OrderBy(a => a.CategoryID));
            }
            return View("Error");
        }

        // GET: Categories/Details/CategoryID
        public ActionResult Details(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            return View("Error");
        }

        // GET: Categories/Add
        public ActionResult Add()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            return View("Error");
        }

        // POST: Categories/Add        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Update/CategoryID
        public ActionResult Update(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            return View("Error");

        }

        // POST: Categories/Update/CategoryID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/CategoryID
        public ActionResult Delete(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            return View("Error");

        }

        // POST: Categories/Delete/CategoryID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
