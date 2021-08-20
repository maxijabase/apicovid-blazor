using APICovidBlazor.Clases.Backend;
using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;

namespace APICovidBlazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidController : Controller
    {
        private readonly BECovid _covidHelper;
        public CovidController(
            BECovid covidHelper)
        {
            _covidHelper = covidHelper;
        }

        [HttpGet("existeDataset")]
        public ActionResult<ExisteDatasetDTO> DatasetExists()
        {
            var existeDataset = System.IO.File.Exists(@"..\Covid19Casos.csv");
            if (existeDataset && !System.IO.File.Exists(@"..\Covid19Casos.sqlite"))
            {
                BEDatos.CargarDatosIniciales();
            }
            return new ExisteDatasetDTO()
            {
                Existe = existeDataset
            };
        }

        [HttpGet("total")]
        public async Task<ActionResult<RespuestaConsultaDTO>> GetContagios()
        {
            var args = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value);
            var response = await _covidHelper.ObtenerCasos(args);
            return Ok(response);
        }

        [HttpGet("deaths")]
        public async Task<ActionResult> GetMuertes()
        {
            var args = HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value);
            var response = await _covidHelper.ObtenerCasos(args, true);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<ActionResult<RespuestaIngresoDTO>> PostCaso([FromBody] IngresoDTO ingreso)
        {
            var response = await _covidHelper.IngresarCaso(ingreso);
            return Ok(response);
        }
    }
}
