﻿using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XTECDigital_MainDB.Models;

namespace XTECDigital_MainDB.Controllers
{

    public class DOCUMENTOController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        /// <summary>
        /// Método para obtener una lista de documentos de una carpeta dada
        /// </summary>
        /// <param name="carpeta_nombre">Nombre de la carpeta</param>
        /// <param name="curso_grupo">Grupo del curso al que pertenece la carpeta</param>
        /// <param name="curso_codigo">Código del curso al que pertenece la carpeta</param>
        /// <param name="sem_periodo">Periodo del semestre del curso al que pertenece la carpeta</param>
        /// <param name="sem_anno">Año del semestre del curso al que pertenece la carpeta</param>
        /// <returns>Lista de documentos asociados a la carpeta dada</returns>
        [Route("api/DOCUMENTO/{carpeta_nombre}/{curso_grupo}/{curso_codigo}/{sem_periodo}/{sem_anno}")]
        public ArrayList Get(String carpeta_nombre, String curso_grupo, String curso_codigo, char sem_periodo, String sem_anno)
        {
            return dbConnection.GetDocumentos(carpeta_nombre, curso_grupo, curso_codigo, sem_periodo, sem_anno); //metodo de la base para obtener todos los documentos de un curso
        }

        /// <summary>
        /// Método para crear un documento
        /// </summary>
        /// <param name="document">Documento por crear</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/DOCUMENTO/create")]
        public HttpResponseMessage Post([FromBody] DOCUMENTO document)
        {
            string status = dbConnection.CreateDocumento(document);
            if (!status.Equals("OK"))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, status);
            }
            return Request.CreateResponse(HttpStatusCode.Created, "¡Documento creado correctamente!");
        }

        /// <summary>
        /// Método para eliminar un documento
        /// </summary>
        /// <param name="document">Documento por eliminar</param>
        /// <returns>Mensaje sobre el estado de la operación</returns>
        [Route("api/DOCUMENTO/delete")]
        public HttpResponseMessage Delete([FromBody] DOCUMENTO document)
        {
            string response = dbConnection.DeleteDocumento(document);
            if (!response.Equals("404"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "¡Documento eliminado correctamente!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No se pudo encontrar el documento solicitado");
        }
    }
}