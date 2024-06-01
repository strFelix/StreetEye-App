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

        btnBuscarCep.Pressed += OnButtonBuscarCepPressed;
        btnAlterar.Pressed += OnButtonAlterarPressed;

        string login = Preferences.Get("UsuarioUsername", string.Empty);
        lblLogin.Text = $"{login}";

        string email = Preferences.Get("UsuarioEmail", string.Empty);
        lblEmail.Text = $"Email: {email}";

        string telefone = Preferences.Get("UsuarioTelefone", string.Empty);
        lblTelefone.Text = $"Telefone: {telefone}";

        string cep = Preferences.Get("UsuarioCep", string.Empty);
        lblCep.Text = $"Cep: {cep}";

        string endereco = Preferences.Get("UsuarioEndereco", string.Empty);
        lblEndereco.Text = $"Endereço: {endereco}";

        string numeroEndereco = Preferences.Get("UsuarioNumeroEndereco", string.Empty);
        lblNumeroEndereco.Text = $"{numeroEndereco}";

        string bairro = Preferences.Get("UsuarioBairro", string.Empty);
        lblBairro.Text = $"Bairro: {bairro}";

        string cidade = Preferences.Get("UsuarioCidade", string.Empty);
        lblCidade.Text = $"{cidade}";

    }

    private async void OnButtonBuscarCepPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnBuscarCep.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnBuscarCep.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OnButtonAlterarPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnAlterar.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnAlterar.ScaleTo(1, 50, Easing.Linear);

    }
}