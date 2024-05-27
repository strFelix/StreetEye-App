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

        string login = Preferences.Get("UsuarioUsername", string.Empty);
        lblLogin.Text = $"{login}";

        string email = Preferences.Get("UsuarioEmail", string.Empty);
        lblEmail.Text = $"{email}";

        string telefone = Preferences.Get("UsuarioTelefone", string.Empty);
        lblTelefone.Text = $"{telefone}";

        string cep = Preferences.Get("UsuarioCep", string.Empty);
        lblCep.Text = $"{cep}";

        string endereco = Preferences.Get("UsuarioEndereco", string.Empty);
        lblEndereco.Text = $"{endereco}";

        string numeroEndereco = Preferences.Get("UsuarioNumeroEndereco", string.Empty);
        lblNumeroEndereco.Text = $"{numeroEndereco}";

        string bairro = Preferences.Get("UsuarioBairro", string.Empty);
        lblBairro.Text = $"{bairro}";

        string cidade = Preferences.Get("UsuarioCidade", string.Empty);
        lblCidade.Text = $"{cidade}";

        string uf = Preferences.Get("UsuarioUf", string.Empty);
        lblUf.Text = $"{uf}";

    }
}