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

        botaoLogin.Pressed += OnButtonLoginPressed;
        botaoSenha.Pressed += OnButtonSenhaPressed;
    }

    private async void OnButtonLoginPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await botaoLogin.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await botaoLogin.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OnButtonSenhaPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await botaoSenha.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await botaoSenha.ScaleTo(1, 50, Easing.Linear);
    }
}