using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NonLinearDigitalDemo.Util;
using NonLinearDigitalDemo.ViewModels;

namespace NonLinearDigitalDemo.Controllers
{
    public class CartController : Controller
    {
        private static string path = string.Empty;

        // GET: Cart
        public ActionResult Index()
        {
            List<CartViewModel> cartItens = new List<CartViewModel>();
            path = Server.MapPath("~/groceries.xml");
            XmlAccess data = new XmlAccess(path);
            ((List<string>)Session["cart"])?
                .ForEach(c =>
                        {
                            var item = data.GetBySku(c);
                            CartViewModel cart = new CartViewModel() { Name = item.Name, Price = item.Price, Sku = item.Sku, Quantity = 1};
                            cartItens.Add(cart);
                        });           

            return View(cartItens);
        }

        //POST: Cart/AddToCart
        public JsonResult AddToCart(string sku)
        {
            int cartItens = 0;
            if (Session["cart"] == null)
            {
                List<string> cart = new List<string>();
                cart.Add(sku);
                Session["cart"] = cart;
                cartItens = cart.Count();
            }
            else
            {
                List<string> cart = (List<string>)Session["cart"];
                cart.Add(sku);
                cartItens = cart.Count();
            }

            return Json(cartItens, JsonRequestBehavior.AllowGet);
        }

        //POST: Cart/RemoveFromCart
        public JsonResult RemoveFromCart(string sku)
        {
            sku = sku.Trim();
            int cartItens;
            if (Session["cart"] == null)
            {
                cartItens = 0;
            }
            else
            {
                List<string> cart = (List<string>)Session["cart"];
                cart.Remove(sku.Trim());
                cartItens = cart.Count();
            }

            return Json(cartItens, JsonRequestBehavior.AllowGet);
        }
    }
}