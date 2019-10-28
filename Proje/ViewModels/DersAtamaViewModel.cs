using Proje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class DersAtamaViewModel
    {
        public int Id { get; set; }
        public int Donem_Id { get; set; }
        public string Fakulte_No { get; set; }
        public int Bolum_Id { get; set; }
        public string Ders_Kodu { get; set; }
        public int Sinif { get; set; }
        public string Sicil_No { get; set; }
        public IEnumerable<Acilan_Dersler> DersAtama { get; set; }
        public IEnumerable<Donem> Donem { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
        public IEnumerable<Dersler> Dersler { get; set; }
        public IEnumerable<Kullanici> Kullanici { get; set; }
        public IEnumerable<Siniflar> Siniflar { get; set; }
    }
    public class DersAtamaDetailViewModel
    {
        public DersAtamaDetail UpdatedDersAtama { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
        public IEnumerable<Kullanici> Kullanici { get; set; }
        public IEnumerable<Siniflar> Siniflar { get; set; }
        public IEnumerable<Dersler> Dersler { get; set; }
        public IEnumerable<Donem> Donem { get; set; }

    }
}