using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models {
    public class Especialidade {
        public int EspecialidadeID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}