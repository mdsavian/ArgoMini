using System;
using System.Data.Entity;
using System.Linq;
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
            return View(notaFiscal);
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
                    NotaFiscalSaidaItemNegocio.AdicionarNovoItemNota(ref notaFiscal, notaFiscalItem);

                TempData["NotaFiscalSaida"] = notaFiscal;
                TempData.Keep();
            }
            catch (Exception e)
            {

            }

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