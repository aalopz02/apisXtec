using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class CARRERA
    {
        public int ID{ get; set; }
        public String Nombre { get; set; }

        public CARRERA(int iD, string nombre)
        {
            ID = iD;
            Nombre = nombre;
        }
    }
}