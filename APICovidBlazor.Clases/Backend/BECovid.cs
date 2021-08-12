using APICovidBlazor.Clases.Modelos;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
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
        private readonly BEDatos _datosHelper;

        public BECovid(
            BEDatos datosHelper)
        {
            _datosHelper = datosHelper;
        }



    }
}
