using StreetEye_App.Views.Usuarios;

namespace StreetEye_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PaginaInicialUsuarioView());
        }
    }
}
