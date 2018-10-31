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

        public ActionResult ImportarNotaFiscalCompra()
        {
            var xx = NotaFiscalCompraNegocio.Consultar();
            if (string.IsNullOrEmpty(xx))
                xx = NotaFiscalCompraNegocio.Consultar();
            var xy = xx;
            return View();
        }

        //public ActionResult EmitirNotaFiscal()
        //{
        //    return RedirectToAction("FrenteCaixa", "FrenteCaixa");
        //}

        //public ActionResult Detalhes(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var notaFiscal = _context.NotasFiscalSaida.SingleOrDefault(e => e.NotaFiscalSaidaId == id);
        //    if (notaFiscal == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notaFiscal);

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