namespace ArgoMini.Models
{
    public class NotaFiscalCompraItem
    {
        public int Id { get; set; }
        public int NotaFiscalCompraId { get; set; }

        public virtual NotaFiscalCompra NotaFiscalCompra { get; set; }
        public int MercadoriaId { get; set; }
        public virtual Mercadoria Mercadoria { get; set; }

        public string CodigoMercadoriaImportada { get; set; }
        public decimal CodigoBarrasMercadoriaImportada { get; set; }
        public string DescricaoMercadoriaImportada { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal TotalMercadoria { get; set; }

        public string Cest { get; set; }
        public string Ncm { get; set; }

    }
}
