using System;
using System.Data.Entity;
using System.Web.Mvc;
using aspnet2.Models;
using aspnet2.Services;
using System.Linq;

namespace aspnet2.Controllers
{
    public class OrderController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        CartService cartService = new CartService();
        CartToOrderService cartToOrderService = new CartToOrderService();

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            var orders = db.Order.Include(o => o.UserInfo);

            return View(orders.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var order = db.Order
                .Include(p => p.UserInfo)
                .FirstOrDefault(t => t.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderProducts = db.OrderProducts
                .Where(op => op.OrderId == id)
                .Include(op => op.Product)
                .Include(op => op.Product.Material);

            return View(order);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo userInfo)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("index", "Home");
            }

            db.UserInfo.Add(userInfo);
            db.SaveChanges();

            var userId = int.Parse(Session["UserId"].ToString());

            var cart = Session["Cart"];
            var cartJson = "";

            if (cart != null)
            {
                cartJson = cart.ToString();
            }

            var sum = cartService.GetPrice(cartJson);

            Order order = new Order
            {
                UserId = userId,
                UserInfoId = userInfo.Id,
                Sum = sum
            };


            db.Order.Add(order);
            db.SaveChanges();
            cartToOrderService.CartToOrder(cartJson, order.Id);
            Session["Cart"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}
