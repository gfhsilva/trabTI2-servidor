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
    public class Refeicaos1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Refeicaos1
        public async Task<ActionResult> Index()
        {
            var refeicoes = db.Refeicoes.Include(r => r.Prato).Include(r => r.Utilizador);
            return View(await refeicoes.ToListAsync());
        }

        // GET: Refeicaos1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeicao refeicao = await db.Refeicoes.FindAsync(id);
            if (refeicao == null)
            {
                return HttpNotFound();
            }
            return View(refeicao);
        }

        // GET: Refeicaos1/Create
        public ActionResult Create()
        {
            ViewBag.PratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao");
            ViewBag.utilizadorFK = new SelectList(db.Utilizadores, "IDUtilizador", "Nome");
            return View();
        }

        // POST: Refeicaos1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDRefeicao,Periodo,Dia,DataReserva,Fornecido,utilizadorFK,PratosFk")] Refeicao refeicao)
        {
            if (ModelState.IsValid)
            {
                db.Refeicoes.Add(refeicao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", refeicao.PratosFk);
            ViewBag.utilizadorFK = new SelectList(db.Utilizadores, "IDUtilizador", "Nome", refeicao.utilizadorFK);
            return View(refeicao);
        }

        // GET: Refeicaos1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeicao refeicao = await db.Refeicoes.FindAsync(id);
            if (refeicao == null)
            {
                return HttpNotFound();
            }
            ViewBag.PratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", refeicao.PratosFk);
            ViewBag.utilizadorFK = new SelectList(db.Utilizadores, "IDUtilizador", "Nome", refeicao.utilizadorFK);
            return View(refeicao);
        }

        // POST: Refeicaos1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDRefeicao,Periodo,Dia,DataReserva,Fornecido,utilizadorFK,PratosFk")] Refeicao refeicao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(refeicao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PratosFk = new SelectList(db.Pratos, "IDPratos", "Descricao", refeicao.PratosFk);
            ViewBag.utilizadorFK = new SelectList(db.Utilizadores, "IDUtilizador", "Nome", refeicao.utilizadorFK);
            return View(refeicao);
        }

        // GET: Refeicaos1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Refeicao refeicao = await db.Refeicoes.FindAsync(id);
            if (refeicao == null)
            {
                return HttpNotFound();
            }
            return View(refeicao);
        }

        // POST: Refeicaos1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Refeicao refeicao = await db.Refeicoes.FindAsync(id);
            db.Refeicoes.Remove(refeicao);
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
