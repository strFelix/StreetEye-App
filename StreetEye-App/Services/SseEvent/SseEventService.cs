using StreetEye_App.Models;
using StreetEye_App.Services.Semaforos;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StreetEye_App.Services.SseEvent
{
    public class TrafficLightEvent
    {
        public string color { get; set; }
        public int timeLeft { get; set; }
    }

    public class SseEventService
    {
        private readonly HttpClient _httpClient;
        private readonly SemaforoService _semaforoService;
        private CancellationTokenSource _cancellationTokenSource;

        private string _lastTrafficLightColor;


        public event EventHandler<TrafficLightEvent> MessageReceived;

        public SseEventService()
        {
            _httpClient = new HttpClient();
            _semaforoService = new SemaforoService();
        }

        public async Task StartListening(string url)
        {
            // Crie um novo CancellationTokenSource para cada chamada
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

            try
            {
                using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);

                string eventName = null;
                string eventData = null;

                while (!cancellationToken.IsCancellationRequested)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    if (line.StartsWith("retry:"))
                    {
                        // Ignore retry line
                        continue;
                    }
                    else if (line.StartsWith("event:"))
                    {
                        eventName = line.Substring(6).Trim();
                    }
                    else if (line.StartsWith("data:"))
                    {
                        eventData = line.Substring(5).Trim();
                    }
                    else if (line != ":") // Empty line signifies the end of an event
                    {
                        if (eventName == "trafficLightUpdate" && !string.IsNullOrEmpty(eventData))
                        {
                            try
                            {
                                //debug line
                                Console.WriteLine($"Received event data: {eventData}");  // Log the raw JSON string
                                var trafficLightEvent = JsonSerializer.Deserialize<TrafficLightEvent>(eventData);
                                if (trafficLightEvent != null)
                                {
                                    //debug line
                                    Console.WriteLine($"Deserialized event: Color={trafficLightEvent.color}, TimeLeft={trafficLightEvent.timeLeft}");
                                    OnMessageReceived(this, trafficLightEvent);
                                    MessageReceived?.Invoke(this, trafficLightEvent);
                                }
                            }
                            catch (JsonException ex)
                            {
                                //debug line
                                // Handle JSON parsing error
                                Console.WriteLine($"Failed to parse event data: {ex.Message}");
                            }
                        }

                        // Reset event name and data for the next event
                        eventName = null;
                        eventData = null;
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // Handle the task cancellation if needed
                Console.WriteLine("Listening task was canceled.");
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void StopListening()
        {
            // Cancele o token para sinalizar a tarefa de escuta para parar
            _cancellationTokenSource?.Cancel();
        }

        private async void OnMessageReceived(object sender, TrafficLightEvent e)
        {
            if (e.color == _lastTrafficLightColor)
            {
                return; // Se não houve mudança de cor, não faz nada
            }

            StatusSemaforo statusSemaforo = new StatusSemaforo
            {
                IdSemaforo = Preferences.Get("SemaforoId", 0),
                StatusVisor = true,
                StatusAudio = true
            };

            _lastTrafficLightColor = e.color;

            if (e.color == "GREEN")
            {
                statusSemaforo.Estado = true;
                Console.WriteLine($"Posting status: Color=GREEN, TimeLeft={e.timeLeft}");
                await _semaforoService.PostStatusSemaforoAsync(statusSemaforo);
            }
            else if (e.color == "RED")
            {
                statusSemaforo.Estado = false;
                Console.WriteLine($"Posting status: Color=RED, TimeLeft={e.timeLeft}");
                await _semaforoService.PostStatusSemaforoAsync(statusSemaforo);
            }
            else
            {
                statusSemaforo.Estado = false;
                statusSemaforo.StatusVisor = false;
                statusSemaforo.StatusAudio = false;
                Console.WriteLine($"Posting status: Color={e.color}, TimeLeft={e.timeLeft}");
                await _semaforoService.PostStatusSemaforoAsync(statusSemaforo);
            }
        }
    }
}
