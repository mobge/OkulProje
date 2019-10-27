using Proje.Models;
using Proje.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class DonemController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            DonemYilViewModel model = new DonemYilViewModel()
            {
                Donemler = db.Donem.ToList()
            };
            return View(model);
        }
        public ActionResult Ekle()
        {
            return View("Ekle");
        }
        [HttpPost]
        public ActionResult Ekle(DonemYilViewModel donem)
        {
            var checkDonem = db.Donem.Where(x => x.Donem_Adi == donem.Donem_Adi).SingleOrDefault();
            if (checkDonem == null)
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                var EklenecekDonem = new Donem();
                EklenecekDonem.Donem_Adi = donem.Donem_Adi;
                db.Entry(EklenecekDonem).State = EntityState.Added;
                db.SaveChanges();
                ViewBag.Mesaj = "Ekleme işlemi başarılı...";
            }
            else if (checkDonem.Donem_Adi == donem.Donem_Adi)
            {
                ViewBag.Mesaj = "Hata, eklemeye çalıştığınız Dönem sistemde mevcut...";
            }
            return View("Ekle");
        }
        public ActionResult Guncelle(int id)
        {
            DonemYilViewModel donem = new DonemYilViewModel();
            donem.Donem_Id = db.Donem.Where(s => s.Donem_Id == id).Select(s => s.Donem_Id).FirstOrDefault();
            donem.Donem_Adi = db.Donem.Where(s => s.Donem_Id == id).Select(s => s.Donem_Adi).FirstOrDefault();
            return View("Guncelle", donem);
        }
        [HttpPost]
        public ActionResult Guncelle(DonemYilViewModel donem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var guncellenecekDonem = db.Donem.Find(donem.Donem_Id);
                if (guncellenecekDonem == null)
                    return HttpNotFound();
                guncellenecekDonem.Donem_Adi = donem.Donem_Adi;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Sil(int id)
        {
            var silinecekDonem = db.Donem.Find(id);
            if (silinecekDonem == null)
                return HttpNotFound();
            db.Donem.Remove(silinecekDonem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}