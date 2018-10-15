
using System;
using System.Collections.Generic;
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
        private List<FrenteCaixa> itensFrenteCaixa = new List<FrenteCaixa>();
        public ActionResult FrenteCaixa()
        {
            //var employees = _context.Employees.ToList();
            return View();
        }

        public Action EmitirNotaFiscal()
        {
            new NotaFiscalNegocio().EmitirNotaFiscal();
            return null;
        }

        public ActionResult AdicionarProduto()
        {
            //new NotaFiscalNegocio().EmitirNotaFiscal();
            //RedirectToAction
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