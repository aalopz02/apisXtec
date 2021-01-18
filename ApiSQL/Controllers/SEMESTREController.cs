using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;
using System.Collections;

namespace ApiSQL.Controllers
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


        // GET: api/SEMESTRE/5
        public ArrayList Get(int id)
        {
            return dbConnection.GetSemestre();
        }

        [Route("api/SEMESTRE/create/{anno}/{perido}")]
        // POST: api/SEMESTRE
        public void Post(String anno, String perido, [FromBody] cursoModel value)
        {
            dbConnection.PostSemestreCurso(anno, perido, value);
        }

        // PUT: api/SEMESTRE/5
        public void Put(int id, [FromBody]string value)
        {

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
