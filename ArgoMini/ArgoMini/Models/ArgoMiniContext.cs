using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArgoMini.Models
{
    public class ArgoMiniContext : DbContext
    {
        public ArgoMiniContext() : base("ArgoMiniContext")
        {
        }

        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<NotaFiscalSaida> NotasFiscalSaida { get; set; }
        public DbSet<NotaFiscalSaidaItem> NotaFiscalSaidaItems { get; set; }
        public DbSet<Mercadoria> Mercadorias { get; set; }
        public DbSet<Impressora> Impressoras { get; set; }
        public DbSet<NotaFiscalCompra> NotaFiscalCompra { get; set; }
        public DbSet<NotaFiscalCompraItem> NotaFiscalCompraItem { get; set; }
        public DbSet<MercadoriaEstoque> MercadoriaEstoque { get; set; }
    }
}