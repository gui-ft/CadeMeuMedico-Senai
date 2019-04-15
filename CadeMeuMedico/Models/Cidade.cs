using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models {
    public class Cidade {
        public int CidadeID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}