namespace ArgoMini.Models
{
    public class MercadoriaEstoque
    {
        public int Id { get; set; }
        public int MercadoriaId { get; set; }
        public virtual Mercadoria Mercadoria { get; set; }
        public decimal EstoqueAtual { get; set; }
    }
}
