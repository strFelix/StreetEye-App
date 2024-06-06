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

        lblCep.TextChanged += OnCepCompleted;

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

        if (newText.Length == 10)
        {
            // Tenta converter a entrada em um objeto DateTime
            if (DateTime.TryParseExact(e.NewTextValue, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                if (DateTime.ParseExact(e.NewTextValue, "dd/MM/yyyy", null) > DateTime.Now)
                {
                    Application.Current.MainPage.DisplayAlert("Informação:", "Data de nascimento inválida.", "OK");
                    ((Entry)sender).Text = "";
                    return;
                }   
                ((Entry)sender).Text = newText;
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Informação:", "Data de nascimento inválida.", "OK");
                ((Entry)sender).Text = "";
            }
        }

    }

    private void OnCepCompleted(object sender, EventArgs e)
    {
        if (lblCep.Text.Length == 8)
        {
            string cep = lblCep.Text;
            viewModel.GetEnderecoByCepAsync(cep);
        }
    }

    private async void OnButtonCadastrarPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnCadastrar.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnCadastrar.ScaleTo(1, 50, Easing.Linear);

    }

}