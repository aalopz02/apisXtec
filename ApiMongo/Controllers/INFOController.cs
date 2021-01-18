using ApiMongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiMongo.Controllers
{
    public class INFOController : ApiController
    {
       
        public ArrayList Get()
        {
            ArrayList estudiantes = new ArrayList();
            foreach (BsonDocument estudiante in MongoDBAccess.GetEstudianteDocuments().Find(new BsonDocument()).ToList())
            {
                BsonValue carnet;
                estudiante.TryGetValue("Carnet", out carnet);
                BsonValue nombre;
                estudiante.TryGetValue("Nombre", out nombre);
                BsonValue correo;
                estudiante.TryGetValue("Correo", out correo);
                BsonValue telefono;
                estudiante.TryGetValue("Telefono", out telefono);
                BsonValue contrasenna;
                estudiante.TryGetValue("Contrasenna", out contrasenna);

                estudiantes.Add(new ModelEstudiante(carnet.ToString(), nombre.ToString(), correo.ToString(), int.Parse(telefono.ToString()), contrasenna.ToString()));
            }
            return estudiantes;
        }


        public ArrayList Get(string paramx)
        {
            ArrayList estudiantes = new ArrayList();
            ICollection<BsonDocument> documentos = MongoDBAccess.GetEstudianteDocuments().Find(new BsonDocument()).ToList();
            foreach (BsonDocument estudiante in documentos)
            {
                BsonValue carnet;
                estudiante.TryGetValue("Carnet", out carnet);
                BsonValue correo;
                estudiante.TryGetValue("Correo", out correo);

                estudiantes.Add(new ModelEstudianteCarnet(carnet.ToString(), correo.ToString()));

            }
            return estudiantes;
        }

    }
}