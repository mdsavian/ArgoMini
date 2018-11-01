using System;

namespace ArgoMini.Models
{
    public class Impressora
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string SerialHd { get; set; }
        public bool Imprimindo { get; set; }
        public string Nome { get;set; }
        public DateTime? DataHoraUltimaImpressao { get; set; }
    }
}
