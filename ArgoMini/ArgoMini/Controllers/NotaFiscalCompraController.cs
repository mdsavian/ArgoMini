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
    public class NotaFiscalCompraController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();

        public ActionResult ImportarNotaFiscalCompraView()
        {
            return View();
        }

        public ActionResult ImportarNota()
        {
            var notaCompra = NotaFiscalCompraNegocio.ConsultarNotaCompra();

            if (notaCompra == null)
                return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");

            TempData["notaCompra"] = notaCompra;
            TempData.Keep("notaCompra");

            return RedirectToAction("NotaFiscalCompraDetalhe", "NotaFiscalCompra");
        }

        public ActionResult NotaFiscalCompraDetalhe()
        {
            var notaCompra = (NotaFiscalCompra)TempData.Peek("notaCompra");

            if (notaCompra == null)
                return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");

            return View(notaCompra);
        }

        //public ActionResult EditarNotaFiscalCompraItem(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var notaFiscalCompraItem = _context.NotaFiscalCompraItem.SingleOrDefault(e => e.Id == id);
        //    if (notaFiscalCompraItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notaFiscalCompraItem);
        //}

        //[HttpPost]
        //public ActionResult EditarNotaFiscalCompraItem(NotaFiscalCompraItem notaFiscalCompraItem)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var notaCompra = (NotaFiscalCompra)TempData.Peek("notaCompra");

        //            NotaFiscalCompraItemNegocio.EditarItemNota(notaFiscalCompraItem);

        //            notaCompra = NotaFiscalCompraNegocio.AtualizarValorNota(notaCompra.Id);

        //            TempData["NotaFiscalSaida"] = notaFiscal;
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        return RedirectToAction("FrenteCaixa");
        //    }

        //    return View(notaFiscalSaidaItem);
        //}

        //public ActionResult Cancelar(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var notaFiscalSaidaItem = _context.NotaFiscalSaidaItems.SingleOrDefault(e => e.NotaFiscalSaidaItemId == id);
        //    if (notaFiscalSaidaItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notaFiscalSaidaItem);
        //}

        //[HttpPost]
        //public ActionResult Cancelar(int id)
        //{
        //    var notaFiscalSaidaItem = _context.NotaFiscalSaidaItems.FirstOrDefault(c => c.NotaFiscalSaidaItemId == id);

        //    var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
        //    _context.NotaFiscalSaidaItems.Remove(notaFiscalSaidaItem ?? throw new InvalidOperationException());
        //    _context.SaveChanges();

        //    notaFiscal = NotaFiscalNegocio.AtualizarValorNota(notaFiscal.NotaFiscalSaidaId);

        //    TempData["NotaFiscalSaida"] = notaFiscal;
        //    return RedirectToAction("FrenteCaixa");
        //}



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