using StreetEye_App.Models;

namespace StreetEye_App.Services.Usuarios
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        
        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Usuarios";

        public UsuarioService()
        {
            _request = new Request();
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
    }
}