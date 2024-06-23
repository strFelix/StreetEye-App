using StreetEye_App.ViewModels.Semaforos;

namespace StreetEye_App.Views.Semaforos;

public partial class SemaforoView : ContentPage
{
    private readonly SseSemaforoViewModel viewModel;
    public SemaforoView()
    {
        InitializeComponent();
        viewModel = new SseSemaforoViewModel();
        BindingContext = viewModel;
    }
}