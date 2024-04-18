using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App;

public partial class MainPage : ContentPage
{
    UsuarioViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;
    }
}