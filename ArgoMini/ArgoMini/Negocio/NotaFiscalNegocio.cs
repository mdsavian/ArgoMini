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


        public NotaFiscalSaida CriarNotaFiscal()
        {
            using (var contexto = new ArgoMiniContext())
            {
                var ultimaNotaFiscalSaida = contexto.NotasFiscalSaidas.ToList().LastOrDefault();

                NotaFiscalSaida nota =
                    new NotaFiscalSaida
                    {
                        NotaFiscalSaidaId = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                        Numero = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                        Itens = new List<NotaFiscalSaidaItem>(),
                        Situacao = ESituacaoNotaFiscalSaida.Construcao,
                        Serie = 113
                    };
                return nota;
            }
        }


        public void EmitirNotaFiscal(NotaFiscalSaida notaFiscal)
        {
            try
            {
                using (var contexto = new ArgoMiniContext())
                {
                    contexto.NotasFiscalSaidas.Add(notaFiscal);
                    new FlexDocsNegocio().EmitirNfe(notaFiscal, contexto);
                    MercadoriaEstoqueNegocio.AtualizarEstoqueNotaSaida(notaFiscal, contexto);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void CancelarNotaFiscal(NotaFiscalSaida notaFiscal)
        {
            using (var contexto = new ArgoMiniContext())
            {
                try
                {
                    contexto.NotasFiscalSaidas.Add(notaFiscal);
                    new FlexDocsNegocio().CancelarNfce(notaFiscal, "teste cancelamento");

                }
                catch (Exception ex)
                {

                }
            }
        }

        public void InutilizarNotaFiscal(NotaFiscalSaida notaFiscal)
        {
            using (var contexto = new ArgoMiniContext())
            {
                try
                {
                    contexto.NotasFiscalSaidas.Add(notaFiscal);
                    new FlexDocsNegocio().InutilizarNfce(notaFiscal, "teste inutlização");

                }
                catch (Exception ex)
                {

                }
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