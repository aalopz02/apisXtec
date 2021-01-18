using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class ENTREGABLEController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener un entregable
        /// </summary>
        /// <returns>Entregable solicitado</returns>
        [Route("api/ENTREGABLE/id")]
        public ENTREGABLE Get(int id)
        {
            return dbConnection.GetEntregable(id);
        }

        /// <summary>
        /// Método para actualizar un entregable (es el que se utiliza para las entregas en evaluaciones)
        /// </summary>
        /// <param name="entregable">Entregable por actualizar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/ENTREGABLE/update")]
        public HttpResponseMessage Put([FromBody] ENTREGABLE entregable)
        {
            string response = dbConnection.UpdateEntregable(entregable);
            if (response.Equals("200"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Entregable actualizado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Entregable no encontrado");
        }
    }
}
