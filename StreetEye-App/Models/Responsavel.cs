using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Models
{
    public class Responsavel
    {
        public int IdUtilizador { get; set; }
        public int IdResponsavel { get; set; }

        [NotMapped]
        public Utilizador? Utilizador { get; set; }

        [NotMapped]
        public Utilizador? ResponsavelUtilizador { get; set; }
    }
}
