using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedico.ViewModels {
    public class AlterarSenhaViewModel {

        [Required(ErrorMessage = "Informa a senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informa nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Repita a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirma a senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não correspondem")]
        public string ConfirmacaoSenha { get; set; }

    }
}