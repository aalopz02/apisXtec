using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTECDigital_MainDB.Models
{
    public class NOTICIA
    {
        public String Curso_Grupo { get; set; }
        public String Curso_Codigo { get; set; }
        public char Sem_Periodo { get; set; }
        public String Sem_Anno { get; set; }
        public String Titulo { get; set; }
        public String Autor { get; set; }
        public String Fecha { get; set; }
        public String Mensaje { get; set; }

        public NOTICIA(string curso_Grupo, string curso_Codigo, char sem_Periodo, string sem_Anno, string titulo, string autor, string fecha, string mensaje)
        {
            Curso_Grupo = curso_Grupo;
            Curso_Codigo = curso_Codigo;
            Sem_Periodo = sem_Periodo;
            Sem_Anno = sem_Anno;
            Titulo = titulo;
            Autor = autor;
            Fecha = fecha;
            Mensaje = mensaje;
        }
    }
}