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

        botaoLogin.Pressed += OnButtonLoginPressed;
        botaoCadastro.Pressed += OnButtonCadastroPressed;
    }

    private async void OnButtonLoginPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await botaoLogin.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await botaoLogin.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OnButtonCadastroPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await botaoCadastro.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await botaoCadastro.ScaleTo(1, 50, Easing.Linear);

    }
}