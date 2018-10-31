namespace ArgoMini.Models
{
    public class NotaFiscalCompraItem
    {
        public int Id { get; set; }
        public int NotaFiscalCompraId { get; set; }

        public virtual NotaFiscalCompra NotaFiscalCompra { get; set; }

        public string MercadoriaId { get; set; }
        public string MercadoriaDescricao { get; set; }

        public virtual Mercadoria Mercadoria { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal TotalMercadoria { get; set; }
    }
}
