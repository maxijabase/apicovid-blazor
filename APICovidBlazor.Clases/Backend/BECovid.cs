using APICovidBlazor.Clases.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace APICovidBlazor.Clases.Backend
{
    public class BECovid
    {

        public void DescargarDataset()
        {

        }

        public ContagiosDTO ObtenerContagios(SearchParams sParams)
        {
            using (TextReader dataCsvFileReader = File.OpenText(@"C:\Users\Maxi\Downloads\Covid19Casos.csv"))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                };
                using CsvReader dataCsvReader = new CsvReader(dataCsvFileReader, config);
                while (dataCsvReader.Read())
                {
                    var dataRecord = Enumerable.ToList(dataCsvReader.GetRecord<dynamic>());
                }
            }
            throw new NotImplementedException();
        }
    }
}
