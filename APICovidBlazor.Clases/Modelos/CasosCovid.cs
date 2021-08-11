using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICovidBlazor.Clases.Modelos
{
    [DelimitedRecord(",")]
    public class Covid19Casos
    {
        [FieldOptional()]
        public string? Id_evento_caso { get; set; }

        [FieldOptional()]
        public string? Sexo { get; set; }

        [FieldOptional()]
        public string? Edad { get; set; }

        [FieldOptional()]
        public string? Edad_años_meses { get; set; }

        [FieldOptional()]
        public string? Residencia_pais_nombre { get; set; }

        [FieldOptional()]
        public string? Residencia_provincia_nombre { get; set; }

        [FieldOptional()]
        public string? Residencia_departamento_nombre { get; set; }

        [FieldOptional()]
        public string? Carga_provincia_nombre { get; set; }

        [FieldOptional()]
        public string? Fecha_inicio_sintomas { get; set; }

        [FieldOptional()]
        public string? Fecha_apertura { get; set; }

        [FieldOptional()]
        public string? Sepi_apertura { get; set; }

        [FieldOptional()]
        public string? Fecha_internacion { get; set; }

        [FieldOptional()]
        public string? Cuidado_intensivo { get; set; }

        [FieldOptional()]
        public string? Fecha_cui_intensivo { get; set; }

        [FieldOptional()]
        public string? Fallecido { get; set; }

        [FieldOptional()]
        public string? Fecha_fallecimiento { get; set; }

        [FieldOptional()]
        public string? Asistencia_respiratoria_mecanica { get; set; }

        [FieldOptional()]
        public string? Carga_provincia_id { get; set; }

        [FieldOptional()]
        public string? Origen_financiamiento { get; set; }

        [FieldOptional()]
        public string? Clasificacion { get; set; }

        [FieldOptional()]
        public string? Clasificacion_resumen { get; set; }

        [FieldOptional()]
        public string? Residencia_provincia_id { get; set; }

        [FieldOptional()]
        public string? Fecha_diagnostico { get; set; }

        [FieldOptional()]
        public string? Residencia_departamento_id { get; set; }

        [FieldOptional()]
        public string? Ultima_actualizacion { get; set; }
    }
}