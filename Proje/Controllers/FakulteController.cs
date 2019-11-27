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
    public class FakulteController : Controller
    {      
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            var fakulteModel = db.Fakulte.ToList();
            return View("Index", fakulteModel);
        }
        public ActionResult Ekle()
        {
            return View("Ekle");
        }
        [HttpPost]
        public ActionResult Ekle(Fakulte fakulte)
        {
            var checkFakulte = db.Fakulte.Where(x => x.Fakulte_No == fakulte.Fakulte_No).SingleOrDefault();
            if (checkFakulte == null)
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                db.Fakulte.Add(fakulte);
                db.SaveChanges();
                ViewBag.Mesaj = "Ekleme işlemi başarılı...";
            }
            else if (checkFakulte.Fakulte_No == fakulte.Fakulte_No)
            {
                ViewBag.Mesaj = "Hata, eklemeye çalıştığınız Kişi sistemde mevcut...";
            }
            return View("Ekle");
        }
        public ActionResult Guncelle(int id)
        {
            var fakulteModel = db.Fakulte.Find(id.ToString());
            if (fakulteModel == null)
                return HttpNotFound();
            return View("Guncelle", fakulteModel);
        }
        [HttpPost]
        public ActionResult Guncelle(Fakulte fakulte)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var updatedFakulte = db.Fakulte.SingleOrDefault(x => x.Fakulte_No == fakulte.Fakulte_No);
                updatedFakulte.Fakulte_No = fakulte.Fakulte_No;
                updatedFakulte.Fakulte_Adi = fakulte.Fakulte_Adi;
                db.Entry(updatedFakulte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Sil(int id)
        {
            var silinecekFakulte = db.Fakulte.Find(id.ToString());
            if (silinecekFakulte == null)
                return HttpNotFound();
            Bolum silinecekBolum = db.Bolum.Where(s => s.Fakulte_No == id.ToString()).FirstOrDefault();
            Dersler silinecekDersFakulte = db.Dersler.Where(s => s.Fakulte_No == id.ToString()).FirstOrDefault();
            Acilan_Dersler silinecekAcilanFakulte = db.Acilan_Dersler.Where(s => s.Fakulte_No == id.ToString()).FirstOrDefault();
            if(silinecekBolum!=null && silinecekDersFakulte!=null && silinecekAcilanFakulte!=null)
            {
                db.Bolum.Remove(silinecekBolum);
                Bolum_Kazanim silinecekBolumKazanim = db.Bolum_Kazanim.Where(s => s.Bolum_Id == silinecekBolum.Bolum_Id).FirstOrDefault();
                db.Bolum_Kazanim.Remove(silinecekBolumKazanim);
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == silinecekDersFakulte.Ders_Kodu).FirstOrDefault();
                db.Dersler.Remove(silinecekDersFakulte);
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Acilan_Dersler.Remove(silinecekAcilanFakulte);
                db.Fakulte.Remove(silinecekFakulte);
                db.SaveChanges();
            }
            else if(silinecekBolum!=null && silinecekDersFakulte!=null && silinecekAcilanFakulte==null)
            {
                db.Bolum.Remove(silinecekBolum);
                Bolum_Kazanim silinecekBolumKazanim = db.Bolum_Kazanim.Where(s => s.Bolum_Id == silinecekBolum.Bolum_Id).FirstOrDefault();
                db.Bolum_Kazanim.Remove(silinecekBolumKazanim);
                Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == silinecekDersFakulte.Ders_Kodu).FirstOrDefault();
                db.Dersler.Remove(silinecekDersFakulte);
                db.Ders_Kazanim.Remove(silinecekDersKazanim);
                db.Fakulte.Remove(silinecekFakulte);
                db.SaveChanges();
            }
            else if(silinecekBolum!=null && silinecekDersFakulte==null && silinecekAcilanFakulte==null)
            {
                db.Bolum.Remove(silinecekBolum);
                Bolum_Kazanim silinecekBolumKazanim = db.Bolum_Kazanim.Where(s => s.Bolum_Id == silinecekBolum.Bolum_Id).FirstOrDefault();
                db.Bolum_Kazanim.Remove(silinecekBolumKazanim);
                db.Fakulte.Remove(silinecekFakulte);
                db.SaveChanges();
            }
            else
            {
                db.Fakulte.Remove(silinecekFakulte);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
}