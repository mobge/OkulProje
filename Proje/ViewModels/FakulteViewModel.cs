using Proje.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proje.ViewModels
{
    public class FakulteViewModel
    {
        [Display(Name = "Fakülte Numarası")]
        [Required(ErrorMessage = "Fakülte Numarası boş geçilemez...")]
        public string Fakulte_No { get; set; }
        [Display(Name = "Fakülte Adı")]
        [Required(ErrorMessage = "Fakülte Adı boş geçilemez...")]
        public string Fakulte_Adi { get; set; }
    }
}