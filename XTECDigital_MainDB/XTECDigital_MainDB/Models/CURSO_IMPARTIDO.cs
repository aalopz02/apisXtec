using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTECDigital_MainDB.Models
{
    public class CURSO_IMPARTIDO
    {
        public String Grupo { get; set; }
        public String Curso_Codigo { get; set; }
        public char Sem_Periodo { get; set; }
        public String Sem_Anno { get; set; }

        public CURSO_IMPARTIDO(string grupo, string curso_Codigo, char sem_Periodo, string sem_Anno)
        {
            Grupo = grupo;
            Curso_Codigo = curso_Codigo;
            Sem_Periodo = sem_Periodo;
            Sem_Anno = sem_Anno;
        }
    }
}