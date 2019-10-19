using Proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class UserController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            var model = db.Kullanici.ToList();
            return View(model);
        }
    }
}