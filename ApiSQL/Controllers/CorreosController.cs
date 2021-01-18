using ApiSQL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSQL.Controllers
{
    public class CorreosController : ApiController
    {
        clienteHttp cliente = new clienteHttp();

        [Route("api/Correos/allInfo")]
        public IEnumerable getAll() {
            return cliente.GetAll();
        }

        [Route("api/Correos")]
        public IEnumerable getCorreos()
        {
            return cliente.GetCorreos();
        }
    }
}
