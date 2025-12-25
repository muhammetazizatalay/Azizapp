using Newtonsoft.Json;
using System.Xml;
using System.Text.Json; // JsonDocument için gerekli
using GorselOdev.Models;

namespace GorselOdev.Services
{
    public class NewsServices
    {
        // 1. Metod: Haberleri TRT'den çeker
        public static async Task<List<Item>> GetCategoryNews(NewsCategory category)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string xmlContent = await client.GetStringAsync(category.Url);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlContent);
                    string json = JsonConvert.SerializeXmlNode(doc);
                    var root = JsonConvert.DeserializeObject<Root>(json);
                    return root?.rss?.channel?.item ?? new List<Item>();
                }
            }
            catch { return new List<Item>(); }
        }

        // 2. Metod: Kurlarý Truncgil'den çeker (Senin çalýþan eski kodun)
        public static async Task<List<Kur>> GetExchangeRates()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://finans.truncgil.com/today.json";
                    string json = await client.GetStringAsync(url);
                    var list = new List<Kur>();

                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        foreach (JsonProperty element in doc.RootElement.EnumerateObject())
                        {
                            if (element.Name == "Update_Date") continue;

                            var val = element.Value;
                            if (val.ValueKind == JsonValueKind.Object)
                            {
                                list.Add(new Kur
                                {
                                    Code = element.Name,
                                    Title = val.TryGetProperty("Tür", out JsonElement t) ? t.ToString() : element.Name,
                                    Buying = val.TryGetProperty("Alýþ", out JsonElement a) ? a.ToString() : "-",
                                    Selling = val.TryGetProperty("Satýþ", out JsonElement s) ? s.ToString() : "-",
                                    Change = val.TryGetProperty("Deðiþim", out JsonElement d) ? d.ToString() : "-"
                                });
                            }
                        }
                    }
                    return list;
                }
            }
            catch { return new List<Kur>(); }
        }
    }
}