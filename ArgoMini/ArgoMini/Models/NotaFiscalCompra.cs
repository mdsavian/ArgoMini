using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArgoMini.Enums;

namespace ArgoMini.Models
{
    public class NotaFiscalCompra
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Chave { get; set; }
        public int Serie { get; set; }
        public int FornecedorId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Cnpj{ get; set; }
        public string NomeFornecedor { get; set; }

        public decimal ValorTotalNota { get; set; }

        public virtual List<NotaFiscalCompraItem> Itens { get; set; }
    }

    
}
