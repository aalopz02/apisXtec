using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class ViewController : ApiController
    {
        // GET: api/View
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/View/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/View
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/View/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/View/5
        public void Delete(int id)
        {
        }
    }
}
