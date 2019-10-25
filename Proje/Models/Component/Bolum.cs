namespace Proje.Models
{
    public partial class BolumDetail : Bolum
    {
        public string Fakulte_Adi { get; set; }
        public string Bolum_Yeterlilik { get; set; }
        public int Bolum_Kazanim_Id { get; set; }
    }
}