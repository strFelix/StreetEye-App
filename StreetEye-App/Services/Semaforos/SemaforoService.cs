using StreetEye_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Services.Semaforos
{
    public class SemaforoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://myprojects.somee.com/StreetEye/Semaforos";

        public SemaforoService()
        {
            _request = new Request(); ;
        }

        public async Task<ObservableCollection<Semaforo>> GetSemaforosAsync()
        {
            string urlComplementar = "/";
            ObservableCollection<Models.Semaforo> listaSemaforos = await
                _request.GetAsync<ObservableCollection<Models.Semaforo>>(apiUrlBase + urlComplementar, string.Empty);
            return listaSemaforos;
        }

        public async Task PostStatusSemaforoAsync(StatusSemaforo statusSemaforo)
        {
            string urlComplementar = "/historico";
            await _request.PostHistoricoAsync(apiUrlBase + urlComplementar, statusSemaforo, string.Empty);
        }

    }
}
