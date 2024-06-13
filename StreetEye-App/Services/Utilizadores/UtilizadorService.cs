using StreetEye_App.Models;

namespace StreetEye_App.Services.Utilizadores
{
    public class UtilizadorService : Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Utilizadores";

        public UtilizadorService()
        {
            _request = new Request();
        }

        public async Task<int> PutUtilizadorAsync(Utilizador utilizador)
        {
            var result = await _request.PutAsync(apiUrlBase, utilizador, string.Empty);
            return result;
        }

    }
}
