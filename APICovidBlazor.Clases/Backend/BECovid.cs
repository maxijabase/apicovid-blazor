using APICovidBlazor.Clases.Modelos;
using APICovidBlazor.Clases.Entidades;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APICovidBlazor.Clases.Backend
{
    public class BECovid
    {
        private readonly Covid19CasosContext _context;

        public BECovid(
            Covid19CasosContext context)
        {
            _context = context;
        }

        public async Task<RespuestaConsultaDTO> ObtenerContagios(NameValueCollection args)
        {
            var resp = new RespuestaConsultaDTO();
            var cons = new ConsultaDTO(args);

            var query = from caso in await _context.Casoscovids.ToListAsync()
                        select caso;

            if (!string.IsNullOrEmpty(cons.Edades))
            {
                if (cons.Edades.Contains('-'))
                {
                    var edades = cons.Edades.Split('-');
                    query = query.Where(x => x.Edad > int.Parse(edades[0]) && x.Edad < int.Parse(edades[1]));
                }
                else
                {
                    query = query.Where(x => x.Edad == int.Parse(cons.Edades));
                }
            }

            if (cons.Desde != default)
            {
                query = query.Where(x => DateTime.Parse(x.FechaApertura) > cons.Desde);
            }
            if (cons.Hasta != default)
            {
                query = query.Where(x => DateTime.Parse(x.FechaApertura) < cons.Hasta);
            }

            if (cons.Masculino)
            {
                query = query.Where(x => x.Sexo == "M");
            }
            if (cons.Femenino)
            {
                query = query.Where(x => x.Sexo == "F");
            }

            if (!string.IsNullOrEmpty(cons.Provincia))
            {
                query = query.Where(x => x.ResidenciaProvinciaNombre == cons.Provincia);
            }

            resp.Contagios = query.ToList().Count;

            return resp;
        }
       
    }
}
