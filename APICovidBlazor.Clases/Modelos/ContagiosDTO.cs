using System;

namespace APICovidBlazor.Clases.Modelos
{
    public class ContagiosDTO
    {
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public string? Edades { get; set; }
        public string? Genero { get; set; }
        public string? Provincia { get; set; }

        public ContagiosDTO()
        {

        }
    }
}
