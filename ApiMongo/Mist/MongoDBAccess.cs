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
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }

        public static string Connection = "mongodb+srv://admin:admin@cluster0.gbcc0.mongodb.net/<dbname>?connect=replicaSet&retryWrites=true&w=majority";

        public static string DataBase = "xtecInfo";
        public MongoDBAccess() {
        }
        internal static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(Connection);
                database = client.GetDatabase(DataBase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IMongoCollection<BsonDocument> GetEstudianteDocuments()
        {
            ConnectToMongoService();
            return database.GetCollection<BsonDocument>("Estudiantes");
        }

        public static IMongoCollection<BsonDocument> GetProfesoresDocuments()
        {
            ConnectToMongoService();
            return database.GetCollection<BsonDocument>("Profesores");
        }
        public static IMongoCollection<BsonDocument> GetAdminDocuments()
        {
            ConnectToMongoService();
            return database.GetCollection<BsonDocument>("Admin");
        }
    }
}