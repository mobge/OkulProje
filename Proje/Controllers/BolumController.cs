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
    [Authorize(Roles = "1")]
    public class BolumController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index(BolumViewModel bolumler)
        {
            BolumViewModel model = new BolumViewModel()
            {
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == bolumler.Fakulte_No).ToList()
            };
            return View(model);
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
            BolumDetailViewModel bolumDetailViewModel = new BolumDetailViewModel();
            bolumDetailViewModel.UpdatedBolum = (from a in db.Bolum join b in db.Fakulte on a.Fakulte_No equals b.Fakulte_No where a.Bolum_Id == id select new BolumDetail { Bolum_Id = a.Bolum_Id, Bolum_Adi = a.Bolum_Adi, Fakulte_No = b.Fakulte_No, Bolum_Kazanim_Id = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).Select(s => s.Id).FirstOrDefault(), Bolum_Yeterlilik = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).Select(s => s.Bolum_Yeterlilik).FirstOrDefault() }).FirstOrDefault();
            bolumDetailViewModel.Fakulte = db.Fakulte.ToList();
            return View("Guncelle", bolumDetailViewModel);
        }
        [HttpPost]
        public ActionResult Guncelle(BolumDetailViewModel bolumDetail)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var guncellenecekBolum = db.Bolum.Find(bolumDetail.UpdatedBolum.Bolum_Id);
                if (guncellenecekBolum == null)
                    return HttpNotFound();
                guncellenecekBolum.Bolum_Id = bolumDetail.UpdatedBolum.Bolum_Id;
                guncellenecekBolum.Bolum_Adi = bolumDetail.UpdatedBolum.Bolum_Adi;
                guncellenecekBolum.Fakulte_No = bolumDetail.UpdatedBolum.Fakulte_No;
                var guncellenecekBolumYeterlilik = db.Bolum_Kazanim.Find(bolumDetail.UpdatedBolum.Bolum_Kazanim_Id);
                guncellenecekBolumYeterlilik.Bolum_Yeterlilik = bolumDetail.UpdatedBolum.Bolum_Yeterlilik;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //Öncelikle foreign keyleri silmelisin.
        public ActionResult Sil(int id)
        {
            var silinecekBolum = db.Bolum.Find(id);
            if (silinecekBolum == null)
                return HttpNotFound();
            Bolum_Kazanim silinecekBolumKazanim = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).FirstOrDefault();
            db.Bolum_Kazanim.Remove(silinecekBolumKazanim);
            Dersler silinecekDersBolum = db.Dersler.Where(s => s.Bolum_Id == id).FirstOrDefault();
            Acilan_Dersler silinecekAcilanBolum = db.Acilan_Dersler.Where(s => s.Bolum_Id == id).FirstOrDefault();
            if (silinecekDersBolum!=null && silinecekAcilanBolum==null)
            {
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == silinecekDersBolum.Ders_Kodu).FirstOrDefault();
                db.Dersler.Remove(silinecekDersBolum);
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Bolum.Remove(silinecekBolum);
                db.SaveChanges();
            }
            else if(silinecekDersBolum!=null && silinecekAcilanBolum!=null)
            {
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == silinecekDersBolum.Ders_Kodu).FirstOrDefault();
                db.Dersler.Remove(silinecekDersBolum);
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Acilan_Dersler.Remove(silinecekAcilanBolum);
                db.Bolum.Remove(silinecekBolum);
                db.SaveChanges();
            }
            else
            {
                db.Bolum.Remove(silinecekBolum);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
    }
    public ActionResult Kazanim(int id)
    {
        var model = db.Bolum_Kazanim.Where(s => s.Bolum_Id == id).ToList();
        return View("Kazanim", model);
    }
}
}