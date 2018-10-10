using System;
using System.Collections.Generic;

namespace ArgoMini.Models
{
    public class Mercadoria
    {
        public int MercadoriaId { get; set; }
        public string Descricao { get; set; }
        public string NcmId { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal Quantidade { get; set; }
        public int CodigoBarras { get; set; }
    }
}
