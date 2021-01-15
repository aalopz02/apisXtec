using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiMongo.Controllers
{
    public class AdminController : ApiController
    {
        public ICollection<BsonDocument> Get()
        {
            return MongoDBAccess.GetAdminDocuments().Find(new BsonDocument()).ToList();
        }

        // GET: Admin by id
        public BsonDocument GetAdmin(int id)
        {
            var carnetQuery = new BsonDocument("id", id);
            foreach (BsonDocument admin in MongoDBAccess.GetAdminDocuments().Find(new BsonDocument()).ToList())
            {
                BsonValue x;
                admin.TryGetValue("id", out x);
                if (x.Equals(id))
                {
                    return admin;
                }
            }
            return null;
        }
    }
        
}