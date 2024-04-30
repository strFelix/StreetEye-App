using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App.Views.Usuarios;

public partial class LoginUsuarioView : ContentPage
{
    UsuarioViewModel viewModel;

    public LoginUsuarioView()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;
    }
}