using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teste7.Models;

namespace Teste7.Controllers
{
    public class TransferenciasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Transferencias
        public ActionResult Index()
        {
            var transferencias = db.Transferencias.Include(t => t.ClientePagador).Include(t => t.ClienteRecebedor);
            return View(transferencias.ToList());
        }

        // GET: Transferencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // GET: Transferencias/Create
        public ActionResult Create()
        {
            ViewBag.ClientePagadorID = new SelectList(db.Clientes, "ID", "NomeCompleto");
            ViewBag.ClienteRecebedorID = new SelectList(db.Clientes, "ID", "NomeCompleto");
            return View();
        }

        // POST: Transferencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientePagadorID,ClienteRecebedorID,Valor")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                Cliente oClienteRec = db.Clientes.Find(transferencia.ClienteRecebedorID);
                oClienteRec.Saldo = oClienteRec.Saldo + transferencia.Valor;
                db.Entry(oClienteRec).State = EntityState.Modified;

                Cliente oClientePag = db.Clientes.Find(transferencia.ClientePagadorID);
                oClientePag.Saldo = oClientePag.Saldo - transferencia.Valor;
                db.Entry(oClientePag).State = EntityState.Modified;

                db.Transferencias.Add(transferencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientePagadorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClientePagadorID);
            ViewBag.ClienteRecebedorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClienteRecebedorID);
            return View(transferencia);
        }

        // GET: Transferencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientePagadorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClientePagadorID);
            ViewBag.ClienteRecebedorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClienteRecebedorID);
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientePagadorID,ClienteRecebedorID,Valor")] Transferencia transferencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transferencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientePagadorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClientePagadorID);
            ViewBag.ClienteRecebedorID = new SelectList(db.Clientes, "ID", "NomeCompleto", transferencia.ClienteRecebedorID);
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transferencia transferencia = db.Transferencias.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transferencia transferencia = db.Transferencias.Find(id);
            db.Transferencias.Remove(transferencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
