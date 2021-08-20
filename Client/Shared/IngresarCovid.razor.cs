using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace APICovidBlazor.Client.Shared
{
    public partial class IngresarCovid
    {
        public string Edad;
        public DateTime Fecha = DateTime.Today;
        public string Sexo = "Masculino";
        public string Provincia;
        public bool Fallecido;

        public bool Enviando = true;
        public bool DatosErroneos;
        public bool PeticionErronea;
        public string MensajeError;

        private void SexoCambio(ChangeEventArgs e)
        {
            Sexo = e.Value.ToString();
        }

        public async void IngresarDatos()
        {
            Enviando = true;
            if (string.IsNullOrEmpty(Provincia) || !int.TryParse(Edad, out _))
            {
                DatosErroneos = true;
                Enviando = false;
                return;
            }

            DatosErroneos = false;

            var consulta = new IngresoDTO
            {
                Edad = int.Parse(Edad),
                Fecha = Fecha,
                Sexo = Sexo,
                Provincia = Provincia,
                Fallecido = Fallecido
            };

            var res = await Http.PostAsJsonAsync("Covid/update", consulta);
            
            var respDto = JsonConvert.DeserializeObject<RespuestaIngresoDTO>(await res.Content.ReadAsStringAsync());
            PeticionErronea = !respDto.Exito;
            MensajeError = respDto.Error;
            Enviando = false;
            StateHasChanged();
        }
    }
}
