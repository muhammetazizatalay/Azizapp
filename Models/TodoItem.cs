using GorselOdev.Models;
namespace GorselOdev.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Baslik { get; set; } // XAML'daki {Binding Baslik} buraya bakar
        public int Tamamlandi { get; set; }
        public string Detay { get; set; }
        public string Tarih { get; set; }
        public string Saat { get; set; }
    }
}