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
        public List<BsonDocument> Get()
        {
            try
            {
                var connString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connString);
                var allDatabases = client.ListDatabases().ToList();

                return allDatabases;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}