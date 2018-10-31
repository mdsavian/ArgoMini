using System;
using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;
using ArgoMini.Negocio;

namespace ArgoMini.Controllers
{
    internal class NotaFiscalSaidaItemNegocio
    {
        public NotaFiscalSaidaItemNegocio()
        {
        }

        public NotaFiscalSaidaItem TransformaFrenteCaixa(FrenteCaixa frenteCaixa, NotaFiscalSaida notaFiscal)
        {
            using (var contexto = new ArgoMiniContext())
            {

                var notaFiscalItem = new NotaFiscalSaidaItem
                {
                    Mercadoria = contexto.Mercadorias.ToList()
                        .First(c => c.CodigoBarras == frenteCaixa.CodigoMercadoria),
                    Quantidade = frenteCaixa.Quantidade,
                    PrecoVenda = frenteCaixa.PrecoVenda,
                    TotalMercadoria = frenteCaixa.ValorTotal,
                    MercadoriaId = frenteCaixa.CodigoMercadoria,
                    NotaFiscalSaidaId = notaFiscal.NotaFiscalSaidaId
                };
                
                contexto.NotaFiscalSaidaItems.Add(notaFiscalItem);
                contexto.SaveChanges();

                return notaFiscalItem;
            }
        }

        public static void AdicionarNovoItemNota(ref NotaFiscalSaida notaFiscal, NotaFiscalSaidaItem notaFiscalItem)
        {
            try
            {
                using (var contexto = new ArgoMiniContext())
                {
                    notaFiscal.Itens.Add(notaFiscalItem);
                    notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);
                    contexto.Entry(notaFiscal).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                notaFiscal.Itens.Remove(notaFiscalItem);
            }
        }

        public static void EditarItemNota(NotaFiscalSaidaItem notaFiscalItem)
        {
            try
            {
                using (var contexto = new ArgoMiniContext())
                {
                    var notaItemBanco = contexto.NotaFiscalSaidaItems.ToList().FirstOrDefault(c =>
                        c.NotaFiscalSaidaItemId == notaFiscalItem.NotaFiscalSaidaItemId);

                    if (notaItemBanco != null)
                    {
                        notaItemBanco.PrecoVenda = notaFiscalItem.PrecoVenda;
                        notaItemBanco.Quantidade = notaFiscalItem.Quantidade;
                        notaItemBanco.TotalMercadoria = notaFiscalItem.TotalMercadoria;

                        contexto.Entry(notaItemBanco).State = EntityState.Modified;
                        contexto.SaveChanges();    
                    }
                }

            }
            catch (Exception e)
            {
            }
        }
    }
}