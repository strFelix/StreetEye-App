using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;
using System.Windows.Input;
using StreetEye_App.Services.SseEvent;
using StreetEye_App.Models;
using System.Text.Json;

namespace StreetEye_App.ViewModels.Semaforos
{
    public partial class SseSemaforoViewModel : ObservableObject, IDisposable
    {
        private readonly SseEventService _sseEventService;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly string sseEndpoint = "http://localhost:3000/sse";

        [ObservableProperty]
        private string _trafficLightColor;

        [ObservableProperty]
        private int _trafficLightTimeLeft;

        public SseSemaforoViewModel()
        {
            _sseEventService = new SseEventService();
            _sseEventService.MessageReceived += OnMessageReceived;
            _ = StartListeningAsync();
        }

        #region Commands
        #endregion

        #region Methods
        public async Task StartListeningAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _sseEventService.StartListening(sseEndpoint, _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                      .DisplayAlert("Informação", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }

        private void OnMessageReceived(object sender, TrafficLightEvent e)
        {
            TrafficLightColor = e.color == "GREEN" ? "Aberto" : e.color == "RED" ? "Fechado" : "Indefinido";
            TrafficLightTimeLeft = e.timeLeft;
        }
        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _sseEventService.MessageReceived -= OnMessageReceived;
        }
        #endregion
    }
}
