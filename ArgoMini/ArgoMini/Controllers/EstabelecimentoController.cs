using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ArgoMini.Models;
using ArgoMini.Negocio;

namespace ArgoMini.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();
        
        public ActionResult Index()
        {
            var estabelecimentos = _context.Estabelecimentos.ToList();
            if (estabelecimentos.FirstOrDefault() != null)
                return RedirectToAction("Edit", estabelecimentos.First().EstabelecimentoId);

            return RedirectToAction("Create");
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
                var xx = DadosCorreioNegocio.ConsultaCepService(estabelecimento.Cep);
                _context.Estabelecimentos.Add(estabelecimento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estabelecimento);
        }

        public ActionResult Edit(int? id)
        {
            var estabelecimentos = _context.Estabelecimentos.ToList();

            if (id == null)
            {
                id = estabelecimentos.First().EstabelecimentoId;
            }

            var estabelecimento = _context.Estabelecimentos.SingleOrDefault(e => e.EstabelecimentoId == id);
            if (estabelecimento == null)
            {
                return HttpNotFound();
            }
            return View(estabelecimento);
        }

        [HttpPost]
        public ActionResult Edit(Estabelecimento estabelecimento)
        {
            if (ModelState.IsValid)
            {
                var xx = DadosCorreioNegocio.ConsultaCepService(estabelecimento.Cep);
                _context.Entry(estabelecimento).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estabelecimento);
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