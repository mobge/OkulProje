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
    public class DersController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index(DersViewModel dersler)
        {
            DersViewModel model = new DersViewModel()
            {
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Dersler = db.Dersler.Where(s => s.Bolum_Id == dersler.Bolum_Id).Where(s=>s.Fakulte_No==dersler.Fakulte_No).ToList()
            };
            return View(model);
        }
        public ActionResult Ekle(string id)
        {
            DersViewModel model = new DersViewModel()
            {
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == id).ToList(),
            };
            return View("Ekle",model);
        }
        [HttpPost]
        public ActionResult Ekle(DersViewModel dersler)
        {
            var checkDersKodu = db.Dersler.Where(x => x.Ders_Kodu==dersler.Ders_Kodu).SingleOrDefault();
            DersViewModel model = new DersViewModel()
            {
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
            };
            if (checkDersKodu == null)
            {
                if (!ModelState.IsValid)
                {
                    return View("Ekle", model);
                }
                //2 farklı tabloya kendine ait bölümleri tek tek eklemek için yaptığım yöntem.
                var eklenecekDers = new Dersler();
                eklenecekDers.Ders_Kodu = dersler.Ders_Kodu;
                eklenecekDers.Ders_Adi = dersler.Ders_Adi;
                eklenecekDers.Fakulte_No = dersler.Fakulte_No;
                eklenecekDers.Bolum_Id = dersler.Bolum_Id;
                db.Entry(eklenecekDers).State = EntityState.Added;
                var eklenecekKazanim = new Ders_Kazanim();
                eklenecekKazanim.Ders_Kodu = dersler.Ders_Kodu;
                eklenecekKazanim.Ders_Ogrenme = dersler.Ders_Ogrenme;
                db.Entry(eklenecekKazanim).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (checkDersKodu.Ders_Kodu == dersler.Ders_Kodu)
            {
                ViewBag.Mesaj = "Hata, eklemeye çalıştığınız Ders sistemde mevcut...";
            }
            return View("Ekle", model);
        }
    }
}