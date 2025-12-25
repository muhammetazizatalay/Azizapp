using Newtonsoft.Json;
using System.Collections.ObjectModel;
using GorselOdev.Models;

namespace GorselOdev;

public partial class HavaDurumuPage : ContentPage
{
    // Þehirleri tutan liste
    ObservableCollection<SehirHavaDurumu> sehirler = new ObservableCollection<SehirHavaDurumu>();

    // JSON dosyasýnýn telefon hafýzasýndaki yolu
    string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");

    public HavaDurumuPage()
    {
        InitializeComponent();
        VerileriYukle(); // Uygulama açýlýnca eski þehirleri getir
        SehirlerListesi.ItemsSource = sehirler;
    }

    private void SehirEkleClicked(object sender, EventArgs e)
    {
        string sehir = SehirEntry.Text?.Trim().ToUpper();

        if (!string.IsNullOrEmpty(sehir))
        {
            // MGM Linki için Türkçe karakterleri temizleme (Örn: ÝSTANBUL -> ISTANBUL)
            string temizSehir = KarakterTemizle(sehir);

            // Görsel 8'deki MGM link yapýsý
            string resimUrl = $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={temizSehir}&baslik=G";

            sehirler.Add(new SehirHavaDurumu { SehirAdi = sehir, DurumResmi = resimUrl });

            VerileriKaydet(); // JSON olarak kaydet
            SehirEntry.Text = ""; // Giriþ alanýný temizle
        }
    }

    private async void SehirSilClicked(object sender, EventArgs e)
    {
        // Týklanan butonu ve içindeki þehir bilgisini alýyoruz
        var buton = sender as Button;
        var silinecekSehir = buton.CommandParameter as SehirHavaDurumu;

        if (silinecekSehir != null)
        {
            // Kullanýcýya emin olup olmadýðýný soralým (Görsel 2'deki gibi etkileþimli olsun)
            bool cevap = await DisplayAlert("Silme Onayý", $"{silinecekSehir.SehirAdi} listenden silinecek. Emin misin?", "Evet", "Hayýr");

            if (cevap)
            {
                // Listeden kaldýr
                sehirler.Remove(silinecekSehir);

                // Güncel listeyi telefona tekrar kaydet (Böylece uygulama açýlýnca geri gelmez)
                VerileriKaydet();
            }
        }
    }
    // Telefona JSON Kayýt Ýþlemi 
    private void VerileriKaydet()
    {
        string json = JsonConvert.SerializeObject(sehirler);
        File.WriteAllText(dosyaYolu, json);
    }

    private void VerileriYukle()
    {
        if (File.Exists(dosyaYolu))
        {
            string json = File.ReadAllText(dosyaYolu);
            var yuklenenSehirler = JsonConvert.DeserializeObject<ObservableCollection<SehirHavaDurumu>>(json);
            if (yuklenenSehirler != null)
            {
                foreach (var s in yuklenenSehirler) sehirler.Add(s);
            }
        }
    }

    private string KarakterTemizle(string text)
    {
        return text.Replace("Ý", "I").Replace("Ð", "G").Replace("Ü", "U")
                   .Replace("Þ", "S").Replace("Ö", "O").Replace("Ç", "C");
    }
}