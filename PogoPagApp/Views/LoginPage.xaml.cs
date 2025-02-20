namespace PogoPagApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        string senha = txtSenha.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
            return;
        }

        // Simula��o de login bem-sucedido
        if (email == "admin@teste.com" && senha == "1234")
        {
            await DisplayAlert("Sucesso", "Login realizado!", "OK");

            // Navegar para a p�gina principal ap�s o login
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Erro", "E-mail ou senha inv�lidos!", "OK");
        }
    }

    private async void OnCadastroTapped(object sender, EventArgs e)
    {
        //await DisplayAlert("Cadastro", "Tela de cadastro ainda n�o implementada", "OK");
        await Navigation.PushAsync(new CadastroPage());
    }
}