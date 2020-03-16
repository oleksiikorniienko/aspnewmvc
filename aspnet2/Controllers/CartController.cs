using System;
using System.Web.Mvc;
using aspnet2.Models;
using aspnet2.Services;
using System.Linq;
using System.Data.Entity;

namespace aspnet2.Controllers
{
    public class CartController : Controller
    {
        CartService cartService;
        ApplicationContext db;

        public CartController()
        {
            cartService = new CartService();
            db = new ApplicationContext();
        }

        public ActionResult Index()
        {
            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
                ViewBag.CartItems = Newtonsoft.Json.JsonConvert.DeserializeObject<Cart>(cartJson).CartItems.ToList();
                ViewBag.CartPrice = cartService.GetPrice(cartJson);

                var productIds = cartService.getProductIds(cartJson);
                var products = db.Products
                    .Include(p => p.Material)
                    .Where(p => productIds.Contains(p.Id));

                ViewBag.Products = products.ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(CartAddView model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null) {
                cartJson = cart.ToString();
            }

            var cartStr = cartService.Add(model, cartJson);
            Session["Cart"] = cartStr;
            Console.WriteLine(cartStr);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            Session["Cart"] = cartService.Delete(id, cartJson);

            return RedirectToAction("Index", "Cart");
        }
    }
}
