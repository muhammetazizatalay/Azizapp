using GorselOdev.Models;
using GorselOdev.Services;

namespace GorselOdev;

public partial class GorevDetayPage : ContentPage
{
    DatabaseService _dbService = new DatabaseService();
    TodoItem _mevcutGorev;

    public GorevDetayPage(TodoItem gorev = null)
    {
        InitializeComponent();
        _mevcutGorev = gorev;

        if (_mevcutGorev != null)
        {
            entBaslik.Text = _mevcutGorev.Baslik;
            entDetay.Text = _mevcutGorev.Detay;
            dtTarih.Date = DateTime.Parse(_mevcutGorev.Tarih);
            tmSaat.Time = TimeSpan.Parse(_mevcutGorev.Saat);
        }
    }

    // BU METOT TAMAM BUTONUNA BAÐLI
    private async void KaydetClicked(object sender, EventArgs e)
    {
      

        try
        {
            var gorev = _mevcutGorev ?? new TodoItem();
            gorev.Baslik = entBaslik.Text;
            gorev.Detay = entDetay.Text;
            gorev.Tarih = dtTarih.Date.ToShortDateString();
            gorev.Saat = tmSaat.Time.ToString(@"hh\:mm");
            gorev.Tamamlandi = 0;

            await _dbService.SaveItemAsync(gorev);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Hata gözükecek
            await DisplayAlert("Kayýt Hatasý", "Hata þu: " + ex.Message, "Anladým");
        }
    }

    private async void IptalClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}