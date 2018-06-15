using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        // GET: Pratos
        public async Task<ActionResult> IndexPrato()
        {
            return View(await db.Pratos.ToListAsync());
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
        public async Task<ActionResult> Create([Bind(Include = "IDPratos,Image,Descricao,TipoPratoFK")] Pratos pratos, HttpPostedFileBase fileUploadFotografia)
        {
            
            // determinar o ID do novo Agente
            int novoID = 0;
            // *****************************************
            // proteger a geração de um novo ID
            // *****************************************
            // determinar o nº de tipo de pratos na tabela
            if (db.Pratos.Count() == 0)
            {
                novoID = 1;
            }
            else
            {
                novoID = db.Pratos.Max(a => a.IDPratos) + 1;
            }
            // atribuir o ID ao novo agente
            pratos.IDPratos = novoID;
            // ***************************************************
            // outra hipótese possível seria utilizar o
            // try { }
            // catch(Exception) { }
            // ***************************************************

            // var. auxiliar
            string nomeFotografia = "pratos" + novoID + ".jpg";
            string caminhoParaFotografia = Path.Combine(Server.MapPath("~/images/"), nomeFotografia); // indica onde a imagem será guardada

            // verificar se chega efetivamente um ficheiro ao servidor
            if (fileUploadFotografia != null)
            {
                // guardar o nome da imagem na BD
                pratos.Image = nomeFotografia;
            }
            else
            {
                // não há imagem...
                ModelState.AddModelError("", "Não foi fornecida uma imagem..."); // gera MSG de erro
                return View(pratos); // reenvia os dados do 'Agente' para a View
            }

            if (ModelState.IsValid)
            {
                // guardar a imagem no disco rígido
                fileUploadFotografia.SaveAs(caminhoParaFotografia);
                db.Pratos.Add(pratos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

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
        public async Task<ActionResult> Edit([Bind(Include = "IDPratos,Image,Descricao,TipoPratoFK")] Pratos pratos, HttpPostedFileBase uploadFoto)
        {
            // vars. auxiliares
            string novoNome = "";
            string nomeAntigo = "";

            if (ModelState.IsValid)
            {
                try
                {              /// se foi fornecida uma nova imagem,
                               /// preparam-se os dados para efetuar a alteração
                    if (uploadFoto != null)
                    {
                        /// antes de se fazer alguma coisa, preserva-se o nome antigo da imagem,
                        /// para depois a remover do disco rígido do servidor
                        nomeAntigo = pratos.Image;
                        /// para o novo nome do ficheiro, vamos adicionar o termo gerado pelo timestamp
                        /// devidamente formatado, mais
                        /// A extensão do ficheiro é obtida automaticamente em vez de ser escrita de forma explícita
                        novoNome = "tipoPrato" + pratos.IDPratos + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + Path.GetExtension(uploadFoto.FileName).ToLower(); ;
                        /// atualizar os dados do Agente com o novo nome
                        pratos.Image = novoNome;
                        /// guardar a nova imagem no disco rígido
                        uploadFoto.SaveAs(Path.Combine(Server.MapPath("~/images/"), novoNome));
                    }

                    // guardar os dados do Agente
                    db.Entry(pratos).State = EntityState.Modified;
                    // Commit
                    db.SaveChanges();

                    /// caso tenha sido fornecida uma nova imagem há necessidade de remover 
                    /// a antiga
                    if (uploadFoto != null)
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/images/"), nomeAntigo));


                    // enviar os dados para a página inicial
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    // caso haja um erro deve ser enviada uma mensagem para o utilizador
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados do tipo de Prato {0}", pratos.Descricao));
                }
            }
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
