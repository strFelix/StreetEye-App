using StreetEye_App.Views.Responsaveis;
using StreetEye_App.Views.Semaforos;
using StreetEye_App.Views.Usuarios;

namespace StreetEye_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("cadResponsavelView", typeof(CadastroResponsavelView));
            Routing.RegisterRoute("exibirSemaforoView", typeof(SemaforoView));
            Routing.RegisterRoute("altDadosView", typeof(AlterarDadosUsuarioView));

            string login = Preferences.Get("UsuarioEmail", string.Empty);
            lblLogin.Text = $"login: {login}";
        }
    }
}
