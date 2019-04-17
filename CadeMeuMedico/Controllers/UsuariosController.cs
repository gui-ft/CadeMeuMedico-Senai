using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.DAO;
using CadeMeuMedico.Models;
using CadeMeuMedico.Utils;
using CadeMeuMedico.ViewModels;

namespace CadeMeuMedico.Controllers {
    public class UsuariosController : Controller {
        private EFContext db = new EFContext();

        // GET: Usuarios/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadastroUsuarioViewModel usuario) {

            if (!ModelState.IsValid) {
                return View(usuario);
            }

            if (db.Usuarios.Count(u => u.Email == usuario.Email) > 0) {
                ModelState.AddModelError("Email", "Esse Email já está em uso");
                return View(usuario);
            }

            Usuario user = new Usuario {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = Hash.GerarHash(usuario.Senha)
            };

            db.Usuarios.Add(user);
            db.SaveChanges();
            TempData["Mensagem"] = usuario.Nome + " cadastrado com sucesso";
            return View();
        }

        // GET: Usuarios/Login
        public ActionResult Login(string ReturnUrl) {
            var viewModel = new LoginViewModel {
                UrlRetorno = ReturnUrl
            };
            return View(viewModel);
        }

        // POST: Usuario/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel) {
            if (!ModelState.IsValid) {
                return View(loginViewModel);
            }
            var usuario = db.Usuarios.FirstOrDefault(u => u.Email == loginViewModel.Email);

            if (usuario == null) {
                ModelState.AddModelError("Senha", "O Login ou a senha está errada");
                return View(loginViewModel);
            }

            if (usuario.Senha != Hash.GerarHash(loginViewModel.Senha)) {
                ModelState.AddModelError("Senha", "O Login ou a senha está errada");
                return View(loginViewModel);
            }

            var identity = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Email", usuario.Email)
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);
            if (!string.IsNullOrWhiteSpace(loginViewModel.UrlRetorno) || Url.IsLocalUrl(loginViewModel.UrlRetorno))
                return Redirect(loginViewModel.UrlRetorno);
            else return RedirectToAction("Index", "Medicos");
        }

        //GET : Usuarios/Logout
        public ActionResult Logout() {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
