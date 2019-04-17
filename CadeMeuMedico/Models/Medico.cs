using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models {
    public class Medico :ICloneable {
        public int MedicoID { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CRM { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string DataAdd { get; set; }
        public string DataAtual { get; set; } = "01-01-9999";
        public int CidadeID { get; set; }
        public int EspecialidadeID { get; set; }

        [ForeignKey("CidadeID")]
        public virtual Cidade Cidade { get; set; }

        [ForeignKey("EspecialidadeID")]
        public virtual Especialidade Especialidade { get; set; }

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}