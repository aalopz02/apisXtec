﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.IO;

namespace FrameWork
{
    public class ReportController
    {
        public string Reporte_participantes(string nombreCarrera, string nombreAdmin)
        {
            //Carga del documento
            ReportDocument cryRpt = new ReportDocument();
            string ruta_proyecto = AppDomain.CurrentDomain.BaseDirectory;
            // Metodo alternativo: string ruta_reporte = ruta_proyecto.Replace("bin\\Debug\\", "reporteParticipantesPorCarrera.rpt").Replace("\\","/").ToString();
            string ruta_reporte = ruta_proyecto + "reporteParticipantesPorCarrera.rpt";
            cryRpt.Load(ruta_reporte);
            //Insercion de parametros
            cryRpt.SetParameterValue("nombreCarrera", nombreCarrera);
            cryRpt.SetParameterValue("nombreAdminCarrera", nombreAdmin);
            //Generar y convertir documento
            Stream stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);
            var bytes = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }

        public string Reporte_posiciones(string nombreCarrera, string nombreAdmin)
        {
            //Carga del documento
            ReportDocument cryRpt = new ReportDocument();
            string ruta_proyecto = AppDomain.CurrentDomain.BaseDirectory;
            string ruta_reporte = ruta_proyecto + "reportePosicionesPorCarrera.rpt";
            cryRpt.Load(ruta_reporte);
            //Insercion de parametros
            cryRpt.SetParameterValue("nombreCarrera", nombreCarrera);
            cryRpt.SetParameterValue("nombreAdminCarrera", nombreAdmin);
            //Generar y convertir documento
            Stream stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);
            var bytes = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }
    }
    
}
