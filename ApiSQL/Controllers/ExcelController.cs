using ApiSQL.Models;
using straviaBackend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSQL.Controllers
{
    public class ExcelController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            SqlProvider.processFile();
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] FileModel value)
        {
            //ProcessFiles.saveCsvTemp(value);
            SqlProvider.processFile();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}