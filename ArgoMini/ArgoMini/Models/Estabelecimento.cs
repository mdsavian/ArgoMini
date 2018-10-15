using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArgoMini.Models
{
    public class Estabelecimento { 
    
        public int EstabelecimentoId { get; set; }
        public string Cnpj { get; set; }

        [Display(Name="Razão Social")]
        public string RazaoSocial { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        public string Logradouro { get; set; }
        [Display(Name = "Nº")]
        public string Numero { get; set; }
        public string Bairro { get; set; }
        [Display(Name = "Código Munícipio")]
        public string CodigoMunicipio { get; set; }
        [Display(Name = "Nome Munícipio")]
        public string NomeMunicipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Fone { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        [Display(Name = "Código Regime Tributário")]
        public int CodigoRegimeTributario { get; set; }
    }
}
