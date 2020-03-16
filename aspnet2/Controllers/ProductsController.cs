using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspnet2.Models;
using System.Data.Entity;

namespace aspnet2.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        public ActionResult Index()
        {
            if(Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            var products = db.Products.Include(p => p.Material);

            return View(products.ToList());
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

            var product = db.Products
                .Include(p => p.Material)
                .FirstOrDefault(t => t.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            SelectList materials = new SelectList(db.Materials, "Id", "Title");
            ViewBag.Materials = materials;
            return View ();
        } 

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            try {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch {
                return View ();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var product = db.Products.Find(id);
            if (product != null)
            {
                var materials = new SelectList(db.Materials, "Id", "Title", product.MaterialId);
                ViewBag.Materials = materials;
                return View(product);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            if (Convert.ToInt32(Session["UserTypeId"]) != UserType.ADMIN)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}