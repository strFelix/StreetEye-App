using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Services.Whatsapp
{
    public class Whatsapp
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "YOUR_GUPSHUP_API_KEY"; //configurar
        private const string ApiUrl = "https://api.gupshup.io/sm/api/v1/msg"; //configurar

        public Whatsapp()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);
        }

        public async Task<bool> SendMessage(string phoneNumber, string message)
        {
            var payload = new
            {
                channel = "whatsapp", //configurar
                source = "YOUR_WHATSAPP_NUMBER", //configurar
                destination = phoneNumber,
                message = new
                {
                    type = "text",
                    text = message
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);

            return response.IsSuccessStatusCode;
        }
    }
}
