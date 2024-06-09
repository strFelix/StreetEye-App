using StreetEye_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Services.Utilizadores
{
    public class UtilizadorService : Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Utilizadores";

        public UtilizadorService(string token)
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
