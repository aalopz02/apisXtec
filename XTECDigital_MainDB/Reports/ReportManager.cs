using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.IO;

namespace Reports
{
    public class ReportManager
    {
        public String ReporteNotasEstudiante(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_carnet)
        {
            //Carga del documento
            ReportDocument cryRpt = new ReportDocument();
            string ruta_proyecto = AppDomain.CurrentDomain.BaseDirectory;
            // Metodo alternativo: string ruta_reporte = ruta_proyecto.Replace("bin\\Debug\\", "reporteParticipantesPorCarrera.rpt").Replace("\\","/").ToString();
            string ruta_reporte = ruta_proyecto + "ReporteNotasEstudiante.rpt";
            cryRpt.Load(ruta_reporte);
            //Insercion de parametros
            cryRpt.SetParameterValue("Curso_Grupo", curso_grupo);
            cryRpt.SetParameterValue("curso_codigo", curso_codigo);
            cryRpt.SetParameterValue("Sem_Periodo", sem_periodo);
            cryRpt.SetParameterValue("Sem_Año", sem_anno);
            cryRpt.SetParameterValue("Est_Carnet", est_carnet);
            //Generar y convertir documento
            Stream stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);
            var bytes = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }

        public String ReporteNotasProfesor(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            //Carga del documento
            ReportDocument cryRpt = new ReportDocument();
            string ruta_proyecto = AppDomain.CurrentDomain.BaseDirectory;
            string ruta_reporte = ruta_proyecto + "ReporteNotasProfesor.rpt";
            cryRpt.Load(ruta_reporte);
            //Insercion de parametros
            cryRpt.SetParameterValue("Curso_Grupo", curso_grupo);
            cryRpt.SetParameterValue("curso_codigo", curso_codigo);
            cryRpt.SetParameterValue("Sem_Periodo", sem_periodo);
            cryRpt.SetParameterValue("Sem_Año", sem_anno);
            //Generar y convertir documento
            Stream stream = cryRpt.ExportToStream(ExportFormatType.PortableDocFormat);
            var bytes = new byte[(int)stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }
    }
    
}
