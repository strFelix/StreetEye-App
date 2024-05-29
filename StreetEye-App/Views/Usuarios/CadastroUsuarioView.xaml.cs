using StreetEye_App.ViewModels.Usuarios;

namespace StreetEye_App.Views.Usuarios;

public partial class CadastroUsuarioView : ContentPage
{
    UsuarioViewModel viewModel;

    public CadastroUsuarioView()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        BindingContext = viewModel;

        btnBuscarCep.Pressed += OnButtonBuscarCepPressed;
        btnCadastrar.Pressed += OnButtonCadastrarPressed;
    }
    private void OnDateEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string newText = e.NewTextValue;
        if (newText.Length > 2 && newText[2] != '/')
        {
            newText = newText.Insert(2, "/");
        }

        if (newText.Length > 5 && newText[5] != '/')
        {
            newText = newText.Insert(5, "/");
        }

        dateEntry.CursorPosition = newText.Length;
        ((Entry)sender).Text = newText;
    }

    private async void OnButtonBuscarCepPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnBuscarCep.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnBuscarCep.ScaleTo(1, 50, Easing.Linear);

    }

    private async void OnButtonCadastrarPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnCadastrar.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnCadastrar.ScaleTo(1, 50, Easing.Linear);

    }

}