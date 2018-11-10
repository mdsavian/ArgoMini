using ArgoMini.Models;
using ArgoMini.WSCorreios;


namespace ArgoMini.Negocio
{
    public class DadosCorreioNegocio
    {
        public static DadosCorreio ConverDadosCorreio(enderecoERP endereco)
        {
            return new DadosCorreio
            {
                Bairro = endereco.bairro,
                Cidade = endereco.cidade,
                Complemento = endereco.complemento2,
                Uf = endereco.uf,
                Rua = endereco.end
            };
        }

        public static DadosCorreio ConsultaCepService(string cep)
        {
            cep = cep.Replace(".", string.Empty).Replace("-", string.Empty);
            try
            {
                var ws = new consultaCEPResponse();
                //var ws = new AtendeClienteClient();
                //var resposta = ws.consultaCEP(cep);
                //if (resposta != null)
                //    return ConverDadosCorreio(resposta);
                //return null;

                //var resposta = ws.consultaCEPResponse(cep);
                //do
                //{
                //    if (resposta.IsCompleted)
                //    {
                //        //if (resposta.Result.@return == null)
                //        //    return null;

                //        return DadosCorreioNegocio.ConverDadosCorreio(resposta.Result.@return);
                //    }

                //} while (!resposta.IsCompleted);

                //var ws = new WsCorreiros.AtendeClienteClient();
                //var resposta = ws.consultaCEPAsync(cep);
                //do
                //{
                //    if (resposta.IsCompletedSuccessfully)
                //    {
                //        //if (resposta.Result.@return == null)
                //        //    return null;

                //        return DadosCorreioNegocio.ConverDadosCorreio(resposta.Result.@return);
                //    }

                //} while (!resposta.IsCompletedSuccessfully);

                //if (resposta.IsCompletedSuccessfully)
                //    return DadosCorreioNegocio.ConverDadosCorreio(resposta.Result.@return);

            }
            catch (Exception Exception)
            {

            }

            return null;
        }
    }
}
