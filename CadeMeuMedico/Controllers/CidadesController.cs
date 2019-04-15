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
    public class CidadesController : Controller {
        private EFContext db = new EFContext();

        // GET: Cidades
        public ActionResult Index() {
            return View(db.Cidades.ToList());
        }

        // GET: Cidades/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Cidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CidadeID,Nome")] Cidade cidade) {
            if (ModelState.IsValid) {
                db.Cidades.Add(cidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null) {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CidadeID,Nome")] Cidade cidade) {
            if (ModelState.IsValid) {
                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cidade cidade = db.Cidades.Find(id);
            if (cidade == null) {
                return HttpNotFound();
            }
            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Cidade cidade = db.Cidades.Find(id);
            db.Cidades.Remove(cidade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}