using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class ConsultaDTO
    {
        public string Edades { get; set; }
        public bool Masculino { get; set; }
        public bool Femenino { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public string Provincia { get; set; }

        public ConsultaDTO()
        {

        }
    }
}
