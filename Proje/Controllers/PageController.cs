using Proje.Models;
using Proje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proje.Controllers
{
    [AllowAnonymous]
    public class PageController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Login()
        {
            KullaniciViewModel kullaniciViewModel = new KullaniciViewModel();
            return View(kullaniciViewModel);
        }
        [HttpPost] //anasayfa-kullanıcı yönetimi-kategori yönetimi- çıkış sol bar ve tap bar. Shared 
        public ActionResult Login(KullaniciViewModel kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Sicil_No == kullanici.Sicil_No && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}