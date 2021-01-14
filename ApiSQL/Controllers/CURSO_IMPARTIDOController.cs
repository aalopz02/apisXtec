using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class CURSO_IMPARTIDOController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener todos los cursos impartidos según profesor
        /// </summary>
        /// <param name="cedula">Cédula de un profesor</param>
        /// <returns>Lista de cursos impartidos asociados a la cédula dada</returns>
        [Route("api/CURSO_IMPARTIDO/Profesor/{cedula}")]
        public ArrayList GetCursosProfesor(String cedula)
        {
            return dbConnection.GetProfesorCursoImpartido(cedula);
        }

        /// <summary>
        /// Método para obtener todos los cursos impartidos según estudiante
        /// </summary>
        /// <param name="carnet">Carnet de un estudiante</param>
        /// <returns>Lista de cursos impartidos asociados al carnet dado</returns>
        [Route("api/CURSO_IMPARTIDO/Estudiante/{carnet}")]
        public ArrayList GetCursosEstudiante(String carnet)
        {
            return dbConnection.GetEstudianteCursoImpartido(carnet);
        }

        /// <summary>
        /// Método para crear un curso impartido
        /// </summary>
        /// <param name="curso_impartido">Curso impartido por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/CURSO_IMPARTIDO/create")]
        public HttpResponseMessage Post([FromBody] CURSO_IMPARTIDO curso_impartido)
        {
            string status = dbConnection.CreateCursoImpartido(curso_impartido);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Curso_Impartido creado correctamente!");
        }

        /// Aquí se debe insertar el código del controller para inicializar semestre que hizo Andrés
    }
}
