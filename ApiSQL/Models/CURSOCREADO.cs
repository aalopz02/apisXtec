using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class cursoModel
    {
        public String codigoCurso { get; set; }
        public String numeroGrupo { get; set; }
        public String profesor1 { get; set; }
        public String profesor2 { get; set; }
        public ArrayList grupo { get; set; }

        public cursoModel(String codigoCurso, String numeroGrupo, String profesor1, String profesor2, ArrayList grupo) {
            this.codigoCurso = codigoCurso;
            this.numeroGrupo = numeroGrupo;
            this.profesor1 = profesor1;
            this.profesor2 = profesor2;
            this.grupo = grupo;
        }
    }
}
