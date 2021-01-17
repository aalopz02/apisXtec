using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class CURSOController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todos los cursos que existen y pueden ser impartidos
        /// </summary>
        /// <returns>Lista de todos los cursos</returns>
        [Route("api/CURSO")]
        public ArrayList Get()
        {
            return dbConnection.GetCursos();
        }

        /// <summary>
        /// Método para crear un curso
        /// </summary>
        /// <param name="curso">Curso por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CURSO/create")]
        public HttpResponseMessage Post([FromBody] CURSO curso)
        {
            string status = dbConnection.CreateCurso(curso);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Curso creado correctamente!");
        }

        /// <summary>
        /// Método para eliminar un curso
        /// </summary>
        /// <param name="curso">Curso por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CURSO/delete")]
        public HttpResponseMessage Delete([FromBody] CURSO curso)
        {
            string response = dbConnection.DeleteCurso(curso);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Curso eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el curso solicitado");
        }
    }
}
