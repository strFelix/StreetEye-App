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

        botaoLogin.Pressed += OnButtonPressed;
    }

    private async void OnButtonPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await botaoLogin.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await botaoLogin.ScaleTo(1, 50, Easing.Linear);
    }
}