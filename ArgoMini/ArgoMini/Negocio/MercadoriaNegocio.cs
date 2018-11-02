using System.Data.Entity;
using System.Linq;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    public class MercadoriaNegocio
    {

        public static void AtualizaNotaCompraItem(NotaFiscalCompraItem notaFiscalCompraItem)
        {
            using (var contexto = new ArgoMiniContext())
            {
                decimal.TryParse(notaFiscalCompraItem.CodigoMercadoriaImportada, out var codigoDecimal);

                var mercadoria = contexto.Mercadorias.FirstOrDefault(c => c.CodigoBarras == codigoDecimal);

                if (mercadoria == null)
                {
                    mercadoria = new Mercadoria
                    {
                        CodigoBarras = notaFiscalCompraItem.CodigoBarrasMercadoriaImportada,
                        Cest = notaFiscalCompraItem.Cest,
                        NcmId = notaFiscalCompraItem.Ncm,
                        Descricao = notaFiscalCompraItem.DescricaoMercadoriaImportada,
                        PrecoCusto = notaFiscalCompraItem.PrecoCusto,
                        Quantidade = notaFiscalCompraItem.Quantidade
                    };

                    contexto.Entry(mercadoria).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
                else
                {
                    mercadoria.Quantidade += notaFiscalCompraItem.Quantidade;
                    mercadoria.PrecoCusto = notaFiscalCompraItem.PrecoCusto;
                    mercadoria.Cest = notaFiscalCompraItem.Cest;
                    mercadoria.NcmId = notaFiscalCompraItem.Ncm;
                }

                MercadoriaEstoqueNegocio.AtualizarEstoqueMercadoria(mercadoria);

            }

        }
    }
}