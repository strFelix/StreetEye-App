using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App.Views.Usuarios;

public partial class AlterarDadosUsuarioView : ContentPage
{
    UsuarioViewModel viewModel;
    public AlterarDadosUsuarioView()
	{
		InitializeComponent();

		viewModel = new UsuarioViewModel();
		BindingContext = viewModel;

	}
}