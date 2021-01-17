using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;
using System.Collections;

namespace ApiSQL.Controllers
{
    public class SEMESTREController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        // GET: api/SEMESTRE
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SEMESTRE/5
        public ArrayList Get(int id)
        {
            return dbConnection.GetSemestre();
        }

        [Route("api/SEMESTRE/create/{anno}/{perido}")]
        // POST: api/SEMESTRE
        public void Post(String anno, String perido, [FromBody] cursoModel value)
        {
            dbConnection.PostSemestreCurso(anno, perido, value);
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
