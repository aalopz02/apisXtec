﻿using ApiSQL.Models;
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
        private DBConnection dbConnection = new DBConnection();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            dbConnection.processFile();
            return "value";
        }

        // POST api/<controller>
        public string Post([FromBody] FileModel value)
        {
            String writeMsj = ProcessFiles.saveCsvTemp(value);
            if (!writeMsj.Equals("tempWriteOk")) {
                return writeMsj;
            }
            String fill = dbConnection.processFile();
            return fill;
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