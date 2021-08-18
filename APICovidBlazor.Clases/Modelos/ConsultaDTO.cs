using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public ConsultaDTO(NameValueCollection args)
        {
            Edades = args["edades"] ?? string.Empty;
            Masculino = bool.Parse(args["masculino"] ?? "false");
            Femenino = bool.Parse(args["femenino"] ?? "false");
            Desde = DateTime.Parse(args["desde"] ?? "0001-01-01");
            Hasta = DateTime.Parse(args["hasta"] ?? "0001-01-01");
            Provincia = args["provincia"] ?? string.Empty;
        }
    }
}
