using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArgoMini.Models
{
    public class Mercadoria
    {
        public int MercadoriaId { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "NCM")]
        public string NcmId { get; set; }

        [Display(Name = "Preço Venda")]
        public decimal PrecoVenda { get; set; }

        [Display(Name = "Preço Custo")]
        public decimal PrecoCusto { get; set; }

        public decimal Quantidade { get; set; }

        [Display(Name = "Código de Barras")]
        public int CodigoBarras { get; set; }

        public int Cest { get; set; }

        public string UnidadeMedida { get; set; }
    }
}
