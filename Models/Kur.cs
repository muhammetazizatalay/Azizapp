namespace GorselOdev.Models
{
    public class Kur
    {
        public string Code { get; set; }   // Örn: USD
        public string Title { get; set; }  // Örn: ABD DOLARI
        public string Buying { get; set; } // Alýþ fiyatý
        public string Selling { get; set; } // Satýþ fiyatý
        public string Change { get; set; }  // Deðiþim oraný
    }
}