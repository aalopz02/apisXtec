using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XTECDigital_MainDB.Controllers
{
    public class EVALUACIONController : ApiController
    {
        // GET: api/EVALUACION
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EVALUACION/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EVALUACION
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/EVALUACION/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EVALUACION/5
        public void Delete(int id)
        {
        }
    }
}
