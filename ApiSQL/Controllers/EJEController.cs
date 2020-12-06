using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSQL.Controllers
{
    public class EJEController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "ok" };
        }
    }
}
