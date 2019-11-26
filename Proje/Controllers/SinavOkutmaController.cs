using Proje.Models;
using Proje.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Excell
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.Data.Entity;

namespace Proje.Controllers
{
    public class SinavOkutmaController : Controller
    {
        string path;
        string path1;
        okulEntities db = new okulEntities();
        public ActionResult Index(SinavOkutmaViewModel sinav)
        {
            SinavOkutmaViewModel model = new SinavOkutmaViewModel()
            {
                Donem = db.Donem.ToList(),
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.ToList(), //Where(s => s.Fakulte_No == sinav.Fakulte_No).ToList(),
                Dersler = db.Dersler.ToList(), // Where(s => s.Bolum_Id == sinav.Bolum_ıd).Where(s => s.Fakulte_No == sinav.Fakulte_No).ToList(),
                SinavTuru = db.Sınav_Turu.ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult SinavOkut(SinavOkutmaViewModel sinav, HttpPostedFileBase cevapanahtari, HttpPostedFileBase sinavsonuclari)
        {

            SinavOkutmaViewModel model = new SinavOkutmaViewModel()
            {
                Donem = db.Donem.ToList(),
                Fakulte = db.Fakulte.ToList(),
                Bolum = db.Bolum.ToList(), //Where(s => s.Fakulte_No == sinav.Fakulte_No).ToList(),
                Dersler = db.Dersler.ToList(), // Where(s => s.Bolum_Id == sinav.Bolum_ıd).Where(s => s.Fakulte_No == sinav.Fakulte_No).ToList(),
                SinavTuru = db.Sınav_Turu.ToList(),
            };
            var FileName = Path.GetFileName(cevapanahtari.FileName);
            path = Path.Combine(Server.MapPath("~/cevapanahtari"), FileName);
            cevapanahtari.SaveAs(path);
            FileName = Path.GetFileName(sinavsonuclari.FileName);
            path1 = Path.Combine(Server.MapPath("~/sinavsonuclari"), FileName);
            sinavsonuclari.SaveAs(path1);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Excel ve Text dosyalarının yolu
                string ogrenciTxt = path1;
                string cevapTxt = path;
                string excelXlsx = @"C:\Users\Atakan\Desktop\birExcel.xlsx";

                //okuma işlemi
                //string[] ogrenciListe = System.IO.File.ReadAllLines(ogrenciTxt, Encoding.GetEncoding("Windows-1254"));
                //string[] cevapListe = System.IO.File.ReadAllLines(cevapTxt, Encoding.GetEncoding("Windows-1254"));
                string[] ogrenciListe = System.IO.File.ReadAllLines(ogrenciTxt);
                string[] cevapListe = System.IO.File.ReadAllLines(cevapTxt);
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
                _Application excel = new _Excel.Application();
                Workbook wb;
                Worksheet ws;
                wb = excel.Workbooks.Open(excelXlsx);
                ws = wb.Worksheets[1];

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

                    //cevapları kıyasla ve gonder
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
                                //   ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                            }
                            // else { ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
                        }
                        else if (gruplar[0] == gruplar[2])
                        {
                            if (letters[33 + i] == dogruCevap2[i])
                            {
                                cevapSayac++;
                                double puan = (cevapSayac * 3.33);
                                if (puan > 99) { puan = 100; }
                                model.ogrPuan[listeSayac] = puan.ToString();
                                //    ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                            }
                            //  else {  ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
                        }
                        else if (gruplar[0] == gruplar[3])
                        {
                            if (letters[33 + i] == dogruCevap3[i])
                            {
                                cevapSayac++;
                                double puan = (cevapSayac * 3.33);
                                if (puan > 99) { puan = 100; }
                                model.ogrPuan[listeSayac] = puan.ToString();
                                //  ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                            }
                            // else {  ws.Cells[listeSayac + 2, excelSayac].Value2 = 0; }
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
                var eklenecekSinav = new Sinav_Sonuclari();
                //Db'de Sinav Sonuçları adlı tabloya kaydetme işlemi
                //eklenecekSinav.Ders_Kodu = sinav.Ders_Kodu;
                //eklenecekSinav.Fakulte_No = sinav.Fakulte_No;
                //eklenecekSinav.Bolum_ıd = sinav.Bolum_ıd;
                //eklenecekSinav.Donem_Id = sinav.Donem_Id;
                //eklenecekSinav.Sicil_No = sinav.Sicil_No;
                //eklenecekSinav.Sinav_Turu_Id = sinav.Sinav_Turu_Id;
                //eklenecekSinav.Sonuc = sinav.Sonuc;
                //db.Entry(eklenecekSinav).State = EntityState.Added;
                //db.SaveChanges();
                return View("Kiyasla", model);
                
            }
        }
        [HttpPost]
        public ActionResult Excel(SinavOkutmaViewModel model)
        {
            string cevapTxt = path;
            string excelXlsx = @"C:\Users\Atakan\Desktop\birExcel.xlsx";
            _Application excel = new _Excel.Application();
            Workbook wb;
            Worksheet ws, ws2, ws4;
            wb = excel.Workbooks.Open(excelXlsx);
            ws = wb.Worksheets[1];
            ws2 = wb.Worksheets[2];
            ws4 = wb.Worksheets[4];

            //cevapları tekrar kıyasla ve excele kaydet
            string[] cevapListe = System.IO.File.ReadAllLines(cevapTxt);
            char[] dogruCevap = cevapListe[0].ToCharArray();
            char[] dogruCevap2 = cevapListe[1].ToCharArray();
            char[] dogruCevap3 = cevapListe[2].ToCharArray();

            char[] gruplar = new char[4];
            double[] soruA = new double[30];
            double[] soruB = new double[30];
            double[] soruC = new double[30];
            int[] grupSayac = new int[3];

            //Grup sayacı
            for (int i = 0; i < model.ad.Length; i++)
            {
                if (model.grup[i] == dogruCevap[0].ToString()) { grupSayac[0]++; }
                if (model.grup[i] == dogruCevap2[0].ToString()) { grupSayac[1]++; }
                if (model.grup[i] == dogruCevap3[0].ToString()) { grupSayac[2]++; }
            }

            for (int listeSayac = 0; listeSayac < model.ad.Length; listeSayac++)
            {

                char[] cevap = model.ogrCevap[listeSayac].ToCharArray();
                char[] grup = model.grup[listeSayac].ToCharArray();
                gruplar[0] = grup[0];
                gruplar[1] = dogruCevap[0];
                gruplar[2] = dogruCevap2[0];
                gruplar[3] = dogruCevap3[0];

                int dogruSayac = 0;
                int excelSayac = 2;
                double puan = 0;

                for (int cevapSayac = 0; cevapSayac < 30; cevapSayac++)
                {
                    excelSayac++;
                    if (gruplar[0] == gruplar[1])
                    {

                        if (cevap[cevapSayac] == dogruCevap[cevapSayac + 1])
                        {
                            soruA[cevapSayac] = soruA[cevapSayac] + 1;
                            dogruSayac++;
                            puan = (dogruSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            ws.Cells[listeSayac + 2, 33].Value2 = puan;
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        else
                        {
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 0;
                            if (puan == 0) { ws.Cells[listeSayac + 2, 33].Value2 = 0; }
                        }
                    }
                    if (gruplar[0] == gruplar[2])
                    {

                        if (cevap[cevapSayac] == dogruCevap2[cevapSayac + 1])
                        {

                            soruB[cevapSayac] = soruB[cevapSayac] + 1;
                            dogruSayac++;
                            puan = (dogruSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            ws.Cells[listeSayac + 2, 33].Value2 = puan;
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        else
                        {
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 0;
                            if (puan == 0) { ws.Cells[listeSayac + 2, 33].Value2 = 0; }
                        }
                    }
                    if (gruplar[0] == gruplar[3])
                    {

                        if (cevap[cevapSayac] == dogruCevap3[cevapSayac + 1])
                        {
                            soruC[cevapSayac] = soruC[cevapSayac] + 1;
                            dogruSayac++;
                            puan = (dogruSayac * 3.33);
                            if (puan > 99) { puan = 100; }
                            ws.Cells[listeSayac + 2, 33].Value2 = puan;
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 3.33;
                        }
                        else
                        {
                            ws.Cells[listeSayac + 2, excelSayac].Value2 = 0;
                            if (puan == 0) { ws.Cells[listeSayac + 2, 33].Value2 = 0; }
                        }
                    }

                }

            }
            //Excel Soru Ortalama Tablosu
            ws2.Cells[1, 1].Value2 = "A Grubu";
            ws2.Cells[1, 2].Value2 = "Ortalaması(Puan)";
            ws2.Cells[1, 3].Value2 = "Başarımı (%) {Ort P./Tam P.}x100 ";
            for (int i = 0; i < 30; i++)
            {
                ws2.Cells[i + 2, 2].Value2 = (soruA[i] * 3.33) / grupSayac[0];
                ws2.Cells[i + 2, 1].Value2 = "Soru" + (i + 1).ToString();
            }
            //Excel Kazanım Tablosu
            int sutunSayac = 0;
            int satirsayac = 2;
            //A grubu
            for (int i = 0; i < model.IsSelected.Length; i++)
            {
                if (model.IsSelected[i] == true)
                {
                    i++;
                    sutunSayac++;
                    ws4.Cells[satirsayac, sutunSayac + 1].Value2 = "X";
                    if (sutunSayac == 5) { satirsayac++; sutunSayac = 0; }
                }
                else
                {
                    sutunSayac++;
                    if (sutunSayac == 5) { satirsayac++; sutunSayac = 0; }
                }
            }

            for (int i = 0; i < model.ad.Length; i++)
            {
                ws.Cells[i + 2, 1].Value2 = "'" + model.numara[i];
                ws.Cells[i + 2, 2].Value2 = model.ad[i] + " " + model.soyad[i];
            }
            for (int i = 3; i < 33; i++) { ws.Cells[1, i].Value2 = "Soru" + (i - 2).ToString(); }

            //A grubu
            for (int i = 1; i < 6; i++) { ws4.Cells[1, i + 1].Value2 = "Kazanım" + i.ToString(); }
            for (int i = 1; i < 31; i++) { ws4.Cells[i + 1, 1].Value2 = "Soru" + i.ToString(); }
            ws4.Cells[1, 1].Value2 = "A";

            ws.Cells[1, 1].Value2 = "Öğrenci No";
            ws.Cells[1, 2].Value2 = "Adı/Soyadı";
            ws.Cells[1, 33].Value2 = "Puan";

            ws.Rows[1].Font.Size = 14;   //1. satır font boyutu
            ws.Columns.AutoFit();  //otomatik hücre boyutlandır
            ws2.Rows[1].Font.Size = 14;
            ws2.Columns.AutoFit();
            ws4.Rows[1].Font.Size = 14;   //1. satır font boyutu
            ws4.Columns[1].Font.Size = 14; //1.sutun font boyutu
            ws4.Columns.AutoFit();  //otomatik hücre boyutlandır
            ws.Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; //ortala
            ws2.Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft; //sola yapıştır
            ws4.Columns.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; //ortala
            wb.Save();
            wb.Close();
            return View();
        }
    }
}