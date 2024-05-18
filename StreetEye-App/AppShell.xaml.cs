using StreetEye_App.Views.Responsaveis;
using StreetEye_App.Views.Semaforos;

namespace StreetEye_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("cadResponsavelView", typeof(CadastroResponsavelView));
            Routing.RegisterRoute("exibirSemaforoView", typeof(SemaforoView));

            string login = Preferences.Get("UsuarioEmail", string.Empty);
            lblLogin.Text = $"login: {login}";
        }
    }
}
