namespace StreetEye_App.Models
{
    public class Semaforo
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IntervaloAberto { get; set; }
        public int IntervaloFechado { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string ViaCruzamento { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
