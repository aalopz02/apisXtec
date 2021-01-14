using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class EVALUACION
    {
        public String Rubro_Nombre { get; set; }
        public String Curso_Grupo { get; set; }
        public String Curso_Codigo { get; set; }
        public char Sem_Periodo { get; set; }
        public String Sem_Anno { get; set; }
        public int Ent_ID { get; set; }
        public String Est_Carnet { get; set; }
        public String Est_Curso_Grupo { get; set; }
        public String Est_Curso_Codigo { get; set; }
        public char Est_Sem_Periodo { get; set; }
        public String Est_Sem_Anno { get; set; }
        public String Nombre { get; set; }
        public float Peso { get; set; }
        public String Fecha_Entrega { get; set; }
        public String Observaciones { get; set; }
        public int Forma_Evaluacion { get; set; }
        public float Nota { get; set; }
        public String Retroalimentacion { get; set; }
        public String Estado { get; set; }


    }
}