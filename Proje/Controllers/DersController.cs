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
            Ders_Kazanim silinecekDersKazanim = db.Ders_Kazanim.Where(s => s.Ders_Kodu == id).FirstOrDefault();
            if (silinecekDers == null)
                return HttpNotFound();
            db.Ders_Kazanim.Remove(silinecekDersKazanim);
            db.Dersler.Remove(silinecekDers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Kazanim(string id)
        {
            var model = db.Ders_Kazanim.Where(s => s.Ders_Kodu == id).ToList();
            return View("Kazanim", model);
        }

        //soru kiyaslama 
        public ActionResult Kiyasla()
        {
            DersViewModel model = new DersViewModel();
            //Excel ve Text dosyalarının yolu
            string ogrenciTxt = @"C:\Users\Atakan\Desktop\Ogrenci.txt";
            string cevapTxt = @"C:\Users\Atakan\Desktop\Cevap.txt";
            //excel string excelXlsx = @"C:\Users\aksoy\Desktop\birExcel.xlsx";

            //okuma işlemi
            string[] ogrenciListe = System.IO.File.ReadAllLines(ogrenciTxt, Encoding.GetEncoding("Windows-1254"));
            string[] cevapListe = System.IO.File.ReadAllLines(cevapTxt, Encoding.GetEncoding("Windows-1254"));
            char[] dogruCevap = cevapListe[0].ToCharArray();
            char[] dogruCevap2 = cevapListe[1].ToCharArray();
            char[] dogruCevap3 = cevapListe[2].ToCharArray();

            //gerekli string dizileri
            int sayici = 0;
            sayici = ogrenciListe.Length;

            model.ad = new string[sayici];
            model.soyad = new string[sayici];
            model.numara = new string[sayici];
            model.grup = new string[sayici];
            model.ogrCevap = new string[sayici];
            model.ogrPuan = new string[sayici];

            //Excell açma ve worksheet
            //_Application excel = new _Excel.Application();
            //Workbook wb;
            //Worksheet ws;
            //wb = excel.Workbooks.Open(excelXlsx);
            //ws = wb.Worksheets[1];

            //satirlari ayırma işlemi
            for (int listeSayac = 0; listeSayac < sayici; listeSayac++)
            {
                int sayac = 0;
                int sayac2 = 0;
                string ad1 = " ";
                string ad2 = " ";
                string ad3 = " ";
                string adi = " ";
                string soyadi = " ";
                string numarasi = " ";
                string cevaplar = " ";

                char[] letters = ogrenciListe[listeSayac].ToCharArray();

                if (letters[24] != ' ')
                {
                    numarasi = new string(letters, 24, 9);
                }

                //özel durum
                //Excel ilk rakamı 0 olan numaralarda hata veriyor


                if (letters[33] != ' ')
                {
                    cevaplar = new string(letters, 34, 31);
                }

                for (int i = 0; i < 24; i++)
                {
                    if (letters[i] != ' ' || Char.IsDigit(letters[i + 1]))
                    {
                        sayac = i;
                        i = 24;
                    }
                }
                if (sayac != 23)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        sayac = i;
                        if (letters[i] == ' ' || Char.IsDigit(letters[i + 1]))
                        {
                            ad1 = new string(letters, 0, i);
                            i = 24;
                        }

                    }
                    for (int i = sayac; i < 24; i++)
                    {
                        if (letters[i] != ' ' || Char.IsDigit(letters[i + 1]))
                        {
                            sayac = i;
                            i = 24;
                        }
                    }
                    if (sayac != 23)
                    {
                        for (int i = sayac; i < 24; i++)
                        {
                            sayac2++;
                            if (letters[i] == ' ' || Char.IsDigit(letters[i + 1]))
                            {
                                ad2 = new string(letters, sayac, sayac2 - 1);
                                sayac = i;
                                sayac2 = 0;
                                i = 24;
                            }
                        }
                        for (int i = sayac; i < 24; i++)
                        {
                            if (letters[i] != ' ' || Char.IsDigit(letters[i + 1]))
                            {
                                sayac = i;
                                i = 24;
                            }
                        }
                        if (sayac != 23)
                        {
                            for (int i = sayac; i < 24; i++)
                            {
                                sayac2++;
                                if (letters[i] == ' ' || Char.IsDigit(letters[i + 1]))
                                {
                                    ad3 = new string(letters, sayac, sayac2 - 1);
                                    sayac = i;
                                    sayac2 = 0;
                                    i = 24;
                                }
                            }
                        }
                    }
                }
                if (ad3 != " ")
                {
                    adi = ad1 + " " + ad2;
                    soyadi = ad3;
                }
                else if (ad1 == " ")
                {
                    adi = " ";
                    soyadi = " ";
                }
                else if (ad2 == " ")
                {
                    adi = ad1;
                    soyadi = " ";
                }
                else
                {
                    adi = ad1;
                    soyadi = ad2;
                }

                //cevapları kıyasla ve kaydet
                char[] gruplar = new char[4];
                gruplar[0] = letters[33];
                gruplar[1] = dogruCevap[0];
                gruplar[2] = dogruCevap2[0];
                gruplar[3] = dogruCevap3[0];

                double cevapSayac = 0;
                int excelSayac = 2;
                for (int i = 1; i < 31; i++)
                {
                    excelSayac++;
                    if (gruplar[0] == gruplar[1])
                    {
                        if (letters[33 + i] == dogruCevap[i])
                        {
                            cevapSayac++;
                            double puan = (cevapSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            model.ogrPuan[listeSayac] = puan.ToString();
                            //excel ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        //excel else { ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
                    }
                    else if (gruplar[0] == gruplar[2])
                    {
                        if (letters[33 + i] == dogruCevap2[i])
                        {
                            cevapSayac++;
                            double puan = (cevapSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            model.ogrPuan[listeSayac] = puan.ToString();
                            //excel ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        //excel else {  ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
                    }
                    else if (gruplar[0] == gruplar[3])
                    {
                        if (letters[33 + i] == dogruCevap3[i])
                        {
                            cevapSayac++;
                            double puan = (cevapSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            model.ogrPuan[listeSayac] = puan.ToString();
                            //excel ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        //excel else {  ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
                    }
                    else { model.ogrPuan[listeSayac] = 0.ToString(); }
                }
                //Excel
                //ws.Cells[listeSayac + 2, 1].Value2 = numarasi;
                //ws.Cells[listeSayac + 2, 2].Value2 = adi + " " +soyadi;

                model.grup[listeSayac] = gruplar[0].ToString();
                model.ad[listeSayac] = adi;
                model.soyad[listeSayac] = soyadi;
                model.numara[listeSayac] = numarasi;
                model.ogrCevap[listeSayac] = cevaplar;

            }

            //Eski kod
            //sonuç karşılaştırma 
            /* int puan = 0;
             for(int i = 0; i < 10; i++)
             {
                 if (ogrenciListe[i + 3] == cevapListe[i])
                 {
                     puan++;
                 }
             }
             //Viewbag oluşturma
             var ogrListe = ogrenciListe;
             var cvpListe = cevapListe;
             var p = puan * 10;
             ViewBag.puan = p;
             ViewBag.ogrenci = ogrenciListe;
             ViewBag.cevap = cvpListe;
             */

            //Excell Kaydet ve kapat
            //wb.Save();
            //wb.Close();

            //Viewbag oluşturma
            var name = model.ad;
            var surname = model.soyad;
            var number = model.numara;
            var group = model.grup;
            var ans = model.ogrCevap;
            var counter = sayici;
            var point = model.ogrPuan;

            //View a gidecek bilgiler
            ViewBag.grup = group;
            ViewBag.sayici = counter;
            ViewBag.ad = name;
            ViewBag.soyad = surname;
            ViewBag.numara = number;
            ViewBag.cevap = ans;
            ViewBag.puan = point;

            return View(model);
        }
        [HttpPost]
        public ActionResult Excel(DersViewModel model)
        {
            return View();
        }
    }
}