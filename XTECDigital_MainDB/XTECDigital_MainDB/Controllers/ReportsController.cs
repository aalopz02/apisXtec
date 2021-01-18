using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;
using Reports;


namespace XTECDigital_MainDB.Controllers
{
    public class ReportsController : ApiController
    {
        private ReportManager rep = new ReportManager();
        
        [Route("api/Report/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public String GetProf(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return rep.ReporteNotasProfesor(curso_grupo, curso_codigo, sem_periodo, sem_anno);
        }

        [Route("api/Report/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}/{est_carnet}")]
        public String GetEst(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_carnet)
        {
            return rep.ReporteNotasEstudiante(curso_grupo, curso_codigo, sem_periodo, sem_anno, est_carnet);
        }
    }
}
