using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace APICovidBlazor.Client.Shared
{
    public partial class ConsultaCovid
    {
        [Parameter] public TipoConsulta Tipo { get; set; } 
        public string Edades;
        public bool M;
        public bool F;
        public DateTime Desde = default;
        public DateTime Hasta = default;
        public string Provincia;

        public int Casos { get; set; } = 0;

        private async void ConsultarDatos()
        {
            var consulta = new ConsultaDTO
            {
                Edades = Edades,
                Masculino = M,
                Femenino = F,
                Desde = Desde,
                Hasta = Hasta,
                Provincia = Provincia
            };

            var properties = from p in consulta.GetType().GetProperties()
                             where p.GetValue(consulta, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(consulta, null).ToString());

            string queryString = string.Join("&", properties.ToArray());
            var res = await Http.GetFromJsonAsync<RespuestaConsultaDTO>($"Covid/{(Tipo == TipoConsulta.Contagios ? "total" : "deaths")}?{queryString}");
            Casos = res.Casos;
            StateHasChanged();
        }

        public enum TipoConsulta
        {
            Contagios, Muertes
        }
    }
}
