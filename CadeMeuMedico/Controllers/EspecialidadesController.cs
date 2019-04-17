using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.DAO;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers {
    public class EspecialidadesController : Controller {
        private EFContext db = new EFContext();

        // GET: Especialidades
        public ActionResult Index() {
            return View(db.Especialidades.ToList());
        }

       // GET: Especialidades/Create
        [Authorize]
        public ActionResult Create() {
            return View();
        }

        // POST: Especialidades/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecialidadeID,Nome")] Especialidade especialidade) {
            if (ModelState.IsValid) {
                if (db.Especialidades.Count(e => e.Nome == especialidade.Nome) > 0) {
                    ModelState.AddModelError("Nome", "Essa especialidade já esta cadastrada");
                    return View(especialidade);
                }
                db.Especialidades.Add(especialidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Especialidades/Edit/5
        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = db.Especialidades.Find(id);
            if (especialidade == null) {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecialidadeID,Nome")] Especialidade especialidade) {
            if (ModelState.IsValid) {
                if (db.Especialidades.Count(e => e.Nome == especialidade.Nome) > 0) {
                    ModelState.AddModelError("Nome", "Essa especialidade já esta cadastrada");
                    return View(especialidade);
                }
                db.Entry(especialidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidade);
        }

        // GET: Especialidades/Delete/5
        [Authorize]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especialidade especialidade = db.Especialidades.Find(id);
            if (especialidade == null) {
                return HttpNotFound();
            }
            return View(especialidade);
        }

        // POST: Especialidades/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Especialidade especialidade = db.Especialidades.Find(id);
            db.Especialidades.Remove(especialidade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}