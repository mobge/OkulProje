using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var ad = (string)Session["ad"];
            var soyad = (string)Session["soyad"];
            ViewBag.Mesaj = ad+" "+soyad+" Hoşgeldiniz, Lütfen Menü ile ilgili işleme gidiniz.";
            return View();
        }
    }
}