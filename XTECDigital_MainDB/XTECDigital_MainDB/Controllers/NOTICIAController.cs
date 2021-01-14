using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class NOTICIAController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todas las noticias de un curso
        /// </summary>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre del curso</param>
        /// <param name="sem_anno">Año del semestre del curso</param>
        /// <returns>Lista de noticias asociadas a un curso dado</returns>
        [Route("api/NOTICIA/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList Get(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return dbConnection.GetNoticia(curso_grupo, curso_codigo, sem_periodo, sem_anno);
        }

        /// <summary>
        /// Método para crear una noticia
        /// </summary>
        /// <param name="noticia">Noticia por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/NOTICIA/create")]
        public HttpResponseMessage Post([FromBody] NOTICIA noticia)
        {
            string status = dbConnection.CreateNoticia(noticia);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Noticia creada correctamente!");
        }
    }
}
