namespace Proje.Models
{
    public partial class DersDetail : Dersler
    {
        public string Fakulte_Adi { get; set; }
        public string Bolum_Adi { get; set; }
        public string Ders_Ogrenme { get; set; }
        public int Ders_Kazanim_Id { get; set; }
    }
}