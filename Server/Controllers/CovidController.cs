using APICovidBlazor.Clases.Backend;
using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SQLite;

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
        public ActionResult<bool> DatasetExists()
        {
            return System.IO.File.Exists(@".\Covid19Casos.csv");
        }

        [HttpGet("bajarDataset")]
        public async Task<ActionResult<bool>> DescargarDataset()
        {
            await _covidHelper.DescargarDataset();
            return Ok();
        }

        [HttpGet("/covid/total")]
        public ActionResult GetContagios([FromQuery] SearchParamsContagios searchParams)
        {
            var contagios = _covidHelper.ObtenerContagios(searchParams);
            return Ok(contagios);
        }

        [HttpGet("deaths")]
        public async Task<ActionResult> GetUpdate([FromQuery] SearchParamsUpdate searchParams)
        {
            await Task.Delay(2);
            return Ok();
        }

        //[HttpPost]
        //public async Task<ActionResult> PostUpdate([FromBody] )
    }
}
