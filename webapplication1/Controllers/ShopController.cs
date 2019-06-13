using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DAL;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult AddToCart(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("../Items/Products");
            }
            else
            {
                List<CartItem> Koszyk = new List<CartItem>();
                if (Session["cart"] == null)
                {
                    Session["cart"] = Koszyk;
                }
                else
                {
                    Koszyk = Session["cart"] as List<CartItem>;
                    if (Koszyk == null)
                    {
                        Session["cart"] = new List<CartItem>();
                    }

                }

                using (var context = new OurDbContext())
                {
                    var item = context.Items.FirstOrDefault(x => x.ItemID == id);
                    var cartItem = Koszyk.FirstOrDefault(x => x.Item.ItemID == id);

                    if (cartItem != null)
                    {
                        cartItem.Count++;
                    }
                    else
                    {
                        Koszyk.Add(new CartItem { Item = item, Count = 1 });
                    }
                    Session["cart"] = Koszyk;
                }

                return RedirectToAction("../Items/Products");
            }
        }

        public ActionResult Delete(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("../Items/Products");
            }
            else
            {
                List<CartItem> Koszyk = new List<CartItem>();
                if (Session["cart"] == null)
                {
                    Session["cart"] = Koszyk;
                }
                else
                {
                    Koszyk = Session["cart"] as List<CartItem>;
                    if (Koszyk == null)
                    {
                        Session["cart"] = new List<CartItem>();
                    }

                }

                using (var context = new OurDbContext())
                {
                    var item = context.Items.FirstOrDefault(x => x.ItemID == id);
                    var cartItem = Koszyk.FirstOrDefault(x => x.Item.ItemID == id);

                    if (cartItem.Count > 1)
                    {
                        cartItem.Count--;
                        Session["cart"] = Koszyk;
                        return RedirectToAction("../Shop/ShoppingCart");
                    }
                    if (cartItem.Count == 1)
                    {
                        Koszyk.Remove(cartItem);
                    }
                    Session["cart"] = Koszyk;
                }

                return RedirectToAction("../Shop/ShoppingCart");
            }
        }

        public ActionResult ShoppingCart()
        {
            var koszyk = Session["cart"] as List<CartItem>;
            if (koszyk == null)
                koszyk = new List<CartItem>();

            return View(koszyk);
        }

        public ActionResult Order()
        {
            List<CartItem> Koszyk = new List<CartItem>();
            if (Session["cart"] != null)
            {
                Koszyk = Session["cart"] as List<CartItem>;
                double fullprice = 0;
                foreach (var item in Koszyk)
                {
                    fullprice = fullprice + (item.Item.Price * item.Count);
                }
                Session["endprice"] = fullprice;
                return View();
            }
            return RedirectToAction("../Shop/ShoppingCart");
        }

        
        public class CartItem
        {
            [Required]
            public Item Item { get; set; }

            [Required]
            public int Count { get; set; }
        }
        

    }

   
}
