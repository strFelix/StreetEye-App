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
    }
}