using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetEye_App.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdUtilizador { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [NotMapped]
        public string Token { get; set; } = string.Empty;

        [NotMapped]
        public Utilizador? Utilizador { get; set; }
    }
}
