using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NonLinearDigitalDemo.Util;

namespace NonLinearDigitalDemo.Controllers
{
    public class HomeController : Controller
    {
        private static string path = string.Empty;
        public ActionResult Index()
        {
            path = Server.MapPath("~/groceries.xml");
            XmlAccess xml = new XmlAccess(path);
            var products = 
                xml
                .Get()
                .OrderBy(s => s.Sku);
            return View(products);
        }

    }
}