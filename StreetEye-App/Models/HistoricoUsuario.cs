namespace StreetEye_App.Models
{
    public class HistoricoUsuario
    {
        public int IdUsuario { get; set; }
        public int IdSemaforo { get; set; }
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
