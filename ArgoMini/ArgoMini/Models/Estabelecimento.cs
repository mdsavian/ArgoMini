using System;
using System.Collections.Generic;

namespace ArgoMini.Models
{
    public class Estabelecimento { 
    
        public int EstabelecimentoId { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CodigoMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Fone { get; set; }
        public string InscricaoEstadual { get; set; }
        public int CodigoRegimeTributario { get; set; }
    }
}
