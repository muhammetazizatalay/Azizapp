using GorselOdev.Models; // Model dosyamýza eriþmek için

namespace GorselOdev;

public partial class HaberDetayPage : ContentPage
{
    // Haberi paylaþýrken kullanmak için global bir deðiþken tanýmlýyoruz
    Item _secilenHaber;

    public HaberDetayPage(Item haber)
    {
        InitializeComponent();

        _secilenHaber = haber;

        // WebView içine haberin linkini yüklüyoruz
        HaberWebView.Source = haber.link;
    }

  // haberleri paylaþma kýsmý
    private async void ShareClicked(object sender, EventArgs e)
    {
        if (_secilenHaber == null) return;

        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Uri = _secilenHaber.link,
            Title = _secilenHaber.title
        });
    }
}