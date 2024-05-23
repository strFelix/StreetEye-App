using StreetEye_App.Models;
using System.Text.Json;

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
        public event EventHandler<TrafficLightEvent> MessageReceived;

        public SseEventService()
        {
            _httpClient = new HttpClient();
        }

        public async Task StartListening(string url, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            var stream = await response?.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream);

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
    }
}
