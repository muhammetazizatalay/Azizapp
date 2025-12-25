using GorselOdev.Models;
using GorselOdev.Services;

namespace GorselOdev;

public partial class KurlarPage : ContentPage
{
    public KurlarPage()
    {
        InitializeComponent();
        DovizleriYukle();
    }

    private async void DovizleriYukle()
    {
        // 1. Yükleme halkasýný baþlat 
        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        try
        {
            // 2. Servis metodunu çaðýrarak canlý verileri al
            var veriler = await NewsServices.GetExchangeRates();

            // 3. Gelen verileri XAML'deki listeye baðla
            lstKurlar.ItemsSource = veriler;
        }
        catch (Exception ex)
        {
            // Hata durumunda kullanýcýyý bilgilendir
            await DisplayAlert("Hata", "Veriler yüklenirken bir sorun oluþtu: " + ex.Message, "Tamam");
        }
        finally
        {
            // 4. Ýþlem bittiðinde halkayý durdur ve gizle
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    // Sað üstteki yenileme butonuna basýldýðýnda listeyi güncelleyen metod
    private void OnRefreshClicked(object sender, EventArgs e)
    {
        DovizleriYukle();
    }
}