using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace ApiSQL.Models
{
    /// <summary>
    /// Clase para comunicarse con el api de mongo y obtener datos de estudiantes
    /// </summary>
    public class clienteHttp
    {
        public clienteHttp() { }

        /// <summary>
        /// Obtiene todos los datos de cada estudiante y devuelve la lista de modelos
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAll() {
            ArrayList estudiantes = new ArrayList();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://18.217.104.67/api/INFO?");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return JsonSerializer.Deserialize<IEnumerable<ModelEstudiante>>(reader.ReadToEnd());

            }
            catch (HttpRequestException e)
            {
                estudiantes.Add("error request");
            }

            return estudiantes;
        }

        /// <summary>
        /// Obtiene la lista de carnets y correos de todos los estudiantes
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetCorreos()
        {
            ArrayList estudiantes = new ArrayList();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://18.217.104.67/api/INFO?paramx=2");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return JsonSerializer.Deserialize<IEnumerable<ModelEstudianteCarnet>>(reader.ReadToEnd());

            }
            catch (HttpRequestException e)
            {
                estudiantes.Add("error request");
            }

            return estudiantes;
        }


    }
}