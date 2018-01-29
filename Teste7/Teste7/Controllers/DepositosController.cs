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
    public class DepositosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Depositos
        public ActionResult Index()
        {
            var depositoes = db.Depositoes.Include(d => d.cliente);
            return View(depositoes.ToList());
        }

        // GET: Depositos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposito deposito = db.Depositoes.Find(id);
            if (deposito == null)
            {
                return HttpNotFound();
            }
            return View(deposito);
        }

        // GET: Depositos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto");
            return View();
        }

        // POST: Depositos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClienteID,Valor")] Deposito deposito)
        {
            if (ModelState.IsValid)
            {
                Cliente oCliente = db.Clientes.Find(deposito.ClienteID);
                oCliente.Saldo = oCliente.Saldo + deposito.Valor;
                db.Entry(oCliente).State = EntityState.Modified;

                db.Depositoes.Add(deposito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", deposito.ClienteID);
            return View(deposito);
        }

        // GET: Depositos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposito deposito = db.Depositoes.Find(id);
            if (deposito == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", deposito.ClienteID);
            return View(deposito);
        }

        // POST: Depositos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteID,Valor")] Deposito deposito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deposito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", deposito.ClienteID);
            return View(deposito);
        }

        // GET: Depositos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposito deposito = db.Depositoes.Find(id);
            if (deposito == null)
            {
                return HttpNotFound();
            }
            return View(deposito);
        }

        // POST: Depositos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deposito deposito = db.Depositoes.Find(id);
            db.Depositoes.Remove(deposito);
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
