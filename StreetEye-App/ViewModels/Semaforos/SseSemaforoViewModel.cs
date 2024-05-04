using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;
using System.Windows.Input;
using StreetEye_App.Services.SseEvent;

namespace StreetEye_App.ViewModels.Semaforos
{
    public partial class SseSemaforoViewModel : ObservableObject, IDisposable
    {
        private readonly SseEventService _sseEventService;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly string sseEndpoint = "http://localhost:3000/sse";

        [ObservableProperty]
        private string _trafficLightColor;

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

        private void OnMessageReceived(object sender, string message)
        {
            TrafficLightColor = message;
            // Parse the received message and update the TrafficLightColor property
            //if (message.StartsWith("data:\""))
            //{
            //    var color = message.Substring(6, message.Length - 7); // Extract color from message
            //    TrafficLightColor = color;
            //}
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
