using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AssignmentFPTBook.Controllers
{
    public class AdminAccountController : Controller
    {
        private FPTBookDBContext db = new FPTBookDBContext();

        // GET: AdminAccount
        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            return View("Error");
        }

        public ActionResult UpdateInfor()
        {
            var admin = Session["Admin"];

            if (admin == null)
            {
                return View("Error");
            }

            Account objAccount = db.Accounts.ToList().Find(a => a.Username.Equals(admin));

            if (objAccount == null)
            {
                return HttpNotFound();
            }
            return View(objAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInfor(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Attach(account);
                db.Entry(account).Property(a => a.Fullname).IsModified = true;
                db.Entry(account).Property(a => a.Email).IsModified = true;
                db.Entry(account).Property(a => a.Telephone).IsModified = true;
                db.Entry(account).Property(a => a.Address).IsModified = true;

                db.SaveChanges();

                Response.Write("<script>alert('Update information success!');window.location='/AdminAccount';</script>");
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(Account account)
        {
            var user = Session["Admin"];

            Account objAccount = db.Accounts.ToList().Find(p => p.Username.Equals(user) && p.Password.Equals(PasswordMD5(account.CurrentPassword)));
            if (objAccount == null)
            {
                ViewBag.aError = "Current Password is incorrect";
                return View();
            }
            if (account.NewPassword != account.ConfirmNewPassword)
            {
                ViewBag.aConfirm = "The new password and confirmation new password do not match.";
            }

            else
            {
                objAccount.Password = PasswordMD5(account.NewPassword);
                objAccount.ConfirmPassword = objAccount.Password;

                db.Accounts.Attach(objAccount);
                db.Entry(objAccount).Property(a => a.Password).IsModified = true;
                db.SaveChanges();

                ViewBag.aSuccess = "Password Change successfully";
            }
            return View("UpdateInfor");
        }

        public ActionResult ViewUserList()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Accounts.ToList().OrderByDescending(a => a.Role));
            }
            return View("Error");
        }

        public ActionResult DetailUser(string id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Account user = db.Accounts.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailUser(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Attach(account);
                db.Entry(account).Property(e => e.Role).IsModified = true;
                db.Entry(account).Property(e => e.Role).IsModified = true;

                db.SaveChanges();

                return RedirectToAction("ViewUserList");
            }
            return View(account);
        }

        public ActionResult FeedbackView()
        {
            if (Session["Admin"] != null)
            {
                return View(db.Feedbacks.ToList().OrderByDescending(o => o.DateSend));
            }
            return View("Error");
        }



        public static string PasswordMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}