namespace GorselOdev;

public partial class AyarlarPage : ContentPage
{
    public AyarlarPage()
    {
        InitializeComponent();

        // Baþlangýç kontrolü
        bool isDark = Application.Current.UserAppTheme == AppTheme.Dark;
        ThemeSwitch.IsToggled = isDark;
        lblDurum.Text = isDark ? "AÇIK" : "KAPALI";
        lblDurum.TextColor = isDark ? Colors.White : Color.FromArgb("#2196F3");
    }

    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value) // Koyu Tema Aktif
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            lblDurum.Text = "AÇIK";
            lblDurum.TextColor = Colors.White; // Koyu modda kesin beyaz
        }
        else // Açýk Tema Aktif
        {
            Application.Current.UserAppTheme = AppTheme.Light;
            lblDurum.Text = "KAPALI";
            lblDurum.TextColor = Color.FromArgb("#2196F3"); // Açýk modda mavi
        }
    }
}