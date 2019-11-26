using Proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class SinavOkutmaViewModel
    {
        public int Id { get; set; }
        public string Fakulte_No { get; set; }
        public int Bolum_ıd { get; set; }
        public string Ders_Kodu { get; set; }
        public int Donem_Id { get; set; }
        public string Sicil_No { get; set; }
        public string Sonuc { get; set; }
        public int Sinav_Turu_Id { get; set; }
        public IEnumerable<Donem> Donem { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
        public IEnumerable<Dersler> Dersler { get; set; }
        public IEnumerable<Sınav_Turu> SinavTuru { get; set; }
        //kiyaslama için oluşturulan stringlerin modelde tanımı.
        public string[] ad { get; set; }
        public string[] soyad { get; set; }
        public string[] numara { get; set; }
        public string[] grup { get; set; }
        public string[] ogrCevap { get; set; }
        public string[] ogrPuan { get; set; }
        public bool[] IsSelected { get; set; }
    }
}