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
        lblEndereco.Text = Preferences.Get("SemaforoEndereco", string.Empty);
        lblNumero.Text = Preferences.Get("SemaforoNumero", string.Empty);
        lblViaCruzamento.Text = Preferences.Get("SemaforoViaCruzamento", string.Empty);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.StartListeningAsync();
    }
}