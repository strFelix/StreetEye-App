using StreetEye_App.Views.Responsaveis;
using StreetEye_App.Views.Semaforos;

namespace StreetEye_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("cadResponsavelView", typeof(CadastroResponsavelView));
            Routing.RegisterRoute("exibirSemaforoView", typeof(SemaforoView));
            InitializeComponent();
        }
    }
}
