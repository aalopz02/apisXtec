using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class CARRERAController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todas las carreras que existen
        /// </summary>
        /// <returns>Lista de todas las carreras</returns>
        [Route("api/CARRERA")]
        public ArrayList Get()
        {
            return dbConnection.GetCarreras();
        }

        /// <summary>
        /// Método para crear una carrera
        /// </summary>
        /// <param name="carrera">Carrera por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CARRERA/create")]
        public HttpResponseMessage Post([FromBody] CARRERA carrera)
        {
            string status = dbConnection.CreateCarrera(carrera);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Carrera creada correctamente!");
        }

        /// <summary>
        /// Método para eliminar una carrera
        /// </summary>
        /// <param name="carrera">Carrera por eliminar</param>
        /// <returns></returns>
        [Route("api/CARRERA/delete")]
        public HttpResponseMessage Delete([FromBody] CARRERA carrera)
        {
            string response = dbConnection.DeleteCarrera(carrera);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Carrera eliminada correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar la carrera solicitada");
        }
    }
}
