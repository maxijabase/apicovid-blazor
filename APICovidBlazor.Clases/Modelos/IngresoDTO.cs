using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class IngresoDTO
    {
        public int Edad {  get; set; }
        public string Sexo {  get; set; }
        public DateTime Fecha {  get; set; }
        public string Provincia { get; set; }
        public bool Fallecido {  get; set; }
    }
}
