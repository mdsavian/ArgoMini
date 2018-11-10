using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgoMini.Models.NaoPersistidos
{
    public class ReciboNfe
    {
        public string Chave { get; set; }
        public DateTime DataHora { get; set; }
        public string Protocolo { get; set; }
        public string Justificativa { get; set; }
    }
}