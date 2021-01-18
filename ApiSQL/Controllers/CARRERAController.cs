using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSQL.Models;

namespace ApiSQL.Controllers
{
    public class CARRERAController : ApiController
    {
        private DBConnection dbConnection = new DBConnection();
        public ArrayList Get()
        {
            return dbConnection.GetCarreras();
        }
    }
}
