using Firebase.Auth;

namespace GorselOdev;

public partial class LoginPage : ContentPage
{
    
    string webApiKey = "AIzaSyB8PRcup9QcOC_hcMgzt6d2WILZkmNMboc";
    bool isLoginState = true;

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));

        try
        {
            if (isLoginState)
            {
                // Oturum Açma
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
                await DisplayAlert("Baþarýlý", "Giriþ yapýldý!", "Tamam");
                App.Current.MainPage = new AppShell(); // Ana menüye yönlendir
            }
            else
            {
                // Kaydolma
                await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
                await DisplayAlert("Baþarýlý", "Hesap oluþturuldu! Giriþ yapabilirsiniz.", "Tamam");
                OnSwitchClicked(null, null); // Giriþ ekranýna geri dön
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Ýþlem baþarýsýz: " + ex.Message, "Tamam");
        }
    }

    private void OnSwitchClicked(object sender, EventArgs e)
    {
        isLoginState = !isLoginState;
        TitleLabel.Text = isLoginState ? "Oturum Aç" : "Kaydol";
        SubmitButton.Text = isLoginState ? "Oturum Aç" : "Kaydol";
        SwitchButton.Text = isLoginState ? "Hesabým Yok" : "Zaten bir hesabým var";
    }
}