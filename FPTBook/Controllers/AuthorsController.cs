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
    public class AuthorsController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();
        // GET: Authors
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Authors.ToList().OrderBy(a => a.AuthorID));
            }
            return View("Error");

        }

        // GET: Authors/Details/AuthorID
        public ActionResult Details(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return View("Error");


        }

        // GET: Authors/Add
        public ActionResult Add()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            return View("Error");
        }

        // POST: Authors/Add        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "AuthorID,AuthorName,Description")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Update/AuthorID
        public ActionResult Update(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return View("Error");

        }

        // POST: Authors/Update/AuthorID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "AuthorID,AuthorName,Description")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/AuthorID
        public ActionResult Delete(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Author author = db.Authors.Find(id);
                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);
            }
            return View("Error");

        }

        // POST: Authors/Delete/AuthorID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
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
