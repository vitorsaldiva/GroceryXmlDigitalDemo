using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NonLinearDigitalDemo.Models;
using NonLinearDigitalDemo.Util;

namespace NonLinearDigitalDemo.Controllers
{
    public class GroceryController : Controller
    {
        private static string path = string.Empty;
        // GET: Grocery
        public ActionResult Index()
        {
            path = Server.MapPath("~/groceries.xml");
            XmlAccess xml = new XmlAccess(path);
            var products = xml.Get().OrderBy(s => s.Sku);
            return View(products);
        }

        // GET: Grocery/Details/5
        public ActionResult Details(string id)
        {
            XmlAccess xml = new XmlAccess(path);
            var product = xml.GetBySku(id);
            return View(product);
        }

        // GET: Grocery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grocery/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                XmlAccess xml = new XmlAccess(path);
                if (ModelState.IsValid && xml.GetBySku(product.Sku) == null)
                {
                    xml.Add(product);
                }
                else
                {
                    ModelState.AddModelError("error", "Check your data filled");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Check your data filled");
                return View();
            }
        }

        // GET: Grocery/Edit/5
        public ActionResult Edit(string id)
        {
            Product product = new Product();
            try
            {
                XmlAccess xml = new XmlAccess(path);
                product = xml.GetBySku(id);
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(product);
        }

        // POST: Grocery/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                XmlAccess xml = new XmlAccess(path);
                if (ModelState.IsValid && xml.GetBySku(product.Sku) != null)
                {
                    xml.Update(product);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Grocery/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                XmlAccess xml = new XmlAccess(path);
                var product = xml.GetBySku(id);
                xml.Delete(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
