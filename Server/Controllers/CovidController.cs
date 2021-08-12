﻿using APICovidBlazor.Clases.Backend;
using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace APICovidBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidController : Controller
    {
        private readonly BECovid _covidHelper;
        private readonly BEDatos _datosHelper;
        public CovidController(
            BECovid covidHelper,
            BEDatos datosHelper)
        {
            _covidHelper = covidHelper;
            _datosHelper = datosHelper;
        }

        [HttpGet("existeDataset")]
        public ActionResult<ExisteDatasetDTO> DatasetExists()
        {
            var existeDataset = System.IO.File.Exists(@"..\Covid19Casos.csv");
            if (!existeDataset)
            {
                BEDatos.CargarDatosIniciales();
            }
            return new ExisteDatasetDTO()
            {
                Existe = existeDataset
            };
        }

        [HttpGet("/covid/total")]
        public ActionResult GetContagios()
        {
            var args = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value);
            BEDatos.CargarDatosIniciales();
            return Ok();
        }

        [HttpGet("deaths")]
        public async Task<ActionResult> GetUpdate()
        {
            await Task.Delay(2);
            return Ok();
        }

        //[HttpPost]
        //public async Task<ActionResult> PostUpdate([FromBody] )
    }
}
