using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;
using System.Web.Http.Cors;

namespace ApiSQL.Controllers
{
    [EnableCors(origins: "http://201.237.134.97", headers: "*", methods: "*")]
    public class CARPETAController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener una lista de carpetas de un curso dado
        /// </summary>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre del curso</param>
        /// <param name="sem_anno">Año del semestre del curso</param>
        /// <returns>Lista de carpetas asociadas al curso dado</returns>
        [Route("api/CARPETA/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        //[Route("api/CARPETA")]
        public ArrayList Get(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return dbConnection.GetCarpetas(curso_grupo, curso_codigo, sem_periodo, sem_anno); //metodo de la base para obtener todas las carpetas de un curso
        }

        /// <summary>
        /// Método para crear una carpeta
        /// </summary>
        /// <param name="folder">Carpeta por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CARPETA/create")]
        public HttpResponseMessage Post([FromBody] CARPETA folder)
        {
            string status = dbConnection.CreateCarpeta(folder);
            if(!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Carpeta creada correctamente!");
        }

        /// <summary>
        /// Método para actualizar los datos de una carpeta
        /// </summary>
        /// <param name="origfolder">Datos de carpeta original</param>
        /// <param name="modfolder">Datos de carpeta modificada</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CARPETA/update")]
        public HttpResponseMessage Put([FromBody] CARPETA origfolder, [FromBody] CARPETA modfolder)
        {
            string response = dbConnection.UpdateCarpeta(origfolder, modfolder);
            if (response.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Carpeta actualizada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Carpeta no encontrada");
        }

        /// <summary>
        /// Método para eliminar una carpeta
        /// </summary>
        /// <param name="folder">Carpeta por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CARPETA/delete")]
        public HttpResponseMessage Delete([FromBody] CARPETA folder)
        {
            string response = dbConnection.DeleteCarpeta(folder);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Carpeta eliminada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar la carpeta solicitada");
        }
    }
}
