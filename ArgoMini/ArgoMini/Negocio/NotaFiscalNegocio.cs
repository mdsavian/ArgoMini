using System;
using System.Collections.Generic;
using System.Linq;
using ArgoMini.Enums;
using ArgoMini.Models;

namespace ArgoMini.Negocio
{
    public class NotaFiscalNegocio
    {
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();

        public NotaFiscalSaida CriarNotaFiscal()
        {
            var ultimaNotaFiscalSaida = _contexto.NotasFiscalSaida.ToList().LastOrDefault();

            NotaFiscalSaida nota =
                new NotaFiscalSaida
                {
                    NotaFiscalSaidaId = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                    Numero = ultimaNotaFiscalSaida?.NotaFiscalSaidaId + 1 ?? 1,
                    Itens = new List<NotaFiscalSaidaItem>(),
                    Situacao = ESituacaoNotaFiscalSaida.Construcao,
                    Serie = 113
                };


            _contexto.NotasFiscalSaida.Add(nota);
            _contexto.SaveChanges();

            return nota;
        }


        public void EmitirNotaFiscal(NotaFiscalSaida notaFiscal)
        {
            try
            {
                new FlexDocsNegocio().EmitirNfe(notaFiscal);
            }
            catch (Exception)
            {
            }
        }
    }
}