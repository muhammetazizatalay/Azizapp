using GorselOdev.Models;
using GorselOdev.Services;
using System.Collections.ObjectModel;

namespace GorselOdev;

public partial class YapilacaklarPage : ContentPage
{
    DatabaseService _dbService = new DatabaseService();
    public ObservableCollection<TodoItem> Gorevler { get; set; } = new ObservableCollection<TodoItem>();

    public YapilacaklarPage()
    {
        InitializeComponent();
        lstTodo.ItemsSource = Gorevler;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VerileriYukle();
    }

    private async Task VerileriYukle()
    {
        var liste = await _dbService.GetItemsAsync();
        Gorevler.Clear();
        foreach (var item in liste) Gorevler.Add(item);
    }

    // HATA ÇÖZÜMÜ: Ekle butonuna basýldýðýnda detay sayfasýna gider
    private async void EkleClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GorevDetayPage());
    }

    // Düzenle butonuna basýldýðýnda mevcut veriyi göndererek detay sayfasýna gider
    private async void DuzenleClicked(object sender, EventArgs e)
    {
        var buton = sender as Button;
        var secilenGorev = buton.CommandParameter as TodoItem;
        await Navigation.PushAsync(new GorevDetayPage(secilenGorev));
    }

    // Silme onayý alarak silme iþlemi yapar
    private async void SilClicked(object sender, EventArgs e)
    {
        var buton = sender as Button;
        var silinecek = buton.CommandParameter as TodoItem;
        bool onay = await DisplayAlert("Silinsin mi?", "Silmeyi onayla", "OK", "CANCEL");
        if (onay)
        {
            await _dbService.DeleteItemAsync(silinecek.Id);
            await VerileriYukle();
        }
    }
}