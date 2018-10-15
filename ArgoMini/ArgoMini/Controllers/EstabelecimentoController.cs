using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ArgoMini.Models;

namespace ArgoMini.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();
        
        public ActionResult Index()
        {
            var estabelecimentos = _context.Estabelecimentos.ToList();
            return View(estabelecimentos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                _context.Estabelecimentos.Add(estabelecimento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estabelecimento);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _context.Estabelecimentos.SingleOrDefault(e => e.EstabelecimentoId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(estabelecimento).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estabelecimento);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estabelecimento = _context.Estabelecimentos.SingleOrDefault(e => e.EstabelecimentoId == id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estabelecimento = _context.Estabelecimentos.SingleOrDefault(e => e.EstabelecimentoId == id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var estabelecimento = _context.Estabelecimentos.SingleOrDefault(x => x.EstabelecimentoId == id);
            _context.Estabelecimentos.Remove(estabelecimento ?? throw new InvalidOperationException());
            _context.SaveChanges();
            return RedirectToAction("Index");
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