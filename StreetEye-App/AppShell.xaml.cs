using StreetEye_App.Views.Responsaveis;

namespace StreetEye_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("cadResponsavelView", typeof(CadastroResponsavelView));
            InitializeComponent();
        }
    }
}
