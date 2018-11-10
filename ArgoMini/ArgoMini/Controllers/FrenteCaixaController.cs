using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;
using ArgoMini.Negocio;

namespace ArgoMini.Controllers
{
    public class FrenteCaixaController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();

        public ActionResult FrenteCaixa()
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");

            if (notaFiscal == null)
                notaFiscal = new NotaFiscalNegocio().CriarNotaFiscal();

            TempData["NotaFiscalSaida"] = notaFiscal;
            try
            {
                return View(notaFiscal);
            }
            catch (Exception ex)
            {
                return View(notaFiscal);
            }

            
        }

        public ActionResult EmitirNotaFiscal()
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            try
            {
                new NotaFiscalNegocio().EmitirNotaFiscal(notaFiscal);
                
                TempData.Remove("NotaFiscalSaida");
            }
            catch(Exception ex)
            {
                
            }
            // retornar para view frente caixa branco
            return RedirectToAction("FrenteCaixa");
        }


        public ActionResult CancelarNotaFiscal()
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            try
            {
                new NotaFiscalNegocio().CancelarNotaFiscal(notaFiscal);

                TempData.Remove("NotaFiscalSaida");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            // retornar para view frente caixa branco
            return RedirectToAction("FrenteCaixa");
        }

        public ActionResult InutilizarNotaFiscal()
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            try
            {
                new NotaFiscalNegocio().InutilizarNotaFiscal(notaFiscal);

                TempData.Remove("NotaFiscalSaida");
            }
            catch (Exception ex)
            {

            }
            // retornar para view frente caixa branco
            return RedirectToAction("FrenteCaixa");
        }

        public ActionResult AddProduto()
        {
            if (TempData["NotaFiscalSaida"] != null)
            {
                var notaFiscal = TempData["NotaFiscalSaida"] as NotaFiscalSaida;
                TempData["NotaFiscalSaida"] = notaFiscal;
                TempData.Keep();
            }
        
        return View();
        }

        [HttpPost]
        public ActionResult AddProduto(FrenteCaixa frenteCaixa)
        {
            try
            {
                var notaFiscal = (NotaFiscalSaida) TempData.Peek("NotaFiscalSaida");
                var notaFiscalItem = new NotaFiscalSaidaItemNegocio().TransformaFrenteCaixa(frenteCaixa, notaFiscal);
                
                if (notaFiscal != null)
                {
                    notaFiscal.Itens.Add(notaFiscalItem);
                    notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);
                }
                    //NotaFiscalSaidaItemNegocio.AdicionarNovoItemNota(ref notaFiscal, notaFiscalItem);

                TempData["NotaFiscalSaida"] = notaFiscal;
                TempData.Keep();
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("FrenteCaixa");
        }
        
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            var notaFiscalSaidaItem = notaFiscal.Itens.FirstOrDefault(c => c.NotaFiscalSaidaItemId == id);
            if (notaFiscalSaidaItem == null)
            {
                return HttpNotFound();
            }
            return View(notaFiscalSaidaItem);
        }

        [HttpPost]
        public ActionResult Editar(NotaFiscalSaidaItem notaFiscalSaidaItem)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
                    var notaItem = notaFiscal.Itens.FirstOrDefault(c =>
                        c.NotaFiscalSaidaItemId == notaFiscalSaidaItem.NotaFiscalSaidaItemId);

                    if (notaItem != null)
                    {
                        notaItem.PrecoVenda = notaFiscalSaidaItem.PrecoVenda;
                        notaItem.Quantidade = notaFiscalSaidaItem.Quantidade;
                        notaItem.TotalMercadoria = notaFiscalSaidaItem.PrecoVenda * notaFiscalSaidaItem.Quantidade;
                    }

                    //NotaFiscalSaidaItemNegocio.EditarItemNota(notaFiscalSaidaItem);
                    notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);

                    TempData["NotaFiscalSaida"] = notaFiscal;
                }
                catch (Exception ex)
                {

                }
                return RedirectToAction("FrenteCaixa");
            }

            return View(notaFiscalSaidaItem);
        }

        public ActionResult Cancelar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            var notaFiscalSaidaItem = notaFiscal.Itens.FirstOrDefault(c =>
                c.NotaFiscalSaidaItemId == id);

            if (notaFiscalSaidaItem == null)
            {
                return HttpNotFound();
            }
            return View(notaFiscalSaidaItem);
        }

        [HttpPost]
        public ActionResult Cancelar(int id)
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            var notaFiscalSaidaItem = notaFiscal.Itens.FirstOrDefault(c =>
                c.NotaFiscalSaidaItemId == id);

            notaFiscal.Itens.Remove(notaFiscalSaidaItem ?? throw new InvalidOperationException());

            notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);

            TempData["NotaFiscalSaida"] = notaFiscal;
            return RedirectToAction("FrenteCaixa");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}