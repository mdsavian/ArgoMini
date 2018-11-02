using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;
using ArgoMini.Negocio;
using File = System.IO.File;

namespace ArgoMini.Controllers
{
    public class NotaFiscalCompraController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();

        public ActionResult ImportarNotaFiscalCompraView()
        {
            NotaFiscalCompra notaCompra = new NotaFiscalCompra();
            return View(notaCompra);
        }

        [HttpPost]
        public ActionResult ImportarNotaFiscalCompraView(NotaFiscalCompra notaCompraTela)
        {
            


            if (!string.IsNullOrEmpty(notaCompraTela.Chave))
            {
                var notaNova = NotaFiscalCompraNegocio.ConsultarNotaCompra(notaCompraTela.Chave);

                if (notaNova != null)
                {
                    TempData["notaCompra"] = notaNova;
                    TempData.Keep("notaCompra");

                    return RedirectToAction("NotaFiscalCompraDetalhe", "NotaFiscalCompra");
                }
            }

            return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");
        }

        public ActionResult TesteView()
        {
            FileUpload testex = new FileUpload();
           
            return View(testex);
        }

        [HttpPost]
        public ActionResult TesteView(FileUpload id)
        {
            var aa = Request.Files;


            var fileName = "";
            var fileSavePath = "";
            var uploadedFile = Request.Files[0];
            fileName = Path.GetFileName(uploadedFile.FileName);
            fileSavePath = Server.MapPath("~/App_Data/UploadedFiles/" +
                                          fileName);
            uploadedFile.SaveAs(fileSavePath);

            var result = "";
            Array userData = null;
            char[] delimiterChar = { ',' };

            if (System.IO.File.Exists(fileSavePath))
            {
                userData = System.IO.File.ReadAllLines(fileSavePath);
                if (userData == null)
                {
                    // Empty file.
                    result = "The file is empty.";
                }
            }
            else
            {
                // File does not exist.
                result = "The file does not exist.";
            }


            return View();
        }

        public ActionResult NotaFiscalCompraDetalhe()
        {
            var notaCompra = (NotaFiscalCompra)TempData.Peek("notaCompra");

            if (notaCompra == null)
                return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");

            return View(notaCompra);
        }

        [HttpPost]
        public ActionResult NotaFiscalCompraDetalhe(NotaFiscalCompra notaFiscalCompra)
        {
            if (notaFiscalCompra == null)
                return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");

            NotaFiscalCompraNegocio.SalvarNotaCompra(notaFiscalCompra);

            return RedirectToAction("ImportarNotaFiscalCompraView", "NotaFiscalCompra");
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