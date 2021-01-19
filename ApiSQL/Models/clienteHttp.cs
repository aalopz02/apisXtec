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
    public class clienteHttp
    {
        public clienteHttp() { }

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