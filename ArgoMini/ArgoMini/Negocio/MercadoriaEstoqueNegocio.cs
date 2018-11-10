using System;
using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    class MercadoriaEstoqueNegocio
    {
        public static void AtualizarEstoqueMercadoria(Mercadoria mercadoria, ArgoMiniContext contexto = null)
        {
            var mercadoriaEstoque =
                contexto.MercadoriaEstoque.FirstOrDefault(c => c.MercadoriaId == mercadoria.MercadoriaId);

            if (mercadoriaEstoque != null)
            {
                mercadoriaEstoque.EstoqueAtual = mercadoria.Quantidade;
            }
            else
            {
                mercadoriaEstoque = new MercadoriaEstoque
                {
                    Mercadoria = mercadoria,
                    MercadoriaId = mercadoria.MercadoriaId,
                    EstoqueAtual = mercadoria.Quantidade
                };

                contexto.MercadoriaEstoque.Add(mercadoriaEstoque);
                contexto.SaveChanges();
            }
        }

        public static void AtualizarEstoqueNotaSaida(NotaFiscalSaida notaFiscal, ArgoMiniContext context)
        {
            using (var contexto = new ArgoMiniContext())
            {
                foreach (var item in notaFiscal.Itens)
                {
                    var mercadoriaEstoque =
                        contexto.MercadoriaEstoque.FirstOrDefault(c => c.MercadoriaId == item.MercadoriaId);

                    if (mercadoriaEstoque != null)
                    {
                        mercadoriaEstoque.EstoqueAtual -= item.Quantidade;

                        contexto.Entry(mercadoriaEstoque).State = EntityState.Modified;
                        contexto.SaveChanges();
                    }

                    //else if (item.Mercadoria == null)
                    //{
                    //    var mercadoria = contexto.Mercadorias.FirstOrDefault(c => c.MercadoriaId == item.MercadoriaId);
                    //    if (mercadoria != null)
                    //    {
                    //        mercadoria.Quantidade -= item.Quantidade;
                    //        AtualizarEstoqueMercadoria(mercadoria, contexto);
                    //    }

                    //    return;
                    //}
                }
            }
        }

        public static void CancelamentoNotaFiscalSaida(NotaFiscalSaida notaSaida, ArgoMiniContext contexto)
        {
            if (notaSaida?.Itens != null)
            {
                foreach (var item in notaSaida.Itens)
                {
                    var mercadoriaEstoque =
                        contexto.MercadoriaEstoque.FirstOrDefault(c => c.MercadoriaId == item.MercadoriaId);

                    if (mercadoriaEstoque != null)
                    {
                        mercadoriaEstoque.EstoqueAtual += item.Quantidade;
                    }
                    //else
                    //{
                    //    if (item.Mercadoria == null)
                    //    {
                    //        var mercadoria =
                    //            contexto.Mercadorias.FirstOrDefault(c => c.MercadoriaId == item.MercadoriaId);
                    //        if (mercadoria != null)
                    //            AtualizarEstoqueMercadoria(mercadoria, contexto);
                    //        return;
                    //    }

                    //    AtualizarEstoqueMercadoria(item.Mercadoria, contexto);

                    //}
                }
            }
        }
    }
}