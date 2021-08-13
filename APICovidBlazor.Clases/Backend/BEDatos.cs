using APICovidBlazor.Clases.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Backend
{
    public class BEDatos
    {

        private static readonly string[] CamposAInsertar =
        {
            "sexo", "edad", "fecha_apertura", "residencia_provincia_nombre"
        };

        public static void CargarDatosIniciales()
        {
            CrearSQLDesdeCSV();
            var connection = new SQLiteConnection(@"Data Source=..\Covid19Casos.sqlite;Version=3;");
            using TextReader dataCsvFileReader = File.OpenText(@"..\Covid19Casos.csv");
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using CsvReader dataCsvReader = new CsvReader(dataCsvFileReader, config);
            int limit = 0;
            string insertQuery = "INSERT INTO casoscovid VALUES ";
            while (dataCsvReader.Read() && limit < 1000)
            {
                limit++;
                var dataRecord = dataCsvReader.GetRecord<Covid19Casos>();
                var drProps = dataRecord.GetType().GetProperties().Where(x => CamposAInsertar.Contains(x.Name.ToLower())).ToArray();
                insertQuery += "(";
                for (int i = 0; i < drProps.Length; i++)
                {
                    insertQuery += $"{(string.IsNullOrEmpty(drProps[i].GetValue(dataRecord, null).ToString()) ? "''" : $"'{drProps[i].GetValue(dataRecord, null)}'")}";
                    if (i != drProps.Length - 1)
                    {
                        insertQuery += ", ";
                    }
                    else
                    {
                        insertQuery += "), ";
                    }
                }
            }
            insertQuery = insertQuery.Remove(insertQuery.LastIndexOf(','), 1) + ";";
            connection.Open();
            var command = new SQLiteCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static void CrearSQLDesdeCSV()
        {
            SQLiteConnection.CreateFile(@"..\Covid19Casos.sqlite");
            var connection = new SQLiteConnection(@"Data Source=..\Covid19Casos.sqlite;Version=3;");
            string tablesQuery = "CREATE TABLE casoscovid (";
            using TextReader dataCsvFileReader = File.OpenText(@"..\Covid19Casos.csv");
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using CsvReader dataCsvReader = new CsvReader(dataCsvFileReader, config);
            dataCsvReader.Read();
            dataCsvReader.ReadHeader();
            var records = dataCsvReader.HeaderRecord.Where(x => CamposAInsertar.Contains(x)).ToArray();
            for (int i = 0; i < records.Length; i++)
            {
                tablesQuery += $"{records[i]} varchar";
                if (i != records.Length - 1)
                {
                    tablesQuery += ", ";
                }
                else
                {
                    tablesQuery += ");";
                }
            }
            connection.Open();
            var command = new SQLiteCommand(tablesQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
