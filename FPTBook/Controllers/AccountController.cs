using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace FPTBook.Controllers
{
    public class AccountController : Controller
    {
        private FPTBookDBContext _db = new FPTBookDBContext();

        
        public ActionResult SignUp()
        {
            // Return page SignUp
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Account _account)
        {
            if (ModelState.IsValid)
            {
                // Check Exits
                var usernameCheckExists = _db.Accounts.FirstOrDefault(u => u.Username == _account.Username);
                var emailCheckExists = _db.Accounts.FirstOrDefault(e => e.Email == _account.Email);
                var phoneCheckExists = _db.Accounts.FirstOrDefault(p => p.Telephone == _account.Telephone);

                // Check input
                if (usernameCheckExists == null)
                {
                    if (emailCheckExists == null)
                    {
                        if (phoneCheckExists == null)
                        {
                            // Add data into database
                            _account.Password = PasswordMD5(_account.Password);
                            _db.Configuration.ValidateOnSaveEnabled = false;
                            _db.Accounts.Add(_account);
                            _db.SaveChanges();

                            Response.Write("<script>alert('Sign Up Success')</script>");
                            return View("SignIn");
                        }
                        else
                        {
                            Response.Write("<script>alert('Phone number you just entered already exists. Please change another phone number')</script>");
                            return View();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Email you just entered already exists. Please change another email address')</script>");
                        return View();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Username you just entered already exists. Please change another username')</script>");
                    return View();
                }
            }
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var MD5Pass = PasswordMD5(password);
                var data = _db.Accounts.Where(e => e.Username.Equals(username) && e.Password.Equals(MD5Pass)).ToList();

                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().Role == 0)
                    {
                        Session["Username"] = data.FirstOrDefault().Username;
                        Session["Phone"] = data.FirstOrDefault().Telephone;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["Admin"] = data.FirstOrDefault().Username;
                        Session["AdminName"] = data.FirstOrDefault().Fullname;

                        return RedirectToAction("Index", "AdminAccount");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Username or Password is wrong. Please enter again!')</script>");
                }
            }
            return View();
        }

        public ActionResult UpdateAccount()
        {
            var user = Session["Username"];

            if (user == null)
            {
                Response.Write("<script>alert('Please sign in to continue!'); window.location='/Account/SignIn'</script>");
            }
            Account objAccount = _db.Accounts.ToList().Find(a => a.Username.Equals(user));
            if (objAccount == null)
            {
                return HttpNotFound();
            }
            return View(objAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccount(Account _account)
        {
            if (ModelState.IsValid)
            {
                _db.Accounts.Attach(_account);

                _db.Entry(_account).Property(a => a.Fullname).IsModified = true;
                _db.Entry(_account).Property(a => a.Email).IsModified = true;
                _db.Entry(_account).Property(a => a.Telephone).IsModified = true;
                _db.Entry(_account).Property(a => a.Address).IsModified = true;

                _db.SaveChanges();

                Response.Write("<script>alert('Update information success!');window.location='/';</script>");
            }
            return View(_account);
        }

        public ActionResult ChangePassword()
        {
            var user = Session["Username"];
            if (user == null)
            {
                Response.Write("<script>alert('Please sign in to continue!'); window.location='/Account/SignIn'</script>");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Account account)
        {
            var user = Session["Username"];

            Account objAccount = _db.Accounts.ToList().Find(p => p.Username.Equals(user) && p.Password.Equals(PasswordMD5(account.CurrentPassword)));
            if (objAccount == null)
            {
                ViewBag.Error = "Current Password is incorrect";
                return View();
            }
            if (account.NewPassword != account.ConfirmNewPassword)
            {
                ViewBag.Confirm = "The new password and confirmation new password do not match.";
            }

            else
            {
                objAccount.Password = PasswordMD5(account.NewPassword);
                objAccount.ConfirmPassword = objAccount.Password;

                _db.Accounts.Attach(objAccount);
                _db.Entry(objAccount).Property(a => a.Password).IsModified = true;
                _db.SaveChanges();

                ViewBag.Success = "Password Change successfully";
            }
            return View();
        }

        public ActionResult SignOut()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SendFeedback()
        {
            var user = Session["Username"];
            if (user == null)
            {
                Response.Write("<script>alert('Please sign in to continue!'); window.location='/Account/SignIn'</script>");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _db.Feedbacks.Add(feedback);
                _db.SaveChanges();

                ViewBag.Feedback = "Thanks for your feedback.";
            }
            return View();
        }

        // Hash password with MD5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
