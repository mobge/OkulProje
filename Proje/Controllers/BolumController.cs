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
    public class BolumController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            var bolumModel = db.Bolum.ToList();
            return View("Index", bolumModel);
        }
        public ActionResult Ekle()
        {
            BolumViewModel model = new BolumViewModel()
            {
                Fakulte = db.Fakulte.ToList()
            };
            return View("Ekle", model);
        }
        [HttpPost]
        public ActionResult Ekle(BolumViewModel bolum)
        {
            var checkBolumAdi = db.Bolum.Where(x => x.Bolum_Adi == bolum.Bolum_Adi).SingleOrDefault();
            var model = new BolumViewModel();
            model.Fakulte = db.Fakulte.ToList();
            if (checkBolumAdi == null)
            {
                if (!ModelState.IsValid)
                {
                    return View("Ekle", model);
                }
                //2 farklı tabloya kendine ait bölümleri tek tek eklemek için yaptığım yöntem.
                var eklenecekBolum = new Bolum();
                eklenecekBolum.Bolum_Adi = bolum.Bolum_Adi;
                eklenecekBolum.Fakulte_No = bolum.Fakulte_No;
                db.Entry(eklenecekBolum).State = EntityState.Added;
                var eklenecekYeterlilik = new Bolum_Kazanim();
                eklenecekYeterlilik.Bolum_Id = bolum.Bolum_Id;
                eklenecekYeterlilik.Bolum_Yeterlilik = bolum.Bolum_Yeterlilik;
                db.Entry(eklenecekYeterlilik).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (checkBolumAdi.Bolum_Adi == bolum.Bolum_Adi)
            {
                ViewBag.Mesaj = "Hata, eklemeye çalıştığınız Bölüm sistemde mevcut...";
            }
            return View("Ekle", model);
        }
        public ActionResult Guncelle(int id)
        {
            BolumViewModel bolumViewModel = new BolumViewModel();
            bolumViewModel.Fakulte = db.Fakulte.ToList();
            bolumViewModel.Bolum_Id = db.Bolum.Where(s => s.Bolum_Id == id).Select(s => s.Bolum_Id).FirstOrDefault();
            bolumViewModel.Bolum_Adi = db.Bolum.Where(s => s.Bolum_Id == id).Select(s => s.Bolum_Adi).FirstOrDefault();
            bolumViewModel.Bolum_Kazanim_Id = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).Select(s => s.Id).FirstOrDefault();
            bolumViewModel.Bolum_Yeterlilik = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).Select(s => s.Bolum_Yeterlilik).FirstOrDefault();
            return View("Guncelle", bolumViewModel);
        }
        [HttpPost]
        public ActionResult Guncelle(BolumViewModel bolumViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var guncellenecekBolum = db.Bolum.Find(bolumViewModel.Bolum_Id);
                if (guncellenecekBolum == null)
                    return HttpNotFound();
                guncellenecekBolum.Bolum_Id = bolumViewModel.Bolum_Id;
                guncellenecekBolum.Bolum_Adi = bolumViewModel.Bolum_Adi;
                guncellenecekBolum.Fakulte_No = bolumViewModel.Fakulte_No;
                var guncellenecekBolumYeterlilik = db.Bolum_Kazanim.Find(bolumViewModel.Bolum_Kazanim_Id);
                guncellenecekBolumYeterlilik.Bolum_Yeterlilik = bolumViewModel.Bolum_Yeterlilik;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Sil(int id)
        {
            var silinecekBolum = db.Bolum.Find(id);
            Bolum_Kazanim silinecekBolumKazanim = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).First();
                if (silinecekBolum == null)
                return HttpNotFound();
            db.Bolum_Kazanim.Remove(silinecekBolumKazanim);
            db.Bolum.Remove(silinecekBolum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}