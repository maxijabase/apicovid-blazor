using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class RespuestaIngresoDTO
    {
        public bool Exito { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
