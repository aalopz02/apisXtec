using ApiMongo.Mist;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiMongo.Controllers
{
    public class EstudianteController : ApiController
    {
        // GET: Estudiante
        public ICollection<BsonDocument> Get()
        {
            return MongoDBAccess.getEstudianteDocuments().Find(new BsonDocument()).ToList();
        }

        //https://localhost:44370/api/Estudiante?carnet=12345&nombre="Andres A"&correo="aalopz02@gmial.com"&telefono=8888&contrasenna="clave"
        public void PostEstudiante(int carnet, String nombre, String correo, int telefono, String contrasenna) {

            var document = new BsonDocument { 
                { "Carnet", carnet },
                { "Nombre", nombre },
                { "Correo", correo },
                { "Telefono", telefono },
                { "Contrasenna", Encript.EncriptString(contrasenna) }
            };
            MongoDBAccess.getEstudianteDocuments().InsertOne(document);
        }
    }
}