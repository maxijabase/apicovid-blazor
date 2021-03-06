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

        public async Task<RespuestaConsultaDTO> ObtenerCasos(NameValueCollection args, bool muertes = false)
        {
            var resp = new RespuestaConsultaDTO();
            var cons = new ConsultaDTO(args);

            try
            {
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

                if (muertes)
                {
                    query = query.Where(x => x.Fallecido == "SI");
                }

                resp.Casos = query.ToList().Count;

                return resp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaIngresoDTO> IngresarCaso(IngresoDTO ingreso)
        {
            var resp = new RespuestaIngresoDTO();

            try
            {
                var caso = new Casoscovid()
                {
                    Edad = ingreso.Edad,
                    FechaApertura = ingreso.Fecha.ToString("yyyy-MM-dd"),
                    ResidenciaProvinciaNombre = ingreso.Provincia,
                    Sexo = ingreso.Sexo.Substring(0, 1),
                    Fallecido = ingreso.Fallecido ? "SI" : "NO",
                    IdEventoCaso = _context.Casoscovids.Max(x => x.IdEventoCaso) + 1,
                };

                await _context.Casoscovids.AddAsync(caso);
                await _context.SaveChangesAsync();
                resp.Exito = true;
                return resp;
            }
            catch (Exception ex)
            {
                resp.Exito = false;
                resp.Error = ex.Message;
                return resp;
            }

        }

    }
}
