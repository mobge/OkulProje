﻿using Proje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class DersViewModel
    {
        [Display(Name = "Ders Kodu")]
        [Required(ErrorMessage = "Ders Kodu boş geçilemez...")]
        public string Ders_Kodu { get; set; }
        [Display(Name = "Ders Adı")]
        [Required(ErrorMessage = "Ders Adı boş geçilemez...")]
        public string Ders_Adi { get; set; }
        public string Fakulte_No { get; set; }
        [Required(ErrorMessage = "Bölüm Seçiniz.")]
        public int Bolum_Id { get; set; }
        public int Ders_Kazanim_Id { get; set; }
        [Display(Name = "Ders Kazanımları")]
        [Required(ErrorMessage = "Ders Kazanımları boş geçilemez...")]
        public string Ders_Ogrenme { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
        public IEnumerable<Dersler> Dersler { get; set; }
        //kiyaslama için oluşturulan stringlerin modelde tanımı.
        public string[] ad { get; set; }
        public string[] soyad { get; set; }
        public string[] numara { get; set; }
        public string[] grup { get; set; }
        public string[] ogrCevap { get; set; }
        public string[] ogrPuan { get; set; }
        public bool[] IsSelected { get; set; }
    }
    public class DersDetailViewModel
    {
        public DersDetail UpdatedDers { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
    }
}