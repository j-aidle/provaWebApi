using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insti.Models
{
    public class DadaDTO
    {
        public int id { get; set; }
        public AlumneDTO alumne { get; set; }
        public AssignaturaDTO assignatura { get; set; }
        public ProfessorDTO professor { get; set; }
    }
}