using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedico.DAO;
using CadeMeuMedico.Models;

namespace CadeMeuMedico.Controllers {
    public class MedicosController : Controller {
        private EFContext db = new EFContext();

        // GET: Medicos
        public ActionResult Index() {
            var medicos = db.Medicos.Include(m => m.Cidade).Include(m => m.Especialidade).Where(m => m.DataAtual == "01-01-9999" );
            return View(medicos.ToList());
        }

        // GET: Medicos/Create
        [Authorize]
        public ActionResult Create() {
            ViewBag.CidadeID = new SelectList(db.Cidades, "CidadeID", "Nome");
            ViewBag.EspecialidadeID = new SelectList(db.Especialidades, "EspecialidadeID", "Nome");
            return View();
        }

        // POST: Medicos/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DataAdd,Nome,CRM,Endereco,Telefone,CidadeID,EspecialidadeID")] Medico medico) {
            if (ModelState.IsValid) {
                int i = db.Medicos.Select(m => m.MedicoID).DefaultIfEmpty(-1).Max();

                if (i == 0) {
                    medico.MedicoID = 1;
                    db.Medicos.Add(medico);
                    db.SaveChanges();
                }
                else {
                    medico.MedicoID = i + 1;
                    db.Medicos.Add(medico);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.CidadeID = new SelectList(db.Cidades, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidades, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id, string data) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id, data);
            if (medico == null) {
                return HttpNotFound();
            }
            ViewBag.CidadeID = new SelectList(db.Cidades, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidades, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedicoID,DataAdd,Nome,CRM,Endereco,Telefone,DataAtual,CidadeID,EspecialidadeID")] Medico medico) {
            Medico m = (Medico)medico.Clone();

            if (ModelState.IsValid) {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                m.DataAdd = medico.DataAtual;
                m.DataAtual = "01-01-9999";
                db.Medicos.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeID = new SelectList(db.Cidades, "CidadeID", "Nome", medico.CidadeID);
            ViewBag.EspecialidadeID = new SelectList(db.Especialidades, "EspecialidadeID", "Nome", medico.EspecialidadeID);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, string data) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id, data);
            if (medico == null) {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string data) {
            Medico medico = db.Medicos.Find(id, data);
            db.Medicos.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}