using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class CartItem
    {
        public Book _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }
    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(Book _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.BookID == _pro.BookID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }

        public void Update_Quantity_Shopping(string id, int _quantity)
        {
            var item = items.Find(s => s._shopping_product.BookID == id);
            if (item != null)
            {
                item._shopping_quantity = _quantity;
            }
        }

        public double TotalPrice()
        {
            var total = items.Sum(s => s._shopping_product.Price * s._shopping_quantity);

            return total;
        }

        public int TotalQuantity()
        {
            return items.Sum(s => s._shopping_quantity);
        }

        public void DeleteCart(string id)
        {
            items.RemoveAll(s => s._shopping_product.BookID == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}