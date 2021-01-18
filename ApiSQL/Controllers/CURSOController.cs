using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ApiSQL.Models;
using System.Web.Http;
using System.Collections;

namespace ApiSQL.Controllers
{
    public class CURSOController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/CURSO
        public ArrayList Get()
        {
            return dbConnection.GetCursos();
        }

        // GET: api/CURSO/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CURSO
        [Route("api/CURSO/create")]
        public void Post([FromBody] CURSO value)
        {
            dbConnection.CreateCurso(value);
        }

        // PUT: api/CURSO/5
        public void Put(int id, [FromBody]string value)
        {
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
