using StreetEye_App.ViewModels.Utilizadores;

namespace StreetEye_App.Views.Usuarios;

public partial class AlterarDadosUsuarioView : ContentPage
{
    UtilizadoresViewModel viewModel;
    public AlterarDadosUsuarioView()
    {
        InitializeComponent();

        viewModel = new UtilizadoresViewModel();
        BindingContext = viewModel;
        lblCep.TextChanged += OnCepCompleted;
        btnSalvar.Pressed += OnButtonSalvarPressed;

        //completando as labels com as info do usuario autenticado
        lblNome.Text = Preferences.Get("UsuarioUsername", "");
        lblTelefone.Text = Preferences.Get("UsuarioTelefone", "");
        lblCep.Text = Preferences.Get("UsuarioCEP", "").Trim(); ;
        lblEndereco.Text = Preferences.Get("UsuarioEndereco", "");
        lblNumeroEndereco.Text = Preferences.Get("UsuarioNumeroEndereco", "");
        lblComplemento.Text = Preferences.Get("UsuarioComplemento", "");
        lblBairro.Text = Preferences.Get("UsuarioBairro", "");
        lblCidade.Text = Preferences.Get("UsuarioCidade", "");
        lblUF.Text = Preferences.Get("UsuarioUf", "");


    }

    private void OnCepCompleted(object sender, EventArgs e)
    {
        if (lblCep.Text.Length == 8)
        {
            string cep = lblCep.Text;
            viewModel.GetEnderecoByCepAsync(cep);
        }
    }

    private async void OnButtonSalvarPressed(object sender, EventArgs e)
    {
        // Animação de escala
        await btnSalvar.ScaleTo(0.9, 50, Easing.Linear);
        await Task.Delay(50);
        await btnSalvar.ScaleTo(1, 50, Easing.Linear);

    }
}