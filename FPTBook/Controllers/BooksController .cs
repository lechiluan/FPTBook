using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.Models;

namespace AssignmentFPTBook.Controllers
{
    public class BooksController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        // GET: Books
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                var books = db.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.Publisher);
                return View(books.ToList());
            }
            return View("Error");
        }

        // GET: Books/Details/BookID
        public ActionResult Details(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Book book = db.Books.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            return View("Error");
        }

        // GET: Books/Create
        public ActionResult Add()
        {
            if (Session["Admin"] != null)
            {
                ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
                return View();
            }
            return View("Error");
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HttpPostedFileBase image, Book book)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Image/"), pic);
                    image.SaveAs(path);

                    book.Image = pic;
                    db.Books.Add(book);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "............";
                    return View();
                }
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: Books/Edit/BookID
        public ActionResult Update(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Book book = db.Books.Find(id);
                Session["imgPath"] = "~/Image/" + book.Image;
                if (book == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
                return View(book);
            }
            return View("Error");
        }

        // POST: Books/Edit/BookID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HttpPostedFileBase image, Book book)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Image/"), pic);
                    string oldPath = Request.MapPath(Session["imgPath"].ToString());
                    image.SaveAs(path);

                    book.Image = pic;

                    db.Entry(book).State = EntityState.Modified;
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Books.Attach(book);
                    db.Entry(book).Property(a => a.BookName).IsModified = true;
                    db.Entry(book).Property(a => a.AuthorID).IsModified = true;
                    db.Entry(book).Property(a => a.CategoryID).IsModified = true;
                    db.Entry(book).Property(a => a.PublisherID).IsModified = true;
                    db.Entry(book).Property(a => a.Quantity).IsModified = true;
                    db.Entry(book).Property(a => a.Price).IsModified = true;
                    db.Entry(book).Property(a => a.Description).IsModified = true;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", book.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: Books/Delete/BookID
        public ActionResult Delete(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Book book = db.Books.Find(id);
                Session["imgOldPath"] = "~/Image/" + book.Image;
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
            return View("Error");
        }

        // POST: Books/Delete/BookID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string oldPath = Request.MapPath(Session["imgOldPath"].ToString());

            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
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
