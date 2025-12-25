using System.Text.Json;

namespace GorselOdev.Services
{
    public static class WeatherService
    {
        // JSON dosyasýnýn kaydedileceði güvenli yol
        private static string _filePath = Path.Combine(FileSystem.AppDataDirectory, "cities.json");

        // Þehirleri JSON olarak kaydeder
        public static void SaveCities(List<string> cities)
        {
            // Veriyi JSON formatýna dönüþtür
            var json = JsonSerializer.Serialize(cities);
            // Dosyaya yaz (2 parametre ister: Yol ve Veri)
            File.WriteAllText(_filePath, json);
        }

        // Uygulama açýldýðýnda kayýtlý þehirleri geri yükler
        public static List<string> LoadCities()
        {
            // Eðer dosya yoksa varsayýlan þehirleri döndür
            if (!File.Exists(_filePath))
                return new List<string> { "BARTIN", "ANKARA", "ISTANBUL" };

            // HATANIN ÇÖZÜMÜ: File.ReadAllText kullanýldý (Dosyadan veriyi okur)
            string json = File.ReadAllText(_filePath);

            // JSON verisini tekrar List formatýna çevir
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        // Türkçe karakterleri Ýngilizceye çeviren metod (Dikkat-1 kuralý)
        public static string FixCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName)) return "";

            return cityName.Replace("ç", "c").Replace("Ç", "C")
                           .Replace("ð", "g").Replace("Ð", "G")
                           .Replace("ý", "i").Replace("Ý", "I")
                           .Replace("ö", "o").Replace("Ö", "O")
                           .Replace("þ", "s").Replace("Þ", "S")
                           .Replace("ü", "u").Replace("Ü", "U")
                           .ToUpper();
        }
    }
}