using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ArgoMini.Models;

namespace ArgoMini.Controllers
{
    public class MercadoriaController : Controller
    {
        private readonly ArgoMiniContext _context = new ArgoMiniContext();
        
        public ActionResult Index()
        {
            var mercadorias = _context.Mercadorias.ToList();
            return View(mercadorias);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Mercadoria mercadoria)
        {
            if (ModelState.IsValid)
            {
                _context.Mercadorias.Add(mercadoria);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mercadoria);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _context.Mercadorias.SingleOrDefault(e => e.MercadoriaId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Mercadoria mercadoria)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(mercadoria).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mercadoria);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mercadoria = _context.Mercadorias.SingleOrDefault(e => e.MercadoriaId == id);
            if (mercadoria == null)
            {
                return HttpNotFound();
            }
            return View(mercadoria);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var mercadoria = _context.Mercadorias.SingleOrDefault(e => e.MercadoriaId == id);
            if (mercadoria == null)
            {
                return HttpNotFound();
            }
            return View(mercadoria);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var mercadoria = _context.Mercadorias.SingleOrDefault(x => x.MercadoriaId == id);
            _context.Mercadorias.Remove(mercadoria ?? throw new InvalidOperationException());
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