using Proje.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class KullaniciViewModel
    {
        [Display(Name = "Sicil Numarası")]
        [Required(ErrorMessage = "Sicil Numarası boş geçilemez...")]
        public string Sicil_No { get; set; }
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad boş geçilemez...")]
        public string Ad { get; set; }
        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad boş geçilemez...")]
        public string Soyad { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez...")]
        public string Sifre { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Rol boş geçilemez...")]
        public int Rol_Id { get; set; }
        public IEnumerable<Roller> Roller { get; set; }
    }
    public class KullaniciDetailViewModel
    {
        public List<KullaniciDetail> KullaniciList{ get; set; }
        public KullaniciDetail updatedKullanici { get; set; }
        public IEnumerable<Roller> Roller { get; set; }
    }
}