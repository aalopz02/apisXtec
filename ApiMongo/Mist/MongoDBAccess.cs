using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMongo.Controllers
{
    public class MongoDBAccess
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase database = client.GetDatabase("xtecDB");

        public MongoDBAccess() {
        }

        public static IMongoCollection<BsonDocument> GetEstudianteDocuments() {

            return database.GetCollection<BsonDocument>("Estudiantes");
        }

        public static IMongoCollection<BsonDocument> GetProfesoresDocuments()
        {

            return database.GetCollection<BsonDocument>("Profesores");
        }
        public static IMongoCollection<BsonDocument> GetAdminDocuments()
        {

            return database.GetCollection<BsonDocument>("Admin");
        }
    }
}