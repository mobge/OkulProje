using Proje.Models;
using Proje.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class DerslerimController : Controller
    {
        okulEntities db = new okulEntities();
        public ActionResult Index()
        {
            var sicilNo = (string)Session["sicilNo"];
            DerslerimDetailViewModel model = new DerslerimDetailViewModel();
            model.derslerimDetail = (from a in db.Sinav_Sonuclari join b in db.Kullanici on a.Sicil_No equals b.Sicil_No join c in db.Fakulte on a.Fakulte_No equals c.Fakulte_No join d in db.Bolum on a.Bolum_ıd equals d.Bolum_Id join e in db.Dersler on a.Ders_Kodu equals e.Ders_Kodu join f in db.Donem on a.Donem_Id equals f.Donem_Id join h in db.Sınav_Turu on a.Sinav_Turu_Id equals h.Id where a.Sicil_No == sicilNo select new DerslerimDetail { Ad = b.Ad, Fakulte_Adi = c.Fakulte_Adi, Bolum_Adi = d.Bolum_Adi, Ders_Kodu = e.Ders_Kodu, Donem_Adi = f.Donem_Adi, Ders_Adi=e.Ders_Adi, Sinav_Turu_Adi = h.Sinav_Turu, Sonuc=a.Sonuc, Id=a.Id }).ToList();
            return View(model);
        }
        public ActionResult OpenExcel(int id)
        {
            var sonuc = db.Sinav_Sonuclari.Where(s => s.Id == id).Select(s => s.Sonuc).FirstOrDefault();
            string fileName = sonuc;
            string path = Path.Combine(Server.MapPath("~/excel"), fileName);
            string mySheet = path;
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbooks books = excelApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook sheet = books.Open(mySheet);
            return RedirectToAction("Index");
        }
    }
}