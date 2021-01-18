using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiMongo.Models
{
    public class ModelEstudianteCarnet
    {
        public String carnet { get; set; }
        public String correo { get; set; }

        public ModelEstudianteCarnet(string carnet, string correo)
        {
            this.carnet = carnet;
            this.correo = correo;
        }
    }
}