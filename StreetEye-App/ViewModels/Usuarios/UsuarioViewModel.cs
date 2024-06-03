using StreetEye_App.Models;
using StreetEye_App.Services.Enderecos;
using StreetEye_App.Services.Usuarios;
using StreetEye_App.Views.Responsaveis;
using StreetEye_App.Views.Usuarios;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioService _usuarioService;
        private readonly EnderecoService _enderecoService;

        public UsuarioViewModel()
        {
            _usuarioService = new UsuarioService();
            _enderecoService = new EnderecoService();
            InicializarCommads();
        }

        public void InicializarCommads()
        {
            LoginCommand = new Command(async () => await AutenticarUsuarioAsync());
            RegistrarCommand = new Command(async () => await RegistrarUsuarioAsync());
            GetEnderecoByCepCommand = new Command(async () => await GetEnderecoByCepAsync());
            DirecionarLoginViewCommand = new Command(async () => await NavigateToLoginAsync());
            DirecionarCadastroViewCommand = new Command(async () => await NavigateToCadastroAsync());
            DirecionarAlterarDadosViewCommand = new Command(async () => await NavigateToAlterarDadosAsync());
            DirecionarCadastroResponsavelViewCommand = new Command(async () => await NavigateToCadastroResponsavelAsync());
        }

        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand GetEnderecoByCepCommand { get; set; }
        public ICommand DirecionarLoginViewCommand { get; set; }
        public ICommand DirecionarCadastroViewCommand { get; set; }
        public ICommand DirecionarCadastroResponsavelViewCommand { get; set; }
        public ICommand DirecionarAlterarDadosViewCommand { get; set; }

        #endregion

        #region Properties
        //usuario
        private string email;
        private string password;

        //utilizador
        private string nome = string.Empty;
        private String data;
        private string telefone = string.Empty;
        private string endereco = string.Empty;
        private string numeroEndereco = string.Empty;
        private string complemento = string.Empty;
        private string bairro = string.Empty;
        private string cidade = string.Empty;
        private string uf = string.Empty;
        private string cep = string.Empty;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
       
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

        #region Methods
        public async Task AutenticarUsuarioAsync()
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Email = Email;
                usuario.Password = Password;

                if(Email == null || Password == null) // Validação dos campos
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Email ou senha invalidos.", "Ok");
                    return;
                }

                Usuario usuarioAutenticado = await _usuarioService.PostAutenticarUsuarioAsync(usuario);

                if (!string.IsNullOrEmpty(usuarioAutenticado.Token))
                {

                    Preferences.Set("UsuarioId", usuarioAutenticado.Id);
                    Preferences.Set("UsuarioUsername", usuarioAutenticado.Utilizador.Nome);
                    Preferences.Set("UsuarioIdUtilizador", usuarioAutenticado.Utilizador.Id);
                    Preferences.Set("UsuarioEmail", usuarioAutenticado.Email);
                    Preferences.Set("UsuarioTelefone", usuarioAutenticado.Utilizador.Telefone);
                    Preferences.Set("UsuarioCEP", usuarioAutenticado.Utilizador.CEP);
                    Preferences.Set("UsuarioEndereco", usuarioAutenticado.Utilizador.Endereco);
                    Preferences.Set("UsuarioNumeroEndereco", usuarioAutenticado.Utilizador.NumeroEndereco);
                    Preferences.Set("UsuarioComplemento", usuarioAutenticado.Utilizador.Complemento);
                    Preferences.Set("UsuarioBairro", usuarioAutenticado.Utilizador.Bairro);
                    Preferences.Set("UsuarioCidade", usuarioAutenticado.Utilizador.Cidade);
                    Preferences.Set("UsuarioUf", usuarioAutenticado.Utilizador.UF);
                    Preferences.Set("UsuarioToken", usuarioAutenticado.Token);

                    Application.Current.MainPage = new AppShell();

                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Email ou senha invalidos.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }
        
        public async Task RegistrarUsuarioAsync()
        {
            try
            {

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Nome) || string.IsNullOrEmpty(Data) || string.IsNullOrEmpty(Telefone) || string.IsNullOrEmpty(Cep) || string.IsNullOrEmpty(Endereco) || string.IsNullOrEmpty(NumeroEndereco) || string.IsNullOrEmpty(Bairro) || string.IsNullOrEmpty(Cidade) || string.IsNullOrEmpty(Uf))
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Todos os campos são obrigatórios.", "Ok");
                    return;
                }

                Usuario usuario = new Usuario
                {
                    Email = Email,
                    Password = Password,
                    Utilizador = new Utilizador()
                    {
                        Nome = Nome,
                        DataNascimento = DateTime.Parse(Data),
                        Telefone = Telefone,
                        CEP = Cep,
                        Endereco = Endereco,
                        NumeroEndereco = NumeroEndereco,
                        Complemento = Complemento,
                        Bairro = Bairro,
                        Cidade = Cidade,
                        UF = Uf
                    }
                };

                // Validação dos campos
                if (Telefone.Length < 9 || !Email.Contains("@") || !Email.Contains(".com") || Password.Length < 8 || NumeroEndereco == null) 
                {
                    string campo = string.Empty;

                    if (Telefone.Length < 9)
                        campo = "Telefone inserido é invalido.";
                    else if (!Email.Contains("@") || !Email.Contains(".com"))
                        campo = "Email inserido é invalido.";
                    else if (Password.Length < 8)
                        campo = "Senha deve conter no minimo 8 caracteres.";
                    else if (NumeroEndereco == null)
                        campo = "Número do endereço é obrigatório.";

                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", campo, "Ok");
                    return;
                }   

                Usuario usuarioRegistrado = await _usuarioService.PostRegistrarUsuarioAsync(usuario);
                
                if(usuarioRegistrado != null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Usuário registrado com sucesso!", "Ok");

                    await NavigateToLoginAsync();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação:", "Erro ao registrar usuário.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação:", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }

        public async Task GetEnderecoByCepAsync()
        {
            try
            {
                if (Cep.Length < 8 && Cep != null)
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

        #region Navigation
        public async Task NavigateToLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginUsuarioView());
        }
        public async Task NavigateToCadastroAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CadastroUsuarioView());
        }
        public async Task NavigateToCadastroResponsavelAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CadastroResponsavelView());
        }
        public async Task NavigateToAlterarDadosAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AlterarDadosUsuarioView());
        }
        #endregion
    }
}
