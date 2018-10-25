using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ArgoMini.Negocio
{
    public class NotaFiscalCompraNegocio
    {
        public static void ImportarXml()
        {

        }

        public static void ConsultaNotaCompraPorCodigoBarra(string codigoBarra)
        {







        }

        private string Consultar(long unidadeNegocioId, ETipoDocumentoSaida tipoDocumento)
        {
            try
            {
                var config = App.ConfiguracaoDocumentoEletronico.First(k => k.Key == unidadeNegocioId).Value;
                var bytesCertificado = Convert.FromBase64String(config.CertificadoDigitalIntegracaoStringBase64);
                var senha = config.SenhaCertificadoDigitalIntegracao;
                var certificado = new X509Certificate2(bytesCertificado, senha);
                var opcoesAvancadas = new OpcoesAvancadasNegocio().CarregarPorUnidadeDeNegocio(config.UnidadeDeNegocioId);

                var nsu = tipoDocumento == ETipoDocumentoSaida.NotaFiscalEletronica
                    ? opcoesAvancadas.IntegracaoXmlUltimoNsuNotaEletronica ?? 0
                    : opcoesAvancadas.IntegracaoXmlUltimoNsuCupomEletronico ?? 0;

                var requisicao = new ParseNfeIntegracao().MontarRequisicao(config, tipoDocumento, nsu);
                Log.Write.Info($"Iniciando requisição para a unidade de negócio {unidadeNegocioId}.");
                Log.Write.Info($"Certificado: {certificado.SubjectName.Name}");
                Log.Write.Info($"Requisição:{Environment.NewLine}{requisicao}");
                var retorno = Binding(config.EnderecoWebserviceNfeIntegracao, certificado).nfeIntegracaoContab(XElement.Parse(requisicao));
                Log.Write.Info($"Retorno:{Environment.NewLine}{retorno}");

                var xml = new XmlDocument();
                xml.LoadXml(retorno.ToString());

                var cstat = xml.GetElementsByTagName("cStat");
                if (cstat.Count > 0 && cstat[0].InnerText.Equals("118"))
                {
                    var gZip = xml.GetElementsByTagName("loteDistComp");
                    if (gZip.Count > 0)
                    {
                        return CustomControls.Compactacao.SevenZip.DescomprimirArquivoTextoGzip(gZip[0].InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Write.Error(ex.MensagemErroParaLog());
            }

            return string.Empty;
        }


        private NfeIntegracaoSoap.NFeIntegracaoSoapClient Binding(string enderecoWebserviceNfeIntegracao,
            X509Certificate2 certificado)
        {
            var ws = new NfeIntegracaoSoap.NFeIntegracaoSoapClient(
                CriarBasicHttsSoapSefazBinding(), new EndpointAddress(enderecoWebserviceNfeIntegracao));

            if (ws.ClientCredentials != null)
                ws.ClientCredentials.ClientCertificate.Certificate = certificado;

            return ws;
        }



        public CustomBinding CriarBasicHttsSoapSefazBinding()
        {
            var securityElement = new TextMessageEncodingBindingElement
            {
                MaxReadPoolSize = 64,
                MaxWritePoolSize = 16,
                MessageVersion = MessageVersion.Soap12,
                WriteEncoding = Encoding.UTF8,
                ReaderQuotas =
                {
                    MaxDepth = 32,
                    MaxStringContentLength = 8192,
                    MaxArrayLength = 16384,
                    MaxBytesPerRead = 4096,
                    MaxNameTableCharCount = 16384
                }
            };

            var httpsTransport = new HttpsTransportBindingElement
            {
                ManualAddressing = false,
                MaxBufferPoolSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                AllowCookies = false,
                AuthenticationScheme = AuthenticationSchemes.Digest,
                BypassProxyOnLocal = false,
                DecompressionEnabled = true,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                KeepAliveEnabled = true,
                MaxBufferSize = int.MaxValue,
                ProxyAuthenticationScheme = AuthenticationSchemes.Anonymous,
                Realm = "",
                TransferMode = TransferMode.Buffered,
                UnsafeConnectionNtlmAuthentication = false,
                UseDefaultWebProxy = true,
                RequireClientCertificate = true
            };

            return new CustomBinding(securityElement, httpsTransport);
        }
    }
}