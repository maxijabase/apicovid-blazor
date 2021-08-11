using APICovidBlazor.Clases.Backend;
using APICovidBlazor.Clases.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace APICovidBlazor.Server.Controllers
{
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

        [HttpGet]
        public ActionResult DescargarDataset()
        {
            _covidHelper.DescargarDataset();
            return Ok();
        }

        [HttpGet("total")]
        public ActionResult GetContagios([FromQuery] SearchParams searchParams)
        {
            var contagios = _covidHelper.ObtenerContagios(searchParams);
            return Ok(contagios);
        }

        [HttpGet("deaths")]
        public async Task<ActionResult> GetMuertes([FromQuery] SearchParams searchParams)
        {
            await Task.Delay(2);
            return Ok();
        }
    }
}
