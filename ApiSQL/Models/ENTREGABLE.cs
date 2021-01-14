using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class ENTREGABLE
    {
        public int ID { get; set; }
        public String Data { get; set; }

        public ENTREGABLE(int iD, string data)
        {
            ID = iD;
            Data = data;
        }
    }
}