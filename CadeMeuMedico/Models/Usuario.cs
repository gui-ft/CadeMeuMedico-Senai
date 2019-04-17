using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.Models {
    public class Usuario {
        public int UsuarioID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}