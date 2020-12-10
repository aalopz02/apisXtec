using ApiSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSQL.Controllers
{
    public class EJEController : ApiController
    {
        public IEnumerable<string> Get()
        {// IEnumerable<string> 
            OdbcConnection server = SqlProvider.GetConnection();
            server.Open();
            OdbcCommand command = new OdbcCommand("Select * from sales.stores", server);
            OdbcDataReader x = command.ExecuteReader();//new List<string> { 
            DataRowCollection y = x.GetSchemaTable().Rows;

            return new List<string> { };

        }
    }
}
