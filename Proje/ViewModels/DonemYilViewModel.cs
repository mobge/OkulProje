using Proje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class DonemYilViewModel
    {
        public int Donem_Id { get; set; }
        [Display(Name = "Dönem Adı")]
        [Required(ErrorMessage = "Dönem Adı boş geçilemez...")]
        public string Donem_Adi { get; set; }
        public IEnumerable<Donem> Donemler { get; set; }
    }
}