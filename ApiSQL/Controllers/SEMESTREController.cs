using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class SEMESTREController : ApiController
    {
        // GET: api/SEMESTRE
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SEMESTRE/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SEMESTRE
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SEMESTRE/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SEMESTRE/5
        public void Delete(int id)
        {
        }
    }
}
