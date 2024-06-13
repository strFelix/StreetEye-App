using StreetEye_App.Models;
using StreetEye_App.Services.Enderecos;
using StreetEye_App.Services.Utilizadores;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Utilizadores
{
    public class UtilizadoresViewModel : BaseViewModel
    {

        private readonly EnderecoService _enderecoService;
        private readonly UtilizadorService _utilizadorService;

        public UtilizadoresViewModel()
        {
            _enderecoService = new EnderecoService();
            _utilizadorService = new UtilizadorService();
            UpdateUtilizadorCommand = new Command(async () => await UpdateUtilizadorAsync());
        }

        #region Commands
        public ICommand UpdateUtilizadorCommand { get; set; }
        #endregion

        #region Properties
        //utilizador
        private string nome = string.Empty;
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

        #region Methods

        public async Task UpdateUtilizadorAsync()
        {
            try
            {
                Utilizador utilizador = new Utilizador
                {
                    Id = Preferences.Get("UsuarioIdUtilizador", 0),
                    Nome = Nome,
                    Telefone = Telefone,
                    Endereco = Endereco,
                    NumeroEndereco = NumeroEndereco,
                    Complemento = Complemento,
                    Bairro = Bairro,
                    Cidade = Cidade,
                    UF = Uf,
                    CEP = Cep
                };

                int result = await _utilizadorService.PutUtilizadorAsync(utilizador);
                if (result == 0)
                {
                    Preferences.Set("UsuarioUsername", utilizador.Nome);
                    Preferences.Set("UsuarioTelefone", utilizador.Telefone);
                    Preferences.Set("UsuarioCEP", utilizador.CEP);
                    Preferences.Set("UsuarioEndereco", utilizador.Endereco);
                    Preferences.Set("UsuarioNumeroEndereco", utilizador.NumeroEndereco);
                    Preferences.Set("UsuarioComplemento", utilizador.Complemento);
                    Preferences.Set("UsuarioBairro", utilizador.Bairro);
                    Preferences.Set("UsuarioCidade", utilizador.Cidade);
                    Preferences.Set("UsuarioUf", utilizador.UF);

                    await Application.Current.MainPage.Navigation.PopAsync();
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Usuário atualizado com sucesso.", "Ok");
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Erro ao atualizar usuário.", "Ok");
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
