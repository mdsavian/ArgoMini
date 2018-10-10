using System;
using System.Collections.Generic;

namespace ArgoMini.Models
{
    public partial class Pessoa
    {
        public int PessoaId { get; set; }
        public int TipoPessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CnpjCpf { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Bairro      { get; set; }
        public string Numero      { get; set; }
        public string Complemento { get; set; }
        public string Endereco    { get; set; }
        public string Cep         { get; set; }
        public string Estado      { get; set; }
        public string Cidade      { get; set; }
    }
}
