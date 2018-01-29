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
    public class SaquesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Saques
        public ActionResult Index()
        {
            var saques = db.Saques.Include(s => s.cliente);
            return View(saques.ToList());
        }

        // GET: Saques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saque saque = db.Saques.Find(id);
            if (saque == null)
            {
                return HttpNotFound();
            }
            return View(saque);
        }

        // GET: Saques/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto");
            return View();
        }

        // POST: Saques/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClienteID,Valor")] Saque saque)
        {
            if (ModelState.IsValid)
            {
                Cliente oCliente = db.Clientes.Find(saque.ClienteID);
                decimal novoSaldo = oCliente.Saldo - saque.Valor;
                if (novoSaldo <= 0)
                {
                    ModelState.AddModelError("", "Erro: Cliente não possui saldo suficiente para o saque.");
                } else
                {
                    oCliente.Saldo = novoSaldo;
                    db.Entry(oCliente).State = EntityState.Modified;

                    db.Saques.Add(saque);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", saque.ClienteID);
            return View(saque);
        }

        // GET: Saques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saque saque = db.Saques.Find(id);
            if (saque == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", saque.ClienteID);
            return View(saque);
        }

        // POST: Saques/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteID,Valor")] Saque saque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ID", "NomeCompleto", saque.ClienteID);
            return View(saque);
        }

        // GET: Saques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Saque saque = db.Saques.Find(id);
            if (saque == null)
            {
                return HttpNotFound();
            }
            return View(saque);
        }

        // POST: Saques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Saque saque = db.Saques.Find(id);
            db.Saques.Remove(saque);
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
