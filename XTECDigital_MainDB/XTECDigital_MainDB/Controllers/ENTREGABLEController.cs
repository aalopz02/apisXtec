using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XTECDigital_MainDB.Controllers
{
    public class ENTREGABLEController : ApiController
    {
        // GET: api/ENTREGABLE
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ENTREGABLE/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ENTREGABLE
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ENTREGABLE/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ENTREGABLE/5
        public void Delete(int id)
        {
        }
    }
}
