using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class SEMESTREController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todos los semestres que han existido
        /// </summary>
        /// <returns>Lista de todos los cursos</returns>
        [Route("api/SEMESTRE")]
        public ArrayList Get()
        {
            return dbConnection.GetSemestres();
        }

        /// <summary>
        /// Método para crear un semestre
        /// </summary>
        /// <param name="curso">Semestre por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/SEMESTRE/create")]
        public HttpResponseMessage Post([FromBody] SEMESTRE semestre)
        {
            string status = dbConnection.CreateSemestre(semestre);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Semestre creado correctamente!");
        }

        /// <summary>
        /// Método para eliminar un semestre
        /// </summary>
        /// <param name="curso">Curso por semestre</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/SEMESTRE/delete")]
        public HttpResponseMessage Delete([FromBody] SEMESTRE semestre)
        {
            string response = dbConnection.DeleteSemestre(semestre);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Semestre eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el semestre solicitado");
        }
    }
}
