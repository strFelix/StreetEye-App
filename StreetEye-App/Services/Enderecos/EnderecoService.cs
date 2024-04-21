using StreetEye_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Services.Enderecos
{
    public class EnderecoService : Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "https://viacep.com.br/ws/";

        public EnderecoService()
        {
            _request = new Request();
        }

        public async Task<Endereco> GetEnderecoByCepAsync(string cep)
        {
            string uriComplementar = $"{cep}/json/";
            Endereco endereco = await _request.GetAsync<Endereco>(apiUrlBase + uriComplementar, string.Empty);
            return endereco;
        }   
    }
}
