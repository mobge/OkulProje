using Proje.Models;
using Proje.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

//Excell
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Text;

namespace Proje.Controllers
{
    [Authorize(Roles = "1")]
    public class DersController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index(DersViewModel dersler)
        {
            DersViewModel model = new DersViewModel()
            {
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Dersler = db.Dersler.Where(s => s.Bolum_Id == dersler.Bolum_Id).Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList()
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
            return View("Ekle", model);
        }
        [HttpPost]
        public ActionResult Ekle(DersViewModel dersler)
        {
            var checkDersKodu = db.Dersler.Where(x => x.Ders_Kodu == dersler.Ders_Kodu).SingleOrDefault();
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
        public ActionResult Guncelle(string id)
        {
            DersDetailViewModel dersDetailViewModel = new DersDetailViewModel();
            dersDetailViewModel.UpdatedDers = (from a in db.Dersler join b in db.Bolum on a.Bolum_Id equals b.Bolum_Id where a.Ders_Kodu == id select new DersDetail { Ders_Kodu = a.Ders_Kodu, Ders_Adi = a.Ders_Adi, Bolum_Adi = b.Bolum_Adi, Bolum_Id = b.Bolum_Id, Fakulte_No = b.Fakulte_No, Ders_Kazanim_Id = a.Ders_Kazanim.Where(s => s.Ders_Kodu == id).Select(s => s.Id).FirstOrDefault(), Ders_Ogrenme = a.Ders_Kazanim.Where(s => s.Ders_Kodu == id).Select(s => s.Ders_Ogrenme).FirstOrDefault() }).FirstOrDefault();
            dersDetailViewModel.Fakulte = db.Fakulte.ToList();
            dersDetailViewModel.Bolum = db.Bolum.ToList();
            return View("Guncelle", dersDetailViewModel);
        }
        [HttpPost]
        public ActionResult Guncelle(DersDetailViewModel dersDetail)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var guncellenecekDers = db.Dersler.Find(dersDetail.UpdatedDers.Ders_Kodu);
                if (guncellenecekDers == null)
                    return HttpNotFound();
                guncellenecekDers.Fakulte_No = dersDetail.UpdatedDers.Fakulte_No;
                guncellenecekDers.Bolum_Id = dersDetail.UpdatedDers.Bolum_Id;
                guncellenecekDers.Ders_Kodu = dersDetail.UpdatedDers.Ders_Kodu;
                guncellenecekDers.Ders_Adi = dersDetail.UpdatedDers.Ders_Adi;
                var guncellenecekDersKazanim = db.Ders_Kazanim.Find(dersDetail.UpdatedDers.Ders_Kazanim_Id);
                guncellenecekDersKazanim.Ders_Ogrenme = dersDetail.UpdatedDers.Ders_Ogrenme;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //Öncelikle foreign keyleri silmelisin.
        public ActionResult Sil(string id)
        {
            var silinecekDers = db.Dersler.Find(id);
            Acilan_Dersler silinecekAcilanDers = db.Acilan_Dersler.Where(s => s.Ders_Kodu == id).FirstOrDefault();
            if (silinecekDers == null)
                return HttpNotFound();
            if (silinecekDers != null && silinecekAcilanDers != null)
            {
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == id).FirstOrDefault();
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Acilan_Dersler.Remove(silinecekAcilanDers);
                db.Dersler.Remove(silinecekDers);
                db.SaveChanges();
            }
            else if (silinecekDers != null && silinecekAcilanDers == null)
            {
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == id).FirstOrDefault();
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Dersler.Remove(silinecekDers);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Kazanim(string id)
        {
            var model = db.Ders_Kazanim.Where(s => s.Ders_Kodu == id).ToList();
            return View("Kazanim", model);
        }
    }
}