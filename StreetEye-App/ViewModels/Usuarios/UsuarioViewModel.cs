using StreetEye_App.Models;
using StreetEye_App.Services.Usuarios;
using StreetEye_App.Views.Usuarios;
using System.Windows.Input;

namespace StreetEye_App.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioViewModel()
        {
            _usuarioService = new UsuarioService();
            InicializarCommads();
        }

        public void InicializarCommads()
        {
            LoginCommand = new Command(async () => await AutenticarUsuarioAsync());
            RegistrarCommand = new Command(async () => await RegistrarUsuarioAsync());
            DirecionarLoginViewCommand = new Command(async () => await NavigateToLoginAsync());
            DirecionarCadastroViewCommand = new Command(async () => await NavigateToCadastroAsync());
        }

        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarLoginViewCommand { get; set; }
        public ICommand DirecionarCadastroViewCommand { get; set; }
        #endregion

        #region Properties
        //usuario
        private string email;
        private string password;

        //utilizador
        private string nome = string.Empty;
        private DateTime data;
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
        public DateTime Data
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

                Usuario usuarioAutenticado = await _usuarioService.PostAutenticarUsuarioAsync(usuario);

                if (!string.IsNullOrEmpty(usuarioAutenticado.Token))
                {
                    string menssagem = $"Bem vindo(a) {usuarioAutenticado.Utilizador.Nome}!";

                    Preferences.Set("UsuarioId", usuarioAutenticado.Id);
                    Preferences.Set("UsuarioUsername", usuarioAutenticado.Utilizador.Nome);
                    Preferences.Set("UsuarioIdUtilizador", usuarioAutenticado.Utilizador.Id);
                    Preferences.Set("UsuarioToken", usuarioAutenticado.Token);

                    await Application.Current.MainPage
                        .DisplayAlert("Informações", menssagem, "Ok");

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
                Usuario usuario = new Usuario
                {
                    Email = Email,
                    Password = Password,
                    Utilizador = new Utilizador()
                    {
                        Nome = Nome,
                        DataNascimento = Data,
                        Telefone = Telefone,
                        Endereco = Endereco,
                        NumeroEndereco = NumeroEndereco,
                        Complemento = Complemento,
                        Bairro = Bairro,
                        Cidade = Cidade,
                        UF = Uf,
                        CEP = Cep
                    }
                };

                Usuario usuarioRegistrado = await _usuarioService.PostRegistrarUsuarioAsync(usuario);
                
                if(usuarioRegistrado != null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informações", "Usuário registrado com sucesso!", "Ok");
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informações", "Erro ao registrar usuário.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                        .DisplayAlert("Informação", ex.Message + "\n" + ex.InnerException, "Ok");
            }
        }
        #endregion

        #region Navigation
        public async Task NavigateToLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginView());
        }
        public async Task NavigateToCadastroAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CadastroView());
        }
        #endregion
    }
}
