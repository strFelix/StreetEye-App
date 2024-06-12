using StreetEye_App.Models;

namespace StreetEye_App.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private string _token;

        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Usuarios";

        public UsuarioService()
        {
            _request = new Request();
        }

        public UsuarioService(string token)
        {
            _request = new Request();
            _token = token;
        }


        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario usuario)
        {
            string uriComplementar = "/Registrar";
            usuario = await _request.PostAsync(apiUrlBase + uriComplementar, usuario, string.Empty);
            return usuario;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario usuario)
        {
            string uriComplementar = "/Autenticar";
            usuario = await _request.PostAsync(apiUrlBase + uriComplementar, usuario, string.Empty);
            return usuario;
        }

        public async Task PostUsuarioHistoricoAsync()
        {
            string uriComplementar = "/Historico";
            HistoricoUsuario historicoUsuario = new HistoricoUsuario
            {
                IdUsuario = Preferences.Get("UsuarioId", 0),
                IdSemaforo = Preferences.Get("SemaforoId", 0),
                Latitude = Preferences.Get("SemaforoLatitude", string.Empty),
                Longitude = Preferences.Get("SemaforoLongitude", string.Empty)

            };
            await _request.PostHistoricoAsync(apiUrlBase + uriComplementar, historicoUsuario, _token);
        }
    }
}