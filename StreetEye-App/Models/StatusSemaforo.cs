using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetEye_App.Models
{
    public class StatusSemaforo
    {
        public int IdSemaforo { get; set; }
        public bool StatusVisor { get; set; }
        public bool StatusAudio { get; set; }
        public bool Estado { get; set; }
    }
}
