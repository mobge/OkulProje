using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class DerslerimController : Controller
    {
        // GET: Derslerim
        public ActionResult Index()
        {
            return View();
        }
    }
}