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
    public class DersAtamaController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index(DersAtamaViewModel dersler)
        {
            DersAtamaViewModel model = new DersAtamaViewModel()
            {
                Donem = db.Donem.ToList(),
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                DersAtama = db.Acilan_Dersler.Where(s => s.Bolum_Id == dersler.Bolum_Id).Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
            };
            return View(model);
        }
        public ActionResult Ekle(DersAtamaViewModel dersler)
        {
            DersAtamaViewModel model = new DersAtamaViewModel()
            {
                Donem = db.Donem.ToList(),
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Dersler = db.Dersler.Where(s => s.Bolum_Id == dersler.Bolum_Id).Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Kullanici = db.Kullanici.Where(s => s.Rol_Id == 2).ToList(),
                Siniflar = db.Siniflar.ToList(),
            };
            return View("Ekle", model);
        }
        [HttpPost]
        public ActionResult EkleAtama(DersAtamaViewModel dersler)
        {
            var checkDersKodu = db.Acilan_Dersler.Where(x => x.Ders_Kodu == dersler.Ders_Kodu).SingleOrDefault();
            DersAtamaViewModel model = new DersAtamaViewModel()
            {
                Donem = db.Donem.ToList(),
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Dersler = db.Dersler.Where(s => s.Bolum_Id == dersler.Bolum_Id).Where(s => s.Fakulte_No == dersler.Fakulte_No).ToList(),
                Kullanici = db.Kullanici.Where(s => s.Rol_Id == 2).ToList(),
                Siniflar = db.Siniflar.ToList(),
            };
            if (checkDersKodu == null)
            {
                if (!ModelState.IsValid)
                {
                    return View("Ekle", model);
                }
                //farklı tabloya kendine ait bölümleri tek tek eklemek için yaptığım yöntem.
                var atanacakDers = new Acilan_Dersler();
                atanacakDers.Donem_Id = dersler.Donem_Id;
                atanacakDers.Fakulte_No = dersler.Fakulte_No;
                atanacakDers.Bolum_Id = dersler.Bolum_Id;
                atanacakDers.Ders_Kodu = dersler.Ders_Kodu;
                atanacakDers.Sicil_No = dersler.Sicil_No;
                atanacakDers.Sinif = dersler.Sinif;
                db.Entry(atanacakDers).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (checkDersKodu.Ders_Kodu == dersler.Ders_Kodu)
            {
                ViewBag.Mesaj = "Hata, Seçtiğiniz ders başka bir öğretim görevlisine atanmış...";
            }
            return View("Ekle", model);
        }
        public ActionResult Guncelle(int id)
        {
            DersAtamaDetailViewModel dersAtamaDetailViewModel = new DersAtamaDetailViewModel();
            dersAtamaDetailViewModel.UpdatedDersAtama = (from a in db.Acilan_Dersler join b in db.Bolum on a.Bolum_Id equals b.Bolum_Id join c in db.Fakulte on a.Fakulte_No equals c.Fakulte_No join d in db.Kullanici on a.Sicil_No equals d.Sicil_No join e in db.Dersler on a.Ders_Kodu equals e.Ders_Kodu join f in db.Donem on a.Donem_Id equals f.Donem_Id join h in db.Siniflar on a.Sinif equals h.Sinif_Id where a.Id == id select new DersAtamaDetail { Bolum_Id = a.Bolum_Id, Fakulte_No = a.Fakulte_No, Ders_Kodu=a.Ders_Kodu, Sicil_No=a.Sicil_No, Donem_Id=a.Donem_Id, Sinif_No=h.Sinif_No, Id=a.Id}).FirstOrDefault();
            dersAtamaDetailViewModel.Fakulte = db.Fakulte.ToList();
            dersAtamaDetailViewModel.Bolum = db.Bolum.ToList();
            dersAtamaDetailViewModel.Dersler = db.Dersler.ToList();
            dersAtamaDetailViewModel.Kullanici = db.Kullanici.Where(s => s.Rol_Id == 2).ToList();
            dersAtamaDetailViewModel.Siniflar = db.Siniflar.ToList();
            dersAtamaDetailViewModel.Donem = db.Donem.ToList();
            return View("Guncelle", dersAtamaDetailViewModel);
        }
        [HttpPost]
        public ActionResult Guncelle(DersAtamaDetailViewModel dersAtamaDetail)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var guncellenecekDersAtama = db.Acilan_Dersler.Find(dersAtamaDetail.UpdatedDersAtama.Id);
                if (guncellenecekDersAtama == null)
                    return HttpNotFound();
                guncellenecekDersAtama.Donem_Id = dersAtamaDetail.UpdatedDersAtama.Donem_Id;
                guncellenecekDersAtama.Fakulte_No = dersAtamaDetail.UpdatedDersAtama.Fakulte_No;
                guncellenecekDersAtama.Bolum_Id = dersAtamaDetail.UpdatedDersAtama.Bolum_Id;
                guncellenecekDersAtama.Ders_Kodu = dersAtamaDetail.UpdatedDersAtama.Ders_Kodu;
                guncellenecekDersAtama.Sicil_No = dersAtamaDetail.UpdatedDersAtama.Sicil_No;
                guncellenecekDersAtama.Sinif = dersAtamaDetail.UpdatedDersAtama.Sinif;
                db.SaveChanges();
                return View("Guncelle");
            }
        }
        public ActionResult Sil(int id)
        {
            var silinecekDersAtama = db.Acilan_Dersler.Find(id);
            if (silinecekDersAtama == null)
                return HttpNotFound();
            db.Acilan_Dersler.Remove(silinecekDersAtama);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}