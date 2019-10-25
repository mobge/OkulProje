using Proje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proje.ViewModels
{
    public class BolumViewModel
    {
        public int Bolum_Id { get; set; }
        [Display(Name = "Bölüm Adı")]
        [Required(ErrorMessage = "Bölüm Adı boş geçilemez...")]
        public string Bolum_Adi { get; set; }
        public int Bolum_Kazanim_Id { get; set; }
        public string Bolum_Yeterlilik { get; set; }
        public string Fakulte_No { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
    }
    public class BolumDetailViewModel
    {
        public BolumDetail UpdatedBolum { get; set; }
        public IEnumerable<Fakulte> Fakulte { get; set; }
        public IEnumerable<Bolum> Bolum { get; set; }
    }
}