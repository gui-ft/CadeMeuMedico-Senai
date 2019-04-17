using CadeMeuMedico.DAO;
using CadeMeuMedico.Utils;
using CadeMeuMedico.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedico.Controllers {
    public class PerfilController : Controller {
        private EFContext db = new EFContext();

        // GET: Perfil
        [Authorize]
        public ActionResult AlterarSenha() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel alterarSenhaViewModel) {
            if (!ModelState.IsValid) {
                return View(alterarSenhaViewModel);
            }

            var identity = User.Identity as ClaimsIdentity;
            var email = identity.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            var usuario = db.Usuarios.FirstOrDefault(u => u.Email == email);

            if (Hash.GerarHash(alterarSenhaViewModel.SenhaAtual) != usuario.Senha){
                ModelState.AddModelError("Senha Atual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(alterarSenhaViewModel.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Painel");

        }
    }
}