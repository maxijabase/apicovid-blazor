using System;
using System.Collections.Generic;

#nullable disable

namespace APICovidBlazor.Clases.Entidades
{
    public partial class Casoscovid
    {
        public string Sexo { get; set; }
        public long? Edad { get; set; }
        public string ResidenciaProvinciaNombre { get; set; }
        public string FechaApertura { get; set; }
        public string Fallecido { get; set; }
    }
}
