using StreetEye_App.Services.Whatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Whatsapp
{
    public class WhatsappViewModel
    {
        private readonly WhatsappService _whatsappService;

        public ICommand SendHelpMessageCommand { get; }

        public WhatsappViewModel()
        {
            _whatsappService = new WhatsappService();
            SendHelpMessageCommand = new Command(async () => await SendHelpMessage());
        }

        private async Task SendHelpMessage()
        {
            int phoneNumber = 11; //configurar
            string message = "Preciso de ajuda!"; // configurar

            string success = await _whatsappService.SendMessage(phoneNumber, message);

            if (success != null)
            {
                await Application.Current.MainPage.DisplayAlert("Sucesso", "A mensagem de socorro foi enviada com sucesso!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao enviar a mensagem de socorro. Por favor, tente novamente.", "OK");
            }
        }
    }
}
