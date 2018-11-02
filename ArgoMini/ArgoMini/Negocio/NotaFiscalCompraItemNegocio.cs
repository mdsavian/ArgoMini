using System;
using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;
using ArgoMini.Negocio;

namespace ArgoMini.Negocio
{
    internal class NotaFiscalCompraItemNegocio
    {
        public NotaFiscalCompraItemNegocio()
        {
        }

      
        public static void AdicionarNovoItemNota(ref NotaFiscalSaida notaFiscal, NotaFiscalSaidaItem notaFiscalItem)
        {
            //try
            //{
            //    using (var contexto = new ArgoMiniContext())
            //    {
            //        notaFiscal.Itens.Add(notaFiscalItem);
            //        notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);
            //        contexto.Entry(notaFiscal).State = EntityState.Modified;
            //        contexto.SaveChanges();
            //    }
            //}
            //catch (Exception e)
            //{
            //    notaFiscal.Itens.Remove(notaFiscalItem);
            //}
        }

        public static void EditarItemNota(NotaFiscalCompraItem notaFiscalCompraItem)
        {
            //try
            //{
            //    using (var contexto = new ArgoMiniContext())
            //    {
            //        var notaItemBanco = contexto.NotaFiscalCompraItem.ToList().FirstOrDefault(c =>
            //            c.NotaFiscalSaidaItemId == notaFiscalItem.NotaFiscalSaidaItemId);

            //        if (notaItemBanco != null)
            //        {
            //            notaItemBanco.PrecoVenda = notaFiscalItem.PrecoVenda;
            //            notaItemBanco.Quantidade = notaFiscalItem.Quantidade;
            //            notaItemBanco.TotalMercadoria = notaFiscalItem.TotalMercadoria;

            //            contexto.Entry(notaItemBanco).State = EntityState.Modified;
            //            contexto.SaveChanges();    
            //        }
            //    }

            //}
            //catch (Exception e)
            //{
            //}
        }
    }
}