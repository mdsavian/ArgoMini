using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ArgoMini.Models;
using Canducci.Zip;


namespace ArgoMini.Negocio
{
    public class DadosCorreioNegocio
    {
        public static DadosCorreio ConsultaCep(string cep)
        {
            cep = Regex.Replace(cep, @"[^\d]", "");

            try
            {
                using (ZipCodeLoad zipLoad = new ZipCodeLoad())
                {
                    if (ZipCode.TryParse(cep, out var zipCode))
                    {
                        var result = Task.Run(() => zipLoad.FindAsync(zipCode)).Result;

                        if (result.IsValid)
                        {
                            if (!string.IsNullOrWhiteSpace(result.Value.Uf))
                            {
                                var dados = new DadosCorreio
                                {
                                    Bairro = result.Value.District.ToUpper(),
                                    Cep = result.Value.Zip,
                                    Rua = result.Value.Address.ToUpper(),
                                    CodigoMunicipio = result.Value.Ibge,
                                    Cidade = result.Value.City.ToUpper(),
                                    Uf = result.Value.Uf.ToUpper()
                                };

                                return dados;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
