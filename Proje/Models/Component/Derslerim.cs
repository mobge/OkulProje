namespace Proje.Models
{
    public partial class DerslerimDetail : Sinav_Sonuclari
    {
        public string Fakulte_Adi { get; set; }
        public string Bolum_Adi { get; set; }
        public string Ders_Ogrenme { get; set; }
        public int Ders_Kazanim_Id { get; set; }
        public string Donem_Adi { get; set; }
        public string Ad { get; set; }
        public string Ders_Adi { get; set; }
        public string Sinav_Turu_Adi { get; set; }
    }
}