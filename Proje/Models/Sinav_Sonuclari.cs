//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sinav_Sonuclari
    {
        public int Id { get; set; }
        public int Donem_Yil_Id { get; set; }
        public string Fakulte_No { get; set; }
        public int Bolum_Id { get; set; }
        public string Ders_Kodu { get; set; }
        public string Ogrenci_No { get; set; }
        public int Sinav_Turu_Id { get; set; }
        public int Sinav_Grup_Id { get; set; }
        public string Sonuc { get; set; }
    
        public virtual Bolum Bolum { get; set; }
        public virtual Dersler Dersler { get; set; }
        public virtual Donem_Yil Donem_Yil { get; set; }
        public virtual Fakulte Fakulte { get; set; }
        public virtual Sınav_Turu Sınav_Turu { get; set; }
        public virtual Sinav_Grup Sinav_Grup { get; set; }
    }
}
