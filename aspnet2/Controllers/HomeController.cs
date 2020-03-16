using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using aspnet2.Models;
using aspnet2.Services;

namespace aspnet2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {
            var products = db.Products
                .Where(p => p.InStock > 0)
                .Include(p => p.Material);

            ViewBag.Products = products;

            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            var cartService = new CartService();

            ViewBag.CartCount = cartService.GetCount(cartJson);
            ViewBag.CartPrice = cartService.GetPrice(cartJson);

            return View();
        }
    }
}
