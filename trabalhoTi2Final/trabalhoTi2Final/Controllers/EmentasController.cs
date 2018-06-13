using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trabalhoTi2Final.Models;

namespace trabalhoTi2Final.Controllers
{
    public class EmentasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ementas
        public async Task<ActionResult> Index()
        {
            var ementas = db.Ementas.Include(e => e.Prato);
            return View(await ementas.ToListAsync());
        }

        // GET: Ementas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ementas ementas = await db.Ementas.FindAsync(id);
            if (ementas == null)
            {
                return HttpNotFound();
            }
            return View(ementas);
        }

        // GET: Ementas/Create
        public ActionResult Create()
        {
            ViewBag.pratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao");
            return View();
        }

        // POST: Ementas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDEmentas,Dia,Periodo,pratosFk")] Ementas ementas)
        {
            if (ModelState.IsValid)
            {
                db.Ementas.Add(ementas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.pratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", ementas.pratosFk);
            return View(ementas);
        }

        // GET: Ementas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ementas ementas = await db.Ementas.FindAsync(id);
            if (ementas == null)
            {
                return HttpNotFound();
            }
            ViewBag.pratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", ementas.pratosFk);
            return View(ementas);
        }

        // POST: Ementas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDEmentas,Dia,Periodo,pratosFk")] Ementas ementas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ementas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.pratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", ementas.pratosFk);
            return View(ementas);
        }

        // GET: Ementas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ementas ementas = await db.Ementas.FindAsync(id);
            if (ementas == null)
            {
                return HttpNotFound();
            }
            return View(ementas);
        }

        // POST: Ementas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ementas ementas = await db.Ementas.FindAsync(id);
            db.Ementas.Remove(ementas);
            await db.SaveChangesAsync();
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
