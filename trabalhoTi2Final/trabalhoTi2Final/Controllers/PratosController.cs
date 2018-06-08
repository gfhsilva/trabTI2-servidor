using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trabalhoTi2Final.Models;

namespace trabalhoTi2Final.Controllers
{
    public class PratosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pratos
        public ActionResult Index()
        {
            var pratos = db.Pratos.Include(p => p.TipoPrato);
            return View(pratos.ToList());
        }

        // GET: Pratos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            return View(pratos);
        }

        // GET: Pratos/Create
        public ActionResult Create()
        {
            ViewBag.TipoPratoFK = new SelectList(db.TipoPratos, "IDTipoPrato", "Designacao");
            return View();
        }

        // POST: Pratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPratos,Image,Descricao,TipoPratoFK")] Pratos pratos)
        {
            if (ModelState.IsValid)
            {
                db.Pratos.Add(pratos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoPratoFK = new SelectList(db.TipoPratos, "IDTipoPrato", "Designacao", pratos.TipoPratoFK);
            return View(pratos);
        }

        // GET: Pratos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoPratoFK = new SelectList(db.TipoPratos, "IDTipoPrato", "Designacao", pratos.TipoPratoFK);
            return View(pratos);
        }

        // POST: Pratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPratos,Image,Descricao,TipoPratoFK")] Pratos pratos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pratos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoPratoFK = new SelectList(db.TipoPratos, "IDTipoPrato", "Designacao", pratos.TipoPratoFK);
            return View(pratos);
        }

        // GET: Pratos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pratos pratos = db.Pratos.Find(id);
            if (pratos == null)
            {
                return HttpNotFound();
            }
            return View(pratos);
        }

        // POST: Pratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pratos pratos = db.Pratos.Find(id);
            db.Pratos.Remove(pratos);
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
