using StreetEye_App.Models;
using StreetEye_App.Services.Enderecos;
using StreetEye_App.Services.Responsaveis;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Responsaveis
{
    class ResponsavelViewModel : BaseViewModel
    {
        private readonly ResponsavelService _responsavelService;
        private readonly EnderecoService _enderecoService;

        public ResponsavelViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            _responsavelService = new ResponsavelService(token);
            _enderecoService = new EnderecoService();
            InicializarCommads();
        }

        public void InicializarCommads()
        {
            RegistrarResponsavelCommand = new Command(async () => await RegistrarResponsavelAsync());
        }

        #region Properties
        private string nome = string.Empty;
        private string data = string.Empty;
        private string telefone = string.Empty;
        private string endereco = string.Empty;
        private string numeroEndereco = string.Empty;
        private string complemento = string.Empty;
        private string bairro = string.Empty;
        private string cidade = string.Empty;
        private string uf = string.Empty;
        private string cep = string.Empty;

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }
        public string Data
        {
            get => data;
            set
            {
                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public string Telefone
        {
            get => telefone;
            set
            {
                telefone = value;
                OnPropertyChanged(nameof(Telefone));
            }
        }

        public string Endereco
        {
            get => endereco;
            set
            {
                endereco = value;
                OnPropertyChanged(nameof(Endereco));
            }
        }
        public string NumeroEndereco
        {
            get => numeroEndereco;
            set
            {
                numeroEndereco = value;
                OnPropertyChanged(nameof(NumeroEndereco));
            }
        }
        public string Complemento
        {
            get => complemento;
            set
            {
                complemento = value;
                OnPropertyChanged(nameof(Complemento));
            }
        }
        public string Bairro
        {
            get => bairro;
            set
            {
                bairro = value;
                OnPropertyChanged(nameof(Bairro));
            }
        }
        public string Cidade
        {
            get => cidade;
            set
            {
                cidade = value;
                OnPropertyChanged(nameof(Cidade));
            }
        }
        public string Uf
        {
            get => uf;
            set
            {
                uf = value;
                OnPropertyChanged(nameof(Uf));
            }
        }

        public string Cep
        {
            get => cep;
            set
            {
                cep = value;
                OnPropertyChanged(nameof(Cep));
            }
        }
        #endregion

        #region Commands
        public ICommand RegistrarResponsavelCommand { get; set; }
        #endregion

        #region Methods
        private async Task RegistrarResponsavelAsync()
        {
            try
            {
                Utilizador utilizadorResponsavel = new Utilizador
                {
                    Nome = Nome,
                    //DataNascimento = Data,
                    Telefone = Telefone,
                    Endereco = Endereco,
                    NumeroEndereco = NumeroEndereco,
                    Complemento = Complemento,
                    Bairro = Bairro,
                    Cidade = Cidade,
                    UF = Uf,
                    CEP = Cep
                };

                // verificar se o responsável já foi cadastrado
                int id = Preferences.Get("UsuarioIdUtilizador", 0);
                Utilizador ur = await _responsavelService.GetResponsavelByIdUtilizadorAsync(id);
                if (ur.Nome != string.Empty)
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                    await Application.Current.MainPage.DisplayAlert("Informação:", "Usuário já possui um responsável cadastrado.", "Ok");
                }
                else
                {
                    // cadastrar responsável
                    await _responsavelService.PostRegistrarResponsavelAsync(utilizadorResponsavel);

                    // alerta de sucesso ou erro
                    if (utilizadorResponsavel != null)
                    {
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Application.Current.MainPage.DisplayAlert("Informação:", "Responsável cadastrado com sucesso.", "Ok");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Erro", "Erro ao cadastrar responsável", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Erro", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }

        public async Task GetResponsavelByUtilizadorIdAsync(int id)
        {
            try 
            {
                Utilizador utilizador = await _responsavelService.GetResponsavelByIdUtilizadorAsync(id);
                if (utilizador != null) 
                {
                    Preferences.Set("ResponsavelNome", utilizador.Nome);
                    Preferences.Set("ResponsavelTelefone", utilizador.Telefone);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação:", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }

        public async Task GetEnderecoByCepAsync(string cep)
        {
            try
            {
                if (cep.Length < 8 && Cep != null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "CEP informado é inválido.", "Ok");
                    return;
                }

                Endereco endereco = await _enderecoService.GetEnderecoByCepAsync(Cep);

                if (endereco.Logradouro != null)
                {
                    Endereco = endereco.Logradouro;
                    Bairro = endereco.Bairro;
                    Cidade = endereco.Localidade;
                    Uf = endereco.Uf;
                }
                else
                {
                    Endereco = string.Empty;
                    Bairro = string.Empty;
                    Cidade = string.Empty;
                    Uf = string.Empty;
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "CEP não encontrado.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação:", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
