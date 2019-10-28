namespace Proje.Models
{
    public partial class DersAtamaDetail : Acilan_Dersler
    {
        public string Fakulte_Adi { get; set; }
        public string Bolum_Adi { get; set; }
        public string Ders_Adi { get; set; }
        public string KullaniciAd { get; set; }
        public string Donem_Adi { get; set; }
        public int Sinif_No { get; set; }
    }
    public partial class DersADetail : Acilan_Dersler
    {
        public string Fakulte_Adi { get; set; }
        public string Bolum_Adi { get; set; }
        public string Ders_Adi { get; set; }
        public string KullaniciAd { get; set; }
        public string Donem_Adi { get; set; }
        public int Sinif_No { get; set; }
    }
}