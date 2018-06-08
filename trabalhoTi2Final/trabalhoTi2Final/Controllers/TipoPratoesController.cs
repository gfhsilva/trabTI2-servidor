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
using System.IO;

namespace trabalhoTi2Final.Administrador
{
    public class TipoPratoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoPratoes
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoPratos.ToListAsync());
        }

        // GET: TipoPratoes
        public async Task<ActionResult> IndexTipoPrato()
        {
            return View(await db.TipoPratos.ToListAsync());
        }

        // GET: TipoPratoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrato tipoPrato = await db.TipoPratos.FindAsync(id);
            if (tipoPrato == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrato);
        }

        // GET: TipoPratoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPratoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTipoPrato,Designacao,Image")] TipoPrato tipoPrato, HttpPostedFileBase fileUploadFotografia)
        {
            // determinar o ID do novo Agente
            int novoID = 0;
            // *****************************************
            // proteger a geração de um novo ID
            // *****************************************
            // determinar o nº de tipo de pratos na tabela
            if (db.TipoPratos.Count() == 0)
            {
                novoID = 1;
            }
            else
            {
                novoID = db.TipoPratos.Max(a => a.IDTipoPrato) + 1;
            }
            // atribuir o ID ao novo agente
            tipoPrato.IDTipoPrato = novoID;
            // ***************************************************
            // outra hipótese possível seria utilizar o
            // try { }
            // catch(Exception) { }
            // ***************************************************

            // var. auxiliar
            string nomeFotografia = "tipoPrato" + novoID + ".jpg";
            string caminhoParaFotografia = Path.Combine(Server.MapPath("~/images/"), nomeFotografia); // indica onde a imagem será guardada

            // verificar se chega efetivamente um ficheiro ao servidor
            if (fileUploadFotografia != null)
            {
                // guardar o nome da imagem na BD
                tipoPrato.Image = nomeFotografia;
            }
            else
            {
                // não há imagem...
                ModelState.AddModelError("", "Não foi fornecida uma imagem..."); // gera MSG de erro
                return View(tipoPrato); // reenvia os dados do 'Agente' para a View
            }

            if (ModelState.IsValid)
            {
                // guardar a imagem no disco rígido
                fileUploadFotografia.SaveAs(caminhoParaFotografia);
                db.TipoPratos.Add(tipoPrato);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoPrato);
        }

        // GET: TipoPratoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrato tipoPrato = await db.TipoPratos.FindAsync(id);
            if (tipoPrato == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrato);
        }

        // POST: TipoPratoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTipoPrato,Designacao,Image")] TipoPrato tipoPrato, HttpPostedFileBase uploadFoto)
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
                        nomeAntigo = tipoPrato.Image;
                        /// para o novo nome do ficheiro, vamos adicionar o termo gerado pelo timestamp
                        /// devidamente formatado, mais
                        /// A extensão do ficheiro é obtida automaticamente em vez de ser escrita de forma explícita
                        novoNome = "tipoPrato" + tipoPrato.IDTipoPrato + DateTime.Now.ToString("_yyyyMMdd_hhmmss") + Path.GetExtension(uploadFoto.FileName).ToLower(); ;
                        /// atualizar os dados do Agente com o novo nome
                        tipoPrato.Image = novoNome;
                        /// guardar a nova imagem no disco rígido
                        uploadFoto.SaveAs(Path.Combine(Server.MapPath("~/images/"), novoNome));
                    }

                    // guardar os dados do Agente
                    db.Entry(tipoPrato).State = EntityState.Modified;
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
                    ModelState.AddModelError("", string.Format("Ocorreu um erro com a edição dos dados do tipo de Prato {0}", tipoPrato.Designacao));
                }
            }
            return View(tipoPrato);
        }
        // GET: TipoPratoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPrato tipoPrato = await db.TipoPratos.FindAsync(id);
            if (tipoPrato == null)
            {
                return HttpNotFound();
            }
            return View(tipoPrato);
        }

        // POST: TipoPratoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoPrato tipoPrato = await db.TipoPratos.FindAsync(id);
            db.TipoPratos.Remove(tipoPrato);
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
