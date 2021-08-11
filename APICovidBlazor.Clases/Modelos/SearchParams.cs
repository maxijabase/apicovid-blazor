using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    public class SearchParams
    {
        [FromQuery(Name = "desde")]
        public DateTime? Desde { get; set; }
        [FromQuery(Name = "hasta")]
        public DateTime? Hasta { get; set; }
        [FromQuery(Name = "edades")]
        public string? Edades { get; set; }
        [FromQuery(Name = "genero")]
        public string? Genero { get; set; }
        [FromQuery(Name = "provincia")]
        public string? Provincia { get; set; }

        public SearchParams()
        {

        }
    }
}
