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

        public Action EmitirNotaFiscal()
        {
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            new NotaFiscalNegocio().EmitirNotaFiscal(notaFiscal);
            return null;
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
            var notaFiscal = (NotaFiscalSaida)TempData.Peek("NotaFiscalSaida");
            //var aa = _context.Mercadorias.ToList().First(c => c.MercadoriaId == frenteCaixa.CodigoMercadoria);
            //try
            var notaFiscalItem = new NotaFiscalSaidaItemNegocio().TransformaFrenteCaixa(frenteCaixa, notaFiscal);

            
            if (notaFiscal != null)
            {
                notaFiscal.Itens.Add(notaFiscalItem);

                _context.Entry(notaFiscal).State = EntityState.Modified;
                _context.SaveChanges();
            }

            TempData["NotaFiscalSaida"] = notaFiscal;
            TempData.Keep();

            return RedirectToAction("FrenteCaixa");
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Employees.Add(employee);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //[HttpPost]
        //public ActionResult Edit(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(employee).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(employee);
        //}

        //public ActionResult Detail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var employee = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
        //    _context.Employees.Remove(employee ?? throw new InvalidOperationException());
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
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