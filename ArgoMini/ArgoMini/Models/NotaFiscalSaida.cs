using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArgoMini.Enums;
using ArgoMini.Negocio.Utilitarios;

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


        //[ValidacaoAtributoCPF(ErrorMessage = "opaaa")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-###-###.##}")]
        public string CnpjCpf { get; set; }

        public decimal ValorMercadorias { get; set; }

        public int QuantidadeItensNota { get; set; }

        [Display(Name = "Valor Total Nota")]
        public decimal ValorTotalNota { get; set; }

        public string ChaveNota { get; set; }

        public string AutorizacaoNumeroProtocolo { get; set; }

        public DateTime? AutorizacaoDataHoraProtocolo { get; set; }

        public virtual List<NotaFiscalSaidaItem> Itens { get; set; }

        public string CancelamentoNumeroProtocolo { get; set; }
        
        public DateTime? CancelamentoDataHoraProtocolo { get; set; }
        
        public string InutilizacaoNumeroProtocolo { get; set; }
        
        public DateTime? InutilizacaoDataHoraProtocolo { get; set; }

        public string DenegacaoNumeroProtocolo { get; set; }

        public DateTime? DenegacaoDataHoraProtocolo { get; set; }

        public string XmlCancelado { get; set; }

        public bool XmlAutorizadoCompactado { get; set; }

        public string XmlAutorizadoPersistido { get; set; }

    }

    
}

