using System.Linq;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    class MercadoriaEstoqueNegocio
    {
        public static void AtualizarEstoqueMercadoria(Mercadoria mercadoria)
        {
            using (var contexto = new ArgoMiniContext())
            {
                var mercadoriaEstoque = contexto.MercadoriaEstoque.FirstOrDefault(c => c.MercadoriaId == mercadoria.MercadoriaId);

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
        }

        public static void AtualizarEstoqueNotaSaida(NotaFiscalSaida notaFiscal)
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
                    }
                    else
                    {
                        if (item.Mercadoria == null)
                        {
                            var mercadoria = contexto.Mercadorias.FirstOrDefault(c=> c.MercadoriaId == item.MercadoriaId);
                            if(mercadoria != null)
                                AtualizarEstoqueMercadoria(mercadoria);
                            return;
                        }

                        AtualizarEstoqueMercadoria(item.Mercadoria);

                    }
                }
            }
        }
    }
}