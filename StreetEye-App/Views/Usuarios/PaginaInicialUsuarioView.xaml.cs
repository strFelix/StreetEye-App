using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App.Views.Usuarios;

public partial class PaginaInicialUsuarioView : ContentPage
{
    UsuarioViewModel viewModel;

    public PaginaInicialUsuarioView()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;
    }
}