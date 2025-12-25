using Newtonsoft.Json;
using System.Collections.Generic;

namespace GorselOdev.Models
{
    // Haber kategorilerini (Gündem, Spor, Ekonomi vb.) ve linklerini gruplamak için kullanılır
    public class NewsCategory
    {
        public string Category { get; set; } // Kategorinin ekranda görünecek adı (Örn: Spor)
        public string Url { get; set; }      // O kategoriye ait RSS servis adresi

        public NewsCategory(string category, string url)
        {
            Category = category;
            Url = url;
        }
    }

    // JSON verisinin en dış katmanıdır. RSS servisinden gelen tüm veriyi kapsar.
    public class Root
    {
        public Rss rss { get; set; }
    }

    // RSS protokolünün standart kök dizinini temsil eder.
    public class Rss
    {
        public Channel channel { get; set; }
    }

    // Haber kanalını temsil eder; içinde haber başlığı, dili ve asıl haber listesi bulunur.
    public class Channel
    {
        // Haberlerin asıl listesi burada tutulur. CollectionView buna bağlanır.
        public List<Item> item { get; set; }
    }

    // Tek bir haber nesnesini temsil eder.
    public class Item
    {
        public string title { get; set; }      // Haberin başlığı
        public string pubDate { get; set; }    // Haberin yayınlanma tarihi
        public string link { get; set; }       // Haberin detay linki (Paylaşma işlemi için kullanılır)
        public Description description { get; set; } // Haber özeti
        public Enclosure enclosure { get; set; }     // Haberin ana görselini içeren yapı

        // JSON içindeki "media:content" etiketini C# içinde kullanabilmek için eşleştirme yapar
        [JsonProperty("media:content")]
        public MediaContent mediacontent { get; set; }
    }

    // Haberin özet kısmını tutar. RSS içinde bazen özel formatta (CDATA) gelir.
    public class Description
    {
        // XML içindeki CDATA (ham metin) bölümünü okumak için kullanılır
        [JsonProperty("#cdata-section")]
        public string cdatasection { get; set; }
    }

    // Haberin resim URL'sini tutan sınıftır.
    public class Enclosure
    {
        // JSON içindeki "@url" özelliğini C# değişkenine bağlar
        [JsonProperty("@url")]
        public string url { get; set; }
    }

    // Bazı RSS kaynaklarında resim bilgisi farklı bir isimle (MediaContent) gelebilir.
    public class MediaContent
    {
        [JsonProperty("@url")]
        public string url { get; set; }
    }
}