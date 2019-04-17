using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.ViewModels {
    public class CadastroUsuarioViewModel {
            [Required(ErrorMessage = "Informe o nome")]
            [MaxLength(30, ErrorMessage = "No máximo 30 caracteres")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "Informe o email")]
            [MaxLength(30, ErrorMessage = "No máximo 30 caracteres")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Informe a senha")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "No mínimo 6 caracteres")]
            public string Senha { get; set; }

            [Required(ErrorMessage = "Informe a senha novamente")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirme a Senha")]
            [MinLength(6, ErrorMessage = "No mínimo 6 caracteres")]
            [Compare(nameof(Senha), ErrorMessage = "As senhas digitadas estão diferentes")]
            public string ConfirmacaoSenha { get; set; }
        }
}