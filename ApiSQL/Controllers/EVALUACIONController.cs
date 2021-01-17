using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class EVALUACIONController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todas las evaluaciones de un tipo, de un rubro, de un curso determinado, de todos los estudiantes (vista profesor)
        /// </summary>
        /// <param name="rubro_nombre">Nombre del rubro</param>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Periodo del año</param>
        /// <param name="nombre">Nombre del tipo de evaluación</param>
        /// <returns>Lista de todas las evaluaciones de un tipo, de un rubro, de todos los estudiantes</returns>
        [Route("api/EVALUACION/Profesor/{rubro_nombre}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}/{nombre}")]
        public ArrayList GetProfesor(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String nombre)
        {
            return dbConnection.GetEvaluacionesProfesor(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno, curso_grupo, curso_codigo, sem_periodo, sem_anno, nombre);
        }

        /// <summary>
        /// Método para obtener todos los tipos de evaluaciones que existen en un rubro, de un curso determinado
        /// </summary>
        /// <param name="rubro_nombre">Nombre del rubro</param>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Periodo del año</param>
        /// <returns>Lista de todas las evaluaciones de un tipo, de un rubro</returns>
        [Route("api/EVALUACION/Profesor/{rubro_nombre}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList GetTipos(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return dbConnection.GetTiposEvaluacion(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno);
        }

        /// <summary>
        /// Método para obtener todas las evaluaciones de un rubro, de un curso determinado (vista estudiante)
        /// </summary>
        /// <param name="rubro_nombre">Nombre del rubro</param>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre</param>
        /// <param name="sem_anno">Periodo del año</param>
        /// <param name="nombre">Nombre del tipo de evaluación</param>
        /// <returns>Lista de todas las evaluaciones, de un estudiante, de un rubro</returns>
        [Route("api/EVALUACION/Estudiante/{carnet}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList GetEstudiante(String rubro_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno, String est_carnet, String est_curso_grupo, String est_curso_codigo, char est_sem_periodo, String est_sem_anno)
        {
            return dbConnection.GetEvaluacionesEstudiante(rubro_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno, est_carnet, est_curso_grupo, est_curso_codigo, est_sem_periodo, est_sem_anno);
        }

        /// <summary>
        /// Método para crear una evalaución
        /// </summary>
        /// <param name="evaluacion">Evaluación por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/EVALUACION/create")]
        public HttpResponseMessage Post([FromBody] EVALUACION evaluacion)
        {
            string status = dbConnection.CreateEvaluacion(evaluacion);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Evaluación creada correctamente!");
        }

        /// <summary>
        /// Método para actualizar una evaluación
        /// </summary>
        /// <param name="evaluacion">Evaluación por actualizar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
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

        /// <summary>
        /// Método par aeliminar una evaluación
        /// </summary>
        /// <param name="evaluacion">Evaluación por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
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
