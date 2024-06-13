using CommunityToolkit.Mvvm.ComponentModel;
using StreetEye_App.Services.SseEvent;

namespace StreetEye_App.ViewModels.Semaforos
{
    public partial class SseSemaforoViewModel : ObservableObject, IDisposable
    {
        private readonly SseEventService _sseEventService;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly string sseEndpoint = "https://streeteye-traffic-light.azurewebsites.net/sse";
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
                await _sseEventService.StartListening(sseEndpoint);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage
                      .DisplayAlert("Informação", "Cuidado ao atravessar. O semaforo no momento está inacessível!", "Ok");
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
