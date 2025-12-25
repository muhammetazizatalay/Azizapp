using System.Collections.ObjectModel;
using GorselOdev.Models;
using GorselOdev.Services;

namespace GorselOdev;

public partial class HaberlerPage : ContentPage
{
    // Hocanýn kullandýðý kategori listesi yapýsý
    public ObservableCollection<NewsCategory> NewsCategories => new()
    {
        new NewsCategory("Manþet", "https://www.trthaber.com/manset_articles.rss"),
        new NewsCategory("Gündem", "https://www.trthaber.com/gundem_articles.rss"),
        new NewsCategory("Ekonomi", "https://www.trthaber.com/ekonomi_articles.rss"),
        new NewsCategory("Spor", "https://www.trthaber.com/spor_articles.rss"),
        new NewsCategory("Teknoloji", "https://www.trthaber.com/bilim_teknoloji_articles.rss")
    };

    public HaberlerPage()
    {
        InitializeComponent();
        // Kategorileri ekrandaki listeye baðlýyoruz
        lstCategory.ItemsSource = NewsCategories;
    }

    // Hocanýn LoadRSSNews metodu
    private async void LoadRSSNews(object sender, EventArgs e)
    {
        var category = (sender as Button).CommandParameter as NewsCategory;

        // Servis üzerinden veriyi çekiyoruz
        var news = await NewsServices.GetCategoryNews(category);

        // Haberleri ekrandaki listeye baðlýyoruz
        HaberListesi.ItemsSource = news;
    }

    // Habere týklandýðýnda detay sayfasýný açma
    private async void OpenNewsDetail(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Item news)
        {
            await Navigation.PushAsync(new HaberDetayPage(news));
        }
        ((CollectionView)sender).SelectedItem = null;
    }
}