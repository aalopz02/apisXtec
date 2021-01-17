using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class EVALUACION
    {
        public EVALUACION(string rubro_Nombre, string curso_Grupo, string curso_Codigo, char sem_Periodo, string sem_Anno, int ent_ID, string est_Carnet, string est_Curso_Grupo, string est_Curso_Codigo, char est_Sem_Periodo, string est_Sem_Anno, string nombre, float peso, string fecha_Entrega, string observaciones, int forma_Evaluacion, float nota, string retroalimentacion, string estado)
        {
            Rubro_Nombre = rubro_Nombre;
            Curso_Grupo = curso_Grupo;
            Curso_Codigo = curso_Codigo;
            Sem_Periodo = sem_Periodo;
            Sem_Anno = sem_Anno;
            Ent_ID = ent_ID;
            Est_Carnet = est_Carnet;
            Est_Curso_Grupo = est_Curso_Grupo;
            Est_Curso_Codigo = est_Curso_Codigo;
            Est_Sem_Periodo = est_Sem_Periodo;
            Est_Sem_Anno = est_Sem_Anno;
            Nombre = nombre;
            Peso = peso;
            Fecha_Entrega = fecha_Entrega;
            Observaciones = observaciones;
            Forma_Evaluacion = forma_Evaluacion;
            Nota = nota;
            Retroalimentacion = retroalimentacion;
            Estado = estado;
        }

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