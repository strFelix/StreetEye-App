using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App.Views.Usuarios;

public partial class ConfiguracaoUsuarioView : ContentPage
{
    UsuarioViewModel viewModel;
    public ConfiguracaoUsuarioView()
	{
		InitializeComponent();

        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;

        btnCadastrarResponsavel.Pressed += OnButtonCadastrarResponsavelPressed;
        btnAlterarDados.Pressed += OnButtonAlterarDadosPressed;
        openLinkButton.Clicked += OpenLinkButton_Clicked;
    }

    private async void OnButtonCadastrarResponsavelPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnCadastrarResponsavel.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnCadastrarResponsavel.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OnButtonAlterarDadosPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnAlterarDados.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnAlterarDados.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OpenLinkButton_Clicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://strfelix.github.io/Crossolutions-Website/"));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string login = Preferences.Get("UsuarioUsername", string.Empty);
        lblLogin.Text = $"{login}";

        string email = Preferences.Get("UsuarioEmail", string.Empty);
        lblEmail.Text = $"{email}";
    }
}


