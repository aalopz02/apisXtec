using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class RUBROController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener una lista de rubros de un curso dado
        /// </summary>
        /// <param name="curso_grupo">Grupo del curso</param>
        /// <param name="curso_codigo">Código del curso</param>
        /// <param name="sem_periodo">Periodo del semestre del curso</param>
        /// <param name="sem_anno">Año del semestre del curso</param>
        /// <returns>Lista de rubros asociados al curso dado</returns>
        [Route("api/RUBRO/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList Get(String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return dbConnection.GetRubros(curso_grupo, curso_codigo, sem_periodo, sem_anno); //metodo de la base para obtener todas las carpetas de un curso
        }

        /// <summary>
        /// Método para crear rubro
        /// </summary>
        /// <param name="rubro">Rubro por crear</param>
        /// <returns>ensaje sobre el estado de la operación</returns>
        [Route("api/RUBRO/create")]
        public HttpResponseMessage Post([FromBody] RUBRO rubro)
        {
            string status = dbConnection.CreateRubro(rubro);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Rubro creado correctamente!");
        }

        /// <summary>
        /// Método para actualizar los datos de un rubro
        /// </summary>
        /// <param name="rubro">Rubro modificado</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/RUBRO/update")]
        public HttpResponseMessage Put([FromBody] RUBRO rubro)
        {
            string response = dbConnection.UpdateRubro(rubro);
            if (response.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Rubro actualizado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Rubro no encontrado");
        }

        /// <summary>
        /// Método para eliminar un rubro
        /// </summary>
        /// <param name="rubro">Rubro por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/RUBRO/delete")]
        public HttpResponseMessage Delete([FromBody] RUBRO rubro)
        {
            string response = dbConnection.DeleteRubro(rubro);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Rubro eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el rubro solicitado");
        }
    }
}
