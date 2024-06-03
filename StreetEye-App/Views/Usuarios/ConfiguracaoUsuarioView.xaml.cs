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

    }


}