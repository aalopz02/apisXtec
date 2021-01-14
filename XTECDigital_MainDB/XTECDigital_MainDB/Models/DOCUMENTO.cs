using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTECDigital_MainDB.Models
{
    public class DOCUMENTO
    {
        public String Nombre { get; set; }
        public String Data { get; set; }
        public String Tamanno { get; set; }
        public String Fecha_Subida { get; set; }
        public String Carpeta_Nombre { get; set; }
        public String Curso_Grupo { get; set; }
        public String Curso_Codigo { get; set; }
        public char Sem_Periodo { get; set; }
        public String Sem_Anno { get; set; }

        public DOCUMENTO(string nombre, string data, string tamanno, string fecha_Subida, string carpeta_Nombre, string curso_Grupo, string curso_Codigo, char sem_Periodo, string sem_Anno)
        {
            Nombre = nombre;
            Data = data;
            Tamanno = tamanno;
            Fecha_Subida = fecha_Subida;
            Carpeta_Nombre = carpeta_Nombre;
            Curso_Grupo = curso_Grupo;
            Curso_Codigo = curso_Codigo;
            Sem_Periodo = sem_Periodo;
            Sem_Anno = sem_Anno;
        }
    }
}