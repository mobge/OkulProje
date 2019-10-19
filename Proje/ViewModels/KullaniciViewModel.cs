using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class KullaniciViewModel
    {
        [Display(Name = "Sicil Numarası")]
        [Required(ErrorMessage = "Sicil Numarası boş geçilemez...")]
        public string Sicil_No { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez...")]
        public string Sifre { get; set; }
        public int Rol_Id { get; set; }
    }
}