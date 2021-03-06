﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArgoMini.Models.NaoPersistidos
{
    public class FrenteCaixa
    {
        public int FrenteCaixaId { get; set; }
        public List<Mercadoria> MercadoriasFrenteCaixa { get; set; }

        [Required]
        [Display(Name = "Código Mercadoria")]
        public int CodigoMercadoria { get; set; }
        [Required]
        [Display(Name = "Preço Venda")]
        public decimal PrecoVenda { get; set; }
        [Required]
        [Display(Name = "Quantidade")]
        public decimal Quantidade { get; set; }
        [Required]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Valor Total Documento")]
        public decimal ValorTotalNota { get; set; }

        public int NotaFiscalId { get; set; }


        public FrenteCaixa()
        {
            MercadoriasFrenteCaixa = new List<Mercadoria>();
        }
    }
}