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
    public class KullaniciController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            KullaniciDetailViewModel kullaniciDetailViewModel = new KullaniciDetailViewModel();
            kullaniciDetailViewModel.KullaniciList = (from a in db.Kullanici join b in db.Roller on a.Rol_Id equals b.Rol_Id select new KullaniciDetail { Sicil_No = a.Sicil_No, Ad = a.Ad, Soyad = a.Soyad, Sifre = a.Sifre, RolAdi = b.Rol_Adi, Rol_Id = a.Rol_Id }).ToList();
            return View("Index", kullaniciDetailViewModel);
        }
        public ActionResult Yeni()
        {
            KullaniciViewModel model = new KullaniciViewModel()
            {
                Roller = db.Roller.ToList()
            };
            return View("Yeni", model);
        }
        public ActionResult KullaniciEkle(Kullanici kullanici)
        {
            var checkSicilNo = db.Kullanici.Where(x => x.Sicil_No == kullanici.Sicil_No).SingleOrDefault();
            var model = new KullaniciViewModel();
            model.Roller = db.Roller.ToList();
            if (checkSicilNo == null)
            {
                if (!ModelState.IsValid)
                {
                    return View("Yeni", model);
                }
                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                ViewBag.Mesaj = "Ekleme işlemi başarılı...";
            }
            else if (checkSicilNo.Sicil_No == kullanici.Sicil_No)
            {
                ViewBag.Mesaj = "Hata, eklemeye çalıştığınız Kişi sistemde mevcut...";
            }
            return View("Yeni", model);
        }
        public ActionResult Guncelle(int id)
        {
            KullaniciDetailViewModel kullaniciDetailViewModel = new KullaniciDetailViewModel();
            kullaniciDetailViewModel.updatedKullanici = (from a in db.Kullanici join b in db.Roller on a.Rol_Id equals b.Rol_Id where a.Sicil_No == id.ToString() select new KullaniciDetail { Sicil_No = a.Sicil_No, Ad = a.Ad, Soyad = a.Soyad, RolAdi = b.Rol_Adi, Rol_Id = a.Rol_Id, Sifre = a.Sifre }).First();
            kullaniciDetailViewModel.Roller = db.Roller.ToList();
            return View("Guncelle", kullaniciDetailViewModel);
        }
        [HttpPost]
        public ActionResult Guncelle(KullaniciDetailViewModel kullaniciDetailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var updatedUser = db.Kullanici.SingleOrDefault(x => x.Sicil_No == kullaniciDetailViewModel.updatedKullanici.Sicil_No);
                updatedUser.Sicil_No = kullaniciDetailViewModel.updatedKullanici.Sicil_No;
                updatedUser.Ad = kullaniciDetailViewModel.updatedKullanici.Ad;
                updatedUser.Soyad = kullaniciDetailViewModel.updatedKullanici.Soyad;
                updatedUser.Rol_Id = kullaniciDetailViewModel.updatedKullanici.Rol_Id;
                updatedUser.Sifre = kullaniciDetailViewModel.updatedKullanici.Sifre;
                db.Entry(updatedUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Sil(int id)
        {
            var silinecekKullanici = db.Kullanici.Find(id.ToString());
            Acilan_Dersler kullaniciAcilanDers = db.Acilan_Dersler.Where(s => s.Sicil_No == id.ToString()).FirstOrDefault();
            if (silinecekKullanici == null)
                return HttpNotFound();
            if (silinecekKullanici != null && kullaniciAcilanDers != null)
            {
                db.Acilan_Dersler.Remove(kullaniciAcilanDers);
                db.Kullanici.Remove(silinecekKullanici);
                db.SaveChanges();
            }
            else if (silinecekKullanici != null && kullaniciAcilanDers == null)
            {
                db.Kullanici.Remove(silinecekKullanici);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}