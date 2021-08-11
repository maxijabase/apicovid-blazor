using APICovidBlazor.Clases.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Backend
{
    public class BECovid
    {

        public async Task<bool> DescargarDataset()
        {
            bool correct = false;
            try
            {
                using var client = new WebClient();
                await client.DownloadFileTaskAsync(new Uri("https://sisa.msal.gov.ar/datos/descargas/covid-19/files/Covid19Casos.zip"), "../Covid19Casos.zip");
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler((sender, e) =>
                {
                    ZipFile.ExtractToDirectory("../Covid19Casos.zip", "../");
                    correct = true;
                });
            }
            catch (Exception)
            {

            }
            return correct;
        }

        public int ObtenerContagios(SearchParamsContagios sParams)
        {
            int casos = 0;
            using (TextReader dataCsvFileReader = File.OpenText(@"..\Covid19Casos.csv"))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                };
                using CsvReader dataCsvReader = new CsvReader(dataCsvFileReader, config);
                int limit = 0;
                var fechasInicio = new List<string>();
                while (dataCsvReader.Read() && limit < 10000)
                {
                    limit++;
                    var dataRecord = dataCsvReader.GetRecord<Covid19Casos>();
                    // fecha del caso es mas nuevo que el desde? si va
                    if (sParams.Desde != null && !string.IsNullOrEmpty(dataRecord.Fecha_apertura) &&
                        DateTime.Parse(dataRecord.Fecha_apertura).CompareTo(sParams.Desde) > 0)
                    {
                        casos++;
                    }
                    // fecha del caso es mas viejo que el hasta? si va
                    if (sParams.Hasta != null && !string.IsNullOrEmpty(dataRecord.Fecha_apertura) &&
                        DateTime.Parse(dataRecord.Fecha_apertura).CompareTo(sParams.Hasta) < 0)
                    {
                        casos++;
                    }
                    if (sParams.Edades != null && !string.IsNullOrEmpty(dataRecord.Edad))
                    {
                        int? edadCaso = int.Parse(dataRecord.Edad);
                        if (sParams.Edades.Contains('-'))
                        {
                            var edades = sParams.Edades.Split('-');
                            if (edadCaso > int.Parse(edades[0]) && edadCaso < int.Parse(edades[1]))
                            {
                                casos++;
                            }
                        }
                        else
                        {
                            if (edadCaso == int.Parse(sParams.Edades))
                            {
                                casos++;
                            }
                        }
                    }
                    if (sParams.Genero != null && !string.IsNullOrEmpty(dataRecord.Sexo) && sParams.Genero == dataRecord.Sexo)
                    {
                        casos++;
                    }
                    if (sParams.Provincia != null && !string.IsNullOrEmpty(dataRecord.Carga_provincia_nombre) &&
                        sParams.Provincia.ToLower() == dataRecord.Carga_provincia_nombre.ToLower())
                    {
                        casos++;
                    }
                }
            }
            return casos;
        }
    }
}
