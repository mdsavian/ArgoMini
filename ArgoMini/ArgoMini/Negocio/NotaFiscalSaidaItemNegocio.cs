using System;
using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;

namespace ArgoMini.Negocio
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
                var notaFiscalItem = notaFiscal.Itens.FirstOrDefault(c =>
                    c.Mercadoria.CodigoBarras == frenteCaixa.CodigoMercadoria &&
                    c.NotaFiscalSaidaId == notaFiscal.NotaFiscalSaidaId);

                if (notaFiscalItem == null)
                {
                    var id = notaFiscal.Itens.LastOrDefault()?.NotaFiscalSaidaItemId ?? 0;
                    id = id+1;
                    var mercadoria = contexto.Mercadorias.ToList()
                        .First(c => c.CodigoBarras == frenteCaixa.CodigoMercadoria);
                    notaFiscalItem = new NotaFiscalSaidaItem
                    {
                        NotaFiscalSaidaItemId = id,
                        Mercadoria = mercadoria,
                        Quantidade = frenteCaixa.Quantidade,
                        PrecoVenda = frenteCaixa.PrecoVenda,
                        TotalMercadoria = frenteCaixa.Quantidade * frenteCaixa.PrecoVenda,
                        MercadoriaId = mercadoria.MercadoriaId,
                        NotaFiscalSaidaId = notaFiscal.NotaFiscalSaidaId
                    };

                    //contexto.NotaFiscalSaidaItems.Add(notaFiscalItem);
                    //contexto.SaveChanges();
                }
                else
                {
                    notaFiscalItem.PrecoVenda = frenteCaixa.PrecoVenda;
                    notaFiscalItem.Quantidade += frenteCaixa.Quantidade;
                    notaFiscalItem.TotalMercadoria = frenteCaixa.PrecoVenda * notaFiscalItem.Quantidade;
                    notaFiscal.Itens.Remove(notaFiscalItem);
                }
                
                return notaFiscalItem;
            }
        }


        //public static void AdicionarNovoItemNota(ref NotaFiscalSaida notaFiscal, NotaFiscalSaidaItem notaFiscalItem)
        //{
        //    try
        //    {
        //        using (var contexto = new ArgoMiniContext())
        //        {
                    
        //            //contexto.Entry(notaFiscal).State = EntityState.Modified;
        //            //contexto.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        notaFiscal.Itens.Remove(notaFiscalItem);
        //    }
        //}

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

                        //contexto.Entry(notaItemBanco).State = EntityState.Modified;
                        //contexto.SaveChanges();    
                    }
                }

            }
            catch (Exception e)
            {
            }
        }
    }
}