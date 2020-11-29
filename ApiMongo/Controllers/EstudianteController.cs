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
        // GET: Estudiantes
        public ICollection<BsonDocument> Get()
        {
            return MongoDBAccess.GetEstudianteDocuments().Find(new BsonDocument()).ToList();
        }

        // GET: Estudiante by carnet
        public BsonDocument GetEstudiante(int carnet) {
            var carnetQuery = new BsonDocument("Carnet", carnet);
            foreach (BsonDocument estudiante in MongoDBAccess.GetEstudianteDocuments().Find(new BsonDocument()).ToList()) {
                BsonValue x;
                estudiante.TryGetValue("Carnet", out x);
                if (x.Equals(carnet)) {
                    return estudiante;
                }
            }
            return null;
        }

        //Patch estudiante
        //https://localhost:44370/api/Estudiante?carnetEstudiante=12345&nombre=Andres_A&correo=aalopz02@gmial.com&telefono=8888&contrasenna=clave
        public void PatchEstudiante(int carnetEstudiante, String nombre, String correo, int telefono, String contrasenna)
        {
            BsonDocument old = GetEstudiante(carnetEstudiante);
            if (old == null)
            {
                return;
            }
            else {
                BsonValue x;
                old.TryGetValue("Carnet", out x);
                BsonValue pass = contrasenna;
                if (pass == null)
                {
                    old.TryGetValue("Contrasenna", out pass);
                }
                else {
                    pass = Encript.EncriptString(contrasenna);
                }
                var document = new BsonDocument {

                { "Carnet", int.Parse(x.ToString()) },
                { "Nombre", nombre },
                { "Correo", correo },
                { "Telefono", telefono },
                { "Contrasenna", pass }
                };
                BsonValue id;
                old.TryGetValue("_id", out id);
                MongoDBAccess.GetEstudianteDocuments().UpdateOne(new BsonDocument("_id", id),new BsonDocument("$set", document));
            }

        }

        //Post estudiante
        //https://localhost:44370/api/Estudiante?carnet=12345&nombre=Andres_A&correo=aalopz02@gmial.com&telefono=8888&contrasenna=clave
        public void PostEstudiante(int carnet, String nombre, String correo, int telefono, String contrasenna) {

            var document = new BsonDocument { 
                { "Carnet", carnet },
                { "Nombre", nombre },
                { "Correo", correo },
                { "Telefono", telefono },
                { "Contrasenna", Encript.EncriptString(contrasenna) }
            };
            MongoDBAccess.GetEstudianteDocuments().InsertOne(document);
        }
    }
}