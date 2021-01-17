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
    public class ProfesorController : ApiController
    {
        // GET: Profesor
        public ICollection<BsonDocument> Get()
        {
            return MongoDBAccess.GetProfesoresDocuments().Find(new BsonDocument()).ToList();
        }

        // GET: Profesor by cedula
        [System.Web.Http.Cors.DisableCors()]
        public BsonDocument GetProfesor(String cedula)
        {
            var carnetQuery = new BsonDocument("Cedula", cedula.ToString());
            foreach (BsonDocument profesor in MongoDBAccess.GetProfesoresDocuments().Find(new BsonDocument()).ToList())
            {
                BsonValue x;
                profesor.TryGetValue("Cedula", out x);
                if (x.Equals(cedula))
                {
                    return profesor;
                }
            }
            return null;
        }

        //Patch profesor
        //https://localhost:44370/api/Profesor?cedulaProfesor=12345&nombre=Andres_A&correo=aalopz02@gmial.com&contrasenna=clave
        public void PatchProfesor(String cedulaProfesor, String nombre, String correo, String contrasenna)
        {
            BsonDocument old = GetProfesor(cedulaProfesor);
            if (old == null)
            {
                return;
            }
            else
            {
                BsonValue x;
                old.TryGetValue("Cedula", out x);
                BsonValue pass = contrasenna;
                if (pass == null)
                {
                    old.TryGetValue("Contrasenna", out pass);
                }
                else
                {
                    pass = Encript.EncriptString(contrasenna);
                }
                var document = new BsonDocument {

                { "Cedula", x.ToString() },
                { "Nombre", nombre },
                { "Correo", correo },
                { "Contrasenna", pass }
                };
                BsonValue id;
                old.TryGetValue("_id", out id);
                MongoDBAccess.GetProfesoresDocuments().UpdateOne(new BsonDocument("_id", id), new BsonDocument("$set", document));
            }

        }

        //Post profesor
        //https://localhost:44370/api/Profesor?cedula=12345&nombre=Profesor1&correo=aalopz02@gmial.com&contrasenna=clave
        public void PostProfesor(String cedula, String nombre, String correo, String contrasenna)
        {

            var document = new BsonDocument {
                { "Cedula", cedula },
                { "Nombre", nombre },
                { "Correo", correo },
                { "Contrasenna", Encript.EncriptString(contrasenna) }
            };
            MongoDBAccess.GetProfesoresDocuments().InsertOne(document);
        }
    }
}