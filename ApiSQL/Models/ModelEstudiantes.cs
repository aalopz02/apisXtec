using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class ModelEstudiante
    {
        public String carnet { get; set; }
        public String nombre { get; set; }
        public String correo { get; set; }
        public int telefono { get; set; }
        public String contrasenna { get; set; }

        public ModelEstudiante(string carnet, string nombre, string correo, int telefono, string contrasenna)
        {
            this.carnet = carnet;
            this.nombre = nombre;
            this.correo = correo;
            this.telefono = telefono;
            this.contrasenna = contrasenna;
        }
    }
}
