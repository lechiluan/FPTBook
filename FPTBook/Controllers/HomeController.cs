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
    public class HomeController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        public ActionResult Index()
        {
            var books = db.Books.ToList();
            return View(books);
        }

        public ActionResult DetailBook(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.Books.ToList().Find(a => a.BookID == id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        public ActionResult CategoryView(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.Books.ToList().Where(a => a.CategoryID == id);

            //Book book = db.Books.ToList().Find(a => a.CategoryID == id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        public ActionResult Search(string Search)
        {
            ViewBag.Search = Search;

            var books = db.Books.ToList().Where(s =>
            s.BookName.ToUpper().Contains(Search.ToUpper()) ||
                 s.Author.AuthorName.ToUpper().Contains(Search.ToUpper()) ||
                 s.Category.CategoryName.ToUpper().Contains(Search.ToUpper()));
            return View(books);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}