using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class RespuestaConsultaDTO
    {
        public int Contagios {  get; set; }

        public RespuestaConsultaDTO()
        {
            Contagios = 0;
        }
    }
}
