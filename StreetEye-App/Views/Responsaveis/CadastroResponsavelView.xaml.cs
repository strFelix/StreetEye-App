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
}