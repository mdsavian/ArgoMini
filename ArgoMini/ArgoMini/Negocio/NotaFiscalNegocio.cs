using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ArgoMini.Enums;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    internal class NotaFiscalNegocio
    {
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();

        public NotaFiscalSaida CriarNotaFiscal()
        {
            var ultimaNotaFiscalSaida = _contexto.NotasFiscalSaidas.ToList().LastOrDefault();

            NotaFiscalSaida nota =
                new NotaFiscalSaida
                {
                    NotaFiscalSaidaId = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                    Numero = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                    Itens = new List<NotaFiscalSaidaItem>(),
                    Situacao = ESituacaoNotaFiscalSaida.Construcao,
                    Serie = 113
                };


            _contexto.NotasFiscalSaidas.Add(nota);

            return nota;
        }


        public void EmitirNotaFiscal(NotaFiscalSaida notaFiscal)
        {
            try
            {
                _contexto.SaveChanges();
                new FlexDocsNegocio().EmitirNfe(notaFiscal);
                MercadoriaEstoqueNegocio.AtualizarEstoqueNotaSaida(notaFiscal);
                
            }
            catch (Exception)
            {

            }
        }

        public static NotaFiscalSaida AtualizarValorNota(int notaFiscalId)
        {
            using (var contexto = new ArgoMiniContext())
            {
                var notaFiscal = contexto.NotasFiscalSaidas.Include(c => c.Itens).Include(c=> c.Itens.Select(d=> d.Mercadoria))
                    .First(c => c.NotaFiscalSaidaId == notaFiscalId);

                notaFiscal.ValorTotalNota = notaFiscal.Itens.Sum(c => c.TotalMercadoria);

                contexto.Entry(notaFiscal).State = EntityState.Modified;
                contexto.SaveChanges();

                return notaFiscal;  
            }

        }

        public void DeletaNota(NotaFiscalSaida notaFiscal)
        {
            using (var contexto = new ArgoMiniContext())
            {
                contexto.NotasFiscalSaidas.Remove(notaFiscal);

                if (notaFiscal.Itens == null)
                {
                    notaFiscal.Itens = contexto.NotaFiscalSaidaItems.Where(c => c.NotaFiscalSaidaId == notaFiscal.NotaFiscalSaidaId).ToList();
                }

                contexto.NotaFiscalSaidaItems.RemoveRange(notaFiscal.Itens);
            }
        }
    }
}