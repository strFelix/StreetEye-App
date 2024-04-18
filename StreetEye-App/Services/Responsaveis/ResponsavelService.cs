using StreetEye_App.Models;

namespace StreetEye_App.Services.Responsaveis
{
    class ResponsavelService : Request
    {

        private readonly Request _request;
        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Responsaveis";
        private string _token;


        public ResponsavelService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Utilizador> PostRegistrarResponsavelAsync(Utilizador utilizadorResponsavel)
        {
            utilizadorResponsavel = await _request.PostAsync(apiUrlBase, utilizadorResponsavel, _token);
            return utilizadorResponsavel;
        }
    }
}
