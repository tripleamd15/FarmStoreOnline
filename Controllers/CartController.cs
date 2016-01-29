using FarmStore0906.Insfrastructure.Abstract;
using FarmStore0906.Insfrastructure.Concrete;
using FarmStore0906.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmStore0906.Controllers
{
    public class CartController : Controller
    {
        
        // GET: Cart
        public ActionResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel { 
                ReturnUrl=returnUrl,
                Cart=cart
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int fruitId,string returnUrl)
        {
            Fruit fruit = null;
            using (FarmstoreEntities dbContext=new FarmstoreEntities ())
            {
                fruit = dbContext.Fruits.SingleOrDefault(f => f.Id == fruitId);
            }
            if (fruit!=null)
            {
                cart.AddItem(fruit, 1);
            }
            return RedirectToAction("Index", new { returnUrl});
        }

        public RedirectToRouteResult UpdateCart(Cart cart, int id, int quantity, string returnUrl)
        {
            Fruit fruit = null;
            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                fruit = dbContext.Fruits.SingleOrDefault(f => f.Id == id);

                if (quantity>0)
                {
                    cart.UpdateItem(fruit, quantity);
                }
                else 
                {
                    cart.RemoveLine(fruit);   
                }
            }
            
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int fruitId, string returnUrl)
        {
            Fruit fruit = null;
            using (FarmstoreEntities dbContext = new FarmstoreEntities())
            {
                fruit = dbContext.Fruits.SingleOrDefault(f => f.Id == fruitId);
            }
            if (fruit != null)
            {
                cart.RemoveLine(fruit);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ActionResult Checkout()
        {
            return View(new  ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                //orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");

            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}