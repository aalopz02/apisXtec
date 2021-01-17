using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ApiSQL.Models;
using System.Web.Http;
using System.Collections;

namespace ApiSQL.Controllers
{
    public class CURSOController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/CURSO
        public ArrayList Get()
        {
            return dbConnection.GetCursos();
        }

        // GET: api/CURSO/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CURSO
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CURSO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CURSO/5
        public void Delete(int id)
        {
        }
    }
}
