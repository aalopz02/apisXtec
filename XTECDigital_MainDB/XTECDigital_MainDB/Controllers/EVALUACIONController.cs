using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class EVALUACIONController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();

        [Route("api/EVALUACION/Profesor/{rubro_nombre}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}/{est_curso_grupo}/{est_curso_codigo}/{est_sem_periodo}/{est_sem_anno}")]
        public ArrayList GetProfesor(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_curso_grupo, String est_curso_codigo, char est_sem_periodo, String est_sem_anno, String nombre)
        {
            return dbConnection.GetEvaluacionesProfesor(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno, est_curso_grupo, est_curso_codigo, est_sem_periodo, est_sem_anno, nombre); 
        }

        [Route("api/EVALUACION/Estudiante/{carnet}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList GetEstudiante(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_carnet, String est_curso_grupo, String est_curso_codigo, char est_sem_periodo, String est_sem_anno, String nombre)
        {
            return dbConnection.GetEvaluacionesEstudiante(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno, est_carnet, est_curso_grupo, est_curso_codigo, est_sem_periodo, est_sem_anno, nombre); 
        }

        [Route("api/EVALUACION/create/")]
        public HttpResponseMessage Post([FromBody] EVALUACION evaluacion)
        {
            string status = dbConnection.CreateEvaluacion(evaluacion);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Evaluación creada correctamente!");
        }

        [Route("api/EVALUACION/update")]
        public HttpResponseMessage Put([FromBody] EVALUACION evaluacion)
        {
            string response = dbConnection.UpdateEvaluacion(evaluacion);
            if (response.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Evaluación actualizada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Evaluación no encontrada");
        }

        [Route("api/EVALUACION/delete")]
        public HttpResponseMessage Delete([FromBody] EVALUACION evaluacion)
        {
            string response = dbConnection.DeleteEvaluacion(evaluacion);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Evaluación eliminada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar la carpeta solicitada");
        }
    }
}
