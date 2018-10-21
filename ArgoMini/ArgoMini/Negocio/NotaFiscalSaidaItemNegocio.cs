using System.Linq;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;

namespace ArgoMini.Controllers
{
    internal class NotaFiscalSaidaItemNegocio
    {
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();

        public NotaFiscalSaidaItemNegocio()
        {
        }

        public NotaFiscalSaidaItem TransformaFrenteCaixa(FrenteCaixa frenteCaixa, NotaFiscalSaida notaFiscal)
        {
            var notaFiscalItem = new NotaFiscalSaidaItem
            {
                Mercadoria = _contexto.Mercadorias.ToList().First(c => c.CodigoBarras == frenteCaixa.CodigoMercadoria),
                Quantidade = frenteCaixa.Quantidade,
                PrecoVenda = frenteCaixa.PrecoVenda,
                TotalMercadoria = frenteCaixa.ValorTotal,
                MercadoriaId = frenteCaixa.CodigoMercadoria,
                NotaFiscalSaidaId = notaFiscal.NotaFiscalSaidaId
            };


            _contexto.NotaFiscalSaidaItems.Add(notaFiscalItem);
            _contexto.SaveChanges();
            return notaFiscalItem;
        }
    }
}