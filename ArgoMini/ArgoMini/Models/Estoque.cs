namespace ArgoMini.Models
{
    public class Estoque
    {
        public int NotaFiscalSaidaItemId { get; set; }
        public int NotaFiscalSaidaId { get; set; }

        public virtual NotaFiscalSaida NotaFiscalSaida { get; set; }

        public int MercadoriaId { get; set; }

        public virtual Mercadoria Mercadoria { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal TotalMercadoria { get; set; }
    }
}
