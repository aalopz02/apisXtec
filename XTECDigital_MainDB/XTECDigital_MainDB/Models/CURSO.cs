using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTECDigital_MainDB.Models
{
    public class CURSO
    {
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public String Creditos { get; set; }
        public int Carrera_ID { get; set; }

        public CURSO(string codigo, string nombre, string creditos, int carrera_ID)
        {
            Codigo = codigo;
            Nombre = nombre;
            Creditos = creditos;
            Carrera_ID = carrera_ID;
        }
    }
}
