using StreetEye_App.Models;
using StreetEye_App.Services.Semaforos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Semaforos
{
    public class SemaforoViewModel : BaseViewModel
    {
        private readonly SemaforoService _semaforoService;
        public ObservableCollection<Semaforo> Semaforos { get; set; }
        public SemaforoViewModel( )
        {
            _semaforoService = new SemaforoService();
            Semaforos = new ObservableCollection<Semaforo>();
            _ = ObterSemaforos();
        }

        #region Properties
        private int id;
        private string descricao = string.Empty;
        private int intervaloAberto;
        private int intervaloFechado;
        private string endereco = string.Empty;
        private string numero = string.Empty;
        private string viaCruzamento= string.Empty;
        private string latitude = string.Empty;
        private string longitude = string.Empty;

        private Semaforo semaforoSelecionado;
        public Semaforo SemaforoSelecionado
        {
            get { return semaforoSelecionado; }
            set
            {
                if (value != null)
                {
                    semaforoSelecionado = value;
                    if(value.Descricao.ToString() == "Online")
                    {
                        Shell.Current.GoToAsync($"exibirSemaforoView?pId={semaforoSelecionado.Id}");
                        Preferences.Set("SemaforoEndereco", semaforoSelecionado.Endereco);
                        Preferences.Set("SemaforoNumero", semaforoSelecionado.Numero);
                        Preferences.Set("SemaforoViaCruzamento", semaforoSelecionado.ViaCruzamento);
                    } 
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Aviso", "Semaforo selecionado está offline.", "Ok");
                    }
                }
            }
        }

        public int Id { get => id; set => id = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public int IntervaloAberto { get => intervaloAberto; set => intervaloAberto = value; }
        public int IntervaloFechado { get => intervaloFechado; set => intervaloFechado = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Numero { get => numero; set => numero = value; }
        public string ViaCruzamento { get => viaCruzamento; set => viaCruzamento = value; }
        public string Latitude { get => latitude; set => latitude = value; }
        public string Longitude { get => longitude; set => longitude = value; }
        #endregion

        #region Methods
        public async Task ObterSemaforos()
        {
            try
            {
                Semaforos = await _semaforoService.GetSemaforosAsync();
                OnPropertyChanged(nameof(Semaforos));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}