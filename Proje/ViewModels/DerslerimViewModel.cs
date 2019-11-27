using Proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class DerslerimViewModel
    {
        public int Id { get; set; }
        public string Fakulte_No { get; set; }
        public int Bolum_ıd { get; set; }
        public string Ders_Kodu { get; set; }
        public int Donem_Id { get; set; }
        public string Sicil_No { get; set; }
        public string Sonuc { get; set; }
        public int Sinav_Turu_Id { get; set; }

    }
    public class DerslerimDetailViewModel
    {
        public IEnumerable<DerslerimDetail> derslerimDetail { get; set; }
    }
}