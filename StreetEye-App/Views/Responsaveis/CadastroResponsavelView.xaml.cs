using StreetEye_App.ViewModels.Responsaveis;

namespace StreetEye_App.Views.Responsaveis;

public partial class CadastroResponsavelView : ContentPage
{
    ResponsavelViewModel viewModel;

    public CadastroResponsavelView()
    {
        InitializeComponent();
        viewModel = new ResponsavelViewModel();
        BindingContext = viewModel;
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
}