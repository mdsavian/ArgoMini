using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    public class MercadoriaNegocio
    {

        public static void AtualizaNotaCompraItem(NotaFiscalCompra notaFiscalCompra, ArgoMiniContext contexto)
        {

            foreach (var notaFiscalCompraItem in notaFiscalCompra.Itens)
            {
                decimal.TryParse(notaFiscalCompraItem.CodigoMercadoriaImportada, out var codigoDecimal);

                var mercadoria = contexto.Mercadorias.FirstOrDefault(c => c.CodigoBarras == codigoDecimal);

                if (mercadoria == null)
                {
                    mercadoria = new Mercadoria
                    {
                        CodigoBarras = codigoDecimal,
                        Cest = notaFiscalCompraItem.Cest,
                        NcmId = notaFiscalCompraItem.Ncm,
                        Descricao = notaFiscalCompraItem.DescricaoMercadoriaImportada,
                        PrecoCusto = notaFiscalCompraItem.PrecoCusto,
                        Quantidade = notaFiscalCompraItem.Quantidade
                    };
                    contexto.Mercadorias.Add(mercadoria);
                    contexto.SaveChanges();
                }
                else
                {
                    mercadoria.Quantidade += notaFiscalCompraItem.Quantidade;
                    mercadoria.PrecoCusto = notaFiscalCompraItem.PrecoCusto;
                    mercadoria.Cest = notaFiscalCompraItem.Cest;
                    mercadoria.NcmId = notaFiscalCompraItem.Ncm;

                    contexto.Entry(mercadoria).State = EntityState.Modified;
                    contexto.SaveChanges();
                }

                notaFiscalCompraItem.Mercadoria = mercadoria;
                notaFiscalCompraItem.MercadoriaId = mercadoria.MercadoriaId;

                MercadoriaEstoqueNegocio.AtualizarEstoqueMercadoria(mercadoria, contexto);


            }

        }
    }
}