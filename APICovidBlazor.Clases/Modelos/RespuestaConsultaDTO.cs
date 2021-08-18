using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class RespuestaConsultaDTO
    {
        public int Casos { get; set; }

        public RespuestaConsultaDTO()
        {
            Casos = 0;
        }
    }
}
