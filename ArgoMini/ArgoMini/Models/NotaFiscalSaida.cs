using System;
using System.Collections.Generic;
using ArgoMini.Enums;

namespace ArgoMini.Models
{
    public class NotaFiscalSaida
    {
        public int NotaFiscalSaidaId { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
        public int TipoDocumento { get; set; }
        public ESituacaoNotaFiscalSaida Situacao { get; set; }
        public int ClienteId { get; set; }
        public DateTime? DataEmissao { get; set; }
        public int VistaPrazo { get; set; }
        public string CnpjCpf { get; set; }
        public decimal ValorMercadorias { get; set; }
        public int QuantidadeItensNota { get; set; }
        public decimal ValorTotalNota { get; set; }

        public virtual List<NotaFiscalSaidaItem> Itens { get; set; }
    }

    
}
