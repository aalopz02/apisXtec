using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class ESTUDIANTE_MATRICULADO
    {
        public String Carnet { get; set; }
        public String Curso_Grupo { get; set; }
        public String Curso_Codigo { get; set; }
        public char Sem_Periodo { get; set; }
        public String Sem_Anno { get; set; }

        public ESTUDIANTE_MATRICULADO(string carnet, string curso_Grupo, string curso_Codigo, char sem_Periodo, string sem_Anno)
        {
            Carnet = carnet;
            Curso_Grupo = curso_Grupo;
            Curso_Codigo = curso_Codigo;
            Sem_Periodo = sem_Periodo;
            Sem_Anno = sem_Anno;
        }
    }
}