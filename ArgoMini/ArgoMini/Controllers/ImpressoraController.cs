using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;
using ArgoMini.Negocio;

namespace ArgoMini.Controllers
{
    public class ImpressoraController : Controller
    {
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();
        private readonly ImpressoraNegocio _impressoraNegocio = new ImpressoraNegocio();

        public ActionResult Impressora()
        {
            return View(_contexto.Impressoras.ToList());
        }

        public ActionResult Create()
        {
            var impressorasConectadas = new ImpressorasConectadas();
            impressorasConectadas.Impressoras = _impressoraNegocio.BuscarImpressoras();

            return View(impressorasConectadas);
        }

        [HttpPost]
        public ActionResult Create(ImpressorasConectadas impressoraConectada)
        {
            string serialHd = _impressoraNegocio.BuscarSerialDiscoLocalC();
            if (ModelState.IsValid && !_impressoraNegocio.ExisteImpressoraSerial(serialHd))
            {
                impressoraConectada.Impressora.SerialHd = serialHd;
                _contexto.Impressoras.Add(impressoraConectada.Impressora);
                _contexto.SaveChanges();

                return RedirectToAction("Impressora");
            }

            if (impressoraConectada.Impressoras == null)
                impressoraConectada.Impressoras = _impressoraNegocio.BuscarImpressoras();
            return View(impressoraConectada);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var impressora = _contexto.Impressoras.SingleOrDefault(e => e.Id == id);
            if (impressora == null)
            {
                return HttpNotFound();
            }
            return View(impressora);
        }

        [HttpPost]
        public ActionResult Edit(Impressora impressora)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(impressora).State = EntityState.Modified;
                _contexto.SaveChanges();

                return RedirectToAction("Impressora");
            }

            return View(impressora);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var impressora = _contexto.Impressoras.SingleOrDefault(e => e.Id == id);
            if (impressora == null)
            {
                return HttpNotFound();
            }
            return View(impressora);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var impressora = _contexto.Impressoras.SingleOrDefault(x => x.Id == id);
            _contexto.Impressoras.Remove(impressora ?? throw new InvalidOperationException());
            _contexto.SaveChanges();
            return RedirectToAction("Impressora");
        }

        public ActionResult TestaConexao()
        {
            var impressoraNegocio = new ImpressoraNegocio();
            impressoraNegocio.TestarConexao();
            return RedirectToAction("Impressora");
        }
    }
}