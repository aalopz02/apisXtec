using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{
    public class ExpedienteController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todos los cursos que está llevando y ha llevado un estudiante según su carnet
        /// </summary>
        /// <param name="carnet">Carnet del estudiante</param>
        /// <returns>Lista de cursos</returns>
        [Route("api/EXPEDIENTE/{carnet}")]
        public ArrayList Get(String carnet)
        {
            return dbConnection.GetExpediente(carnet); 
        }
    }
}
