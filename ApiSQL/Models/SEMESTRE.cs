using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiSQL.Models
{
    public class SEMESTRE
    {
        public char Periodo { get; set; }
        public String Anno { get; set; }

        public SEMESTRE(char periodo, string anno)
        {
            Periodo = periodo;
            Anno = anno;
        }
    }
}