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
    public class CartsController : Controller
    {
        public FPTBookDBContext db = new FPTBookDBContext();

        // GET: Carts
        public ActionResult Index()
        {
            return View();
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddtoCart(string id)
        {
            if (Session["Username"] != null)
            {
                var pro = db.Books.SingleOrDefault(s => s.BookID == id);
                if (pro != null)
                {
                    GetCart().Add(pro);
                }
                return RedirectToAction("ViewCart", "Carts");
            }
            return View("ErrorCart");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["Book_ID"];
            int quantity = int.Parse(form["Quantity"]);
            Book qStock = db.Books.FirstOrDefault(a => a.BookID == id_pro);

            if (quantity > qStock.Quantity)
            {
                return Content("<script>alert('Quantity is larger than our stock');window.location.replace('/');</script>");
            }
            else
            {
                cart.Update_Quantity_Shopping(id_pro, quantity);

            }
            return RedirectToAction("ViewCart", "Carts");
        }

        public ActionResult Delete(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteCart(id);
            return RedirectToAction("ViewCart", "Carts");
        }

        public ActionResult ViewCart()
        {
            if (Session["Username"] != null)
            {
                if (Session["Cart"] == null)
                {
                    Response.Write("<script>alert('Cart is empty');window.location='/'</script>");
                }
                Cart cart = Session["Cart"] as Cart;
                return View(cart);
            }
            return View("ErrorCart");
        }

        public PartialViewResult BagCart()
        {
            int total_item = 0;

            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
                total_item = cart.TotalQuantity();
            ViewBag.TotalItem = total_item;

            return PartialView("BagCart");
        }

        public ActionResult OrderCart(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                _order.Username = form["cUsername"];
                _order.DeliveyAddress = form["cAddress"];
                _order.Telephone = form["cPhone"];
                _order.Fullname = form["cFullName"];
                _order.TotalPrice = Convert.ToInt32(form["cTotalPrice"]);
                db.Orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = _order.OrderID;
                    orderDetail.BookID = item._shopping_product.BookID;
                    orderDetail.Price = item._shopping_product.Price;
                    orderDetail.Quantity = item._shopping_quantity;
                    orderDetail.Subtotal = item._shopping_product.Price * item._shopping_quantity;

                    var pro = db.Books.SingleOrDefault(s => s.BookID == orderDetail.BookID);

                    pro.Quantity -= orderDetail.Quantity;
                    db.Books.Attach(pro);
                    db.Entry(pro).Property(a => a.Quantity).IsModified = true;

                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("OrderSuccess", "Carts", new { id = _order.OrderID });
            }
            catch
            {
                return Content("Error checkout, Check information again");
            }
        }

        public ActionResult OrderSuccess(int? id)
        {
            if (Session["Username"] != null)
            {
                var order = db.Orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderHistory(string id)
        {
            if (Session["Username"] != null)
            {
                var orderHis = db.Orders.ToList().Where(s => s.Username == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderHistoryDetails(int? id)
        {
            if (Session["Username"] != null)
            {
                var order = db.Orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }
    }
}