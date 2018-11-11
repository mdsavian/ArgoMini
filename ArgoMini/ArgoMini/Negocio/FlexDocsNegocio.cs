using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using ArgoMini.Enums;
using ArgoMini.Models;
using ArgoMini.Models.NaoPersistidos;

namespace ArgoMini.Negocio
{

    public class FlexDocsNegocio
    {
        private readonly NFe_Util_2G.Util _nfeUtil = new NFe_Util_2G.Util();
        private string _cNf = string.Empty;
        private string _cDv = string.Empty;
        private string _url = string.Empty;


        private readonly string _senha = string.Empty;
        private readonly string _funcionario = string.Empty;
        private readonly string _proxy = string.Empty;
        private static string _siglaWs = "rs";

        private static readonly string _nomeCertificado = "CERTIFICADO|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|02807779";
        private readonly string _licenca = "3d44eb2e2b14840499fcb818e4fe4e2478f6ab4c93a6fd610001c8f9e212fb7cc7aa50364e04d3efbd29b15562e74ab4deab817d74fe95d72f8fcdd773b89219";

        private NotaFiscalSaida _notaFiscal;
        private Estabelecimento _estabelecimento;
        
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();

        

        public void SalvarXml(string nfce, int tipoNotaFiscal, string chaveNota)
        {
            string folder = $@"c:\temp\{(tipoNotaFiscal == 1 ? "NotasFiscaisEmitida" : "NotasFiscaisImportada")}";
            string arquivo = Path.Combine(folder, $"{chaveNota}.xml");

            Directory.CreateDirectory(folder);

            File.Create(arquivo).Dispose();

            File.WriteAllText(arquivo, nfce);

            _contexto.NotasFiscalSaidas.Add(_notaFiscal);
            _contexto.SaveChanges();
        }

        #region EmissaoNFE
        private string MontarXmlNfce()
        {
            var versao = "4.00";

            var chave = ConverterParaChave();
            var identificador = ConverteParaIdentificador();
            var emitente = ConverteEmitente();
            var destinatario = ConverterDestinatario();
            var detalhesItens = ConverterParaItens();
            var transporte = ConverterParaTransporte();
            var pagamento = ConverterParaPagamento();
            var totais = ConverterParaTotais();
            var informacoesAdicionais = ConverterParaInformacoesAdicionais();

            var xml = _nfeUtil.NFe310(versao, chave, identificador, emitente, string.Empty, destinatario, string.Empty, string.Empty, detalhesItens, totais, transporte, string.Empty, pagamento, informacoesAdicionais, string.Empty, string.Empty, string.Empty, string.Empty);


            var idTokenCsc = "000001";
            var tokenCsc = "276F567E-9950-430E-9E66-0CFC13A4CD6D";
            var certificado = "CERTIFICADO|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|02807779";
            var versaoQrCode = "2"; // 100 para NFC-e até a v 3.10 | 2 para v 4.0 ou acima.
            var urlConsulta = string.Empty;
            var indSinc = "1"; // Indicador de processamento síncrono (0=não ou 1=sim), este parâmetro será utilizado para criar o lote de envio.
            var falha = string.Empty;
            var urlConsultaChave = "www.sefaz.rs.gov.br/nfce/consulta";

            var xmlNfce = _nfeUtil.AssinarNFCe400(xml, certificado, idTokenCsc, tokenCsc, versaoQrCode, urlConsulta, urlConsultaChave, indSinc, out var cStat, out var msgResultado, out var lote, out var urlnfCe);

            if (cStat != 5300)
            {
                //problema na nfe
            }


            _url = string.Empty;

            _nfeUtil.geraUrlNFCe(idTokenCsc, tokenCsc, versaoQrCode, xmlNfce, ref _url, out msgResultado);

            if (cStat == 5300 && !msgResultado.StartsWith("8400"))
            {
                // problema na nfe
            }

            return xmlNfce;
        }

        
        public void EmitirNfe(NotaFiscalSaida notaFiscal, ArgoMiniContext contexto)
        {
            _notaFiscal = notaFiscal;
            _estabelecimento = _contexto.Estabelecimentos.ToList().First();
            var nfce = MontarXmlNfce();


            var versao = "4.00";

            var contingencia = false;

            try
            {
                if (SefazOnline())
                    throw new Exception("Sem conectividade com os servidores da Secretaria da Fazenda.");
            }
            catch (Exception ex)
            {
                contingencia = true;
            }

            // testa contigencia 
            if (contingencia)
            {
                throw new Exception("Sem conectividade com os servidores da Secretaria da Fazenda.");
            }


            string msgDados;


            string procNFe;
            string msgRetWs = null;
            var cStat = 0;
            string msgResultado = null;
            string nroProtocolo = null;
            string dhProtocolo = null;

            try
            {
                var requisicaoConcluida = FuncoesThread.RunMethodWithTimeout(() => _nfeUtil.EnviaNFSincrono(_siglaWs, nfce, _nomeCertificado, versao, out msgDados, out msgRetWs, out cStat, out msgResultado,
                                                                            out nroProtocolo, out dhProtocolo, out string nfeAssinada, _proxy, _funcionario, _senha, _licenca), 15000, out procNFe);

                if (!requisicaoConcluida)
                    throw new TimeoutException("[408] Sem resposta da Secretaria da Fazenda.");

            }
            catch (Exception ex)
            {
                SalvarXml(nfce, 1, _notaFiscal.ChaveNota);
                _notaFiscal.Situacao = ESituacaoNotaFiscalSaida.ComErro;
                var falha = $"Falha ao transmitir a NFC-e {_notaFiscal.Numero} série {_notaFiscal.Serie}:{Environment.NewLine}Código falha: {ex.Message}";
                throw new Exception(falha);
            }

            // 100 NFC-e autorizada | 150 - NFC-e Autorizada com atraso |
            // 204 Duplicidade de NFC-e com mesma chave, neste caso só ajusta o status, a chave e a data do protocolo de autorização. |
            // 539 Duplicidade de NF-e, com diferença na Chave de Acesso
            if (cStat == 204 || cStat == 539)
            {
                _notaFiscal.Situacao = ESituacaoNotaFiscalSaida.ComErro;
                var falha = $"Falha ao transmitir a NFC-e {_notaFiscal.Numero} série {_notaFiscal.Serie}:{Environment.NewLine}Código falha: {cStat}";
                SalvarXml(nfce, 1, _notaFiscal.ChaveNota);
                throw new Exception(falha);
            }

            if (cStat == 100 || cStat == 150)
            {
                _notaFiscal.AutorizacaoNumeroProtocolo = nroProtocolo;
                _notaFiscal.AutorizacaoDataHoraProtocolo = dhProtocolo.ToDateTime();
                _notaFiscal.XmlAutorizadoCompactado = true;
                _notaFiscal.Situacao = ESituacaoNotaFiscalSaida.Autorizada;
                _notaFiscal.DataEmissao = DateTime.Now;
                
                var result = $"Nota Fiscal {_notaFiscal.Numero} série {_notaFiscal.Serie}:{Environment.NewLine}Emitida com sucesso!";
                SalvarXml(nfce, 1, _notaFiscal.ChaveNota);
                throw new Exception(result);

                //_notaFiscal.UrlQrCode = _url;

            }
            //NFC-e já está cancelada
            if (cStat == 218)
            {

                var reciboCancelamento = ConverterParaReciboNfe(msgRetWs);

                _notaFiscal.Situacao = ESituacaoNotaFiscalSaida.Cancelada;
                _notaFiscal.CancelamentoNumeroProtocolo = reciboCancelamento.Protocolo;
                _notaFiscal.CancelamentoDataHoraProtocolo = reciboCancelamento.DataHora;
                _notaFiscal.ChaveNota = reciboCancelamento.Chave;
                var falha = $"Nota Fiscal {_notaFiscal.Numero} série {_notaFiscal.Serie}:{Environment.NewLine}Já está cancelada!";

                SalvarXml(nfce, 1, _notaFiscal.ChaveNota);

                throw new Exception(falha);
            }
            //NFC-e já está inutilizada
            if (cStat == 206)
            {

                var xmlProtocolo = new XmlDocument();
                xmlProtocolo.LoadXml(msgRetWs);
                var nodoDataHora = xmlProtocolo.GetElementsByTagName("dhRecbto");
                var dataHoraEvento = xmlProtocolo.GetElementsByTagName("dhRegEvento");
                var nodoProtocolo = xmlProtocolo.GetElementsByTagName("nProt");

                _notaFiscal.Situacao = ESituacaoNotaFiscalSaida.Cancelada;
                _notaFiscal.InutilizacaoNumeroProtocolo = nodoProtocolo[0]?.InnerText;
                _notaFiscal.InutilizacaoDataHoraProtocolo = nodoDataHora.Count > 0
                    ? nodoDataHora[nodoDataHora.Count - 1].InnerText.ToDateTime()
                    : dataHoraEvento[0].InnerText.ToDateTime();
                var falha = $"Nota Fiscal {_notaFiscal.Numero} série {_notaFiscal.Serie}:{Environment.NewLine}Já está inutilizada!";

                SalvarXml(nfce, 1, _notaFiscal.ChaveNota);

                throw new Exception(falha);
            }

            SalvarXml(nfce, 1, _notaFiscal.ChaveNota);
            throw new Exception($"Falha na transmissão da NFC-e: {msgResultado}");

        }

        #region ChaveNfe
        private string ConverterParaChave()
        {
            var cUf = _estabelecimento.Uf == "RS" ? "43" : "43"; //ajustar
            var ano = (DateTime.Now.Year % 100).ToString();
            var mes = DateTime.Now.Month.ToString("00");
            var cnpj = _estabelecimento.Cnpj;
            var modelo = "65"; //ajustar 
            var serie = _notaFiscal.Serie.ToString();
            var numero = _notaFiscal.Numero.ToString();
            var tpEmis = "1";
            var codigoSeguranca = "ARGOMINI";
            _notaFiscal.TipoDocumento = 1;
            _notaFiscal.VistaPrazo = 1;
            _nfeUtil.CriaChaveNFe2G(cUf, ano, mes, cnpj, modelo, serie, numero, tpEmis, codigoSeguranca, out var msgResultado, out _cNf, out _cDv, out var chave);
            _notaFiscal.ChaveNota = chave;
            return chave;
        }
        #endregion

        #region Identificador
        private string ConverteParaIdentificador()
        {
            var cUf = 43; //ajustar
            var cNf = Convert.ToInt32(_cNf);
            var natOp = "Venda";
            var mod = 65;
            var serie = _notaFiscal.Serie;
            var nNf = _notaFiscal.Numero;
            var dhEmi = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            var dhSaiEnt = string.Empty;
            var tpNf = 1; //Saída
            var idDest = 1; // 1- Operação interna, 2 - Operação interestadual, 3- Exportação.
            var cMunFg = _estabelecimento.CodigoMunicipio;
            var nfRef = string.Empty;
            var tpImp = 5; //0 - Sem geração de DANFE, 1 - DANFE normal, Retrato, 2 - DANFE normal, Paisagem, 3 - DANFE Simplificado, 4 - DANFE NFC - e, 5 - DANFE NFC - e em mensagem eletrônica(o envio de mensagem eletrônica pode ser feita de forma simultânea com a impressão do DANFE; usar o tpImp - 5 quando esta for a única forma de disponibilização do DANFE)
            var tpEmis = 1; // 1 - Emissão normal | 9 - Contingência off-line da NFC-e.
            var cDv = Convert.ToInt32(_cDv);
            var tpAmb = 2; //1 - Produção, 2- Homologação
            var finNfe = 1; /* Finalidade da NF-e | 1 - Normal, 2 - Complementar, 3 - Ajuste, 4 - Devolução */
            var indFinal = 1; // Indicador de operação com Consumidor final | 0 - Não, 1 - Consumidor final
            var indPres = 1; //Indicador de presença do comprador no estabelecimento comercial no momento da operação | 0 - Não se aplica (por exemplo, Nota Fiscal complementar ou de ajuste), 1 - Operação presencial, 2 - Operação não presencial, pela Internet, 3 - Operação não presencial, Teleatendimento, 4 - NFC - e em operação com entrega a domicílio, 9 - Operação não presencial, outros.
            var procEmi = 0; //Código de identificação do processo de emissão da NF-e: Identificador do processo de emissão da NF-e | 0 - emissão de NF-e com aplicativo do contribuinte, 1 - emissão de NF-e avulsa pelo Fisco, 2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco, 3 - emissão NF - e pelo contribuinte com aplicativo fornecido pelo Fisco.
            var verProc = "000";

            return _nfeUtil.identificador400(cUf, cNf, natOp, mod, serie, nNf, dhEmi, string.Empty, tpNf, idDest, cMunFg, string.Empty, tpImp, tpEmis, cDv, tpAmb, finNfe, indFinal, indPres, procEmi, verProc, string.Empty, string.Empty);

        }
        #endregion

        #region Emitente
        private string ConverteEmitente()
        {

            var cnpj = _estabelecimento.Cnpj;
            var cpf = string.Empty;
            var xNome = _estabelecimento.RazaoSocial;
            var xFant = _estabelecimento.NomeFantasia;
            var xLgr = _estabelecimento.Logradouro;
            var nro = _estabelecimento.Numero;
            var xCpl = string.Empty;
            var xBairro = _estabelecimento.Bairro;
            var cMun = _estabelecimento.CodigoMunicipio;
            var xMun = _estabelecimento.NomeMunicipio;
            var uf = _estabelecimento.Uf;
            var cep = _estabelecimento.Cep;
            var cPais = "1058";
            var xPais = "Brasil";
            var fone = "";
            var ie = _estabelecimento.InscricaoEstadual;
            var iest = string.Empty;
            var im = "";
            var cnae = "";
            var crt = "1"; //detalhe 

            return _nfeUtil.emitente2G(cnpj, cpf, xNome, xFant, xLgr, nro, xCpl, xBairro, cMun, xMun, uf, cep, cPais, xPais, fone, ie, iest, im, cnae, crt);

        }
        #endregion

        #region Destinatário
        private string ConverterDestinatario()
        {
            var cnpj = string.Empty;
            var idEstrangeiro = string.Empty;
            var cpf = string.Empty;
            var xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"; // 60 char
            var xLgr = string.Empty;
            var nro = string.Empty;
            var xCpl = string.Empty;
            var xBairro = string.Empty;
            var cMun = string.Empty;
            var xMun = string.Empty;
            var uf = string.Empty;
            var cep = string.Empty;
            var cPais = string.Empty;
            var xPais = string.Empty;
            var fone = string.Empty;
            var email = string.Empty;
            var indIeDest = "9";
            var ie = string.Empty;
            var isuf = string.Empty;
            var im = string.Empty;


            return _nfeUtil.destinatario310(cnpj, cpf, idEstrangeiro, xNome, xLgr, nro, xCpl, xBairro, cMun, xMun, uf, cep, cPais, xPais, fone, indIeDest, ie, isuf, im, email);

        }
        #endregion

        #region Itens

        public string ConverterParaItens()
        {
            var itens = new StringBuilder();
            var ambienteHomologacao = true;
            var nItem = 1;

            foreach (var item in _notaFiscal.Itens)
            {
                var produto = ConverterParaDetalhesProduto(item, (nItem == 1 && ambienteHomologacao), ambienteHomologacao);
                var imposto = ConverterParaImpostosProduto(item);
                var infAdProd = string.Empty;
                var pDevolOpc = 0D;
                var vIpiDeveolOpc = 0D;

                itens.Append(_nfeUtil.detalhe310(nItem, produto, imposto, infAdProd, pDevolOpc, vIpiDeveolOpc));
                nItem++;
            }

            return itens.ToString();
        }

        protected string ConverterParaDetalhesProduto(NotaFiscalSaidaItem item, bool utilizarDescricaoHomologacao, bool ambienteHomologacao = false)
        {

            var cProd = item.Mercadoria.CodigoBarras.ToString(); //"7894900700015"; // codigo barras
            var xProd = utilizarDescricaoHomologacao ? @"NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : "coca cola lata";
            var ncm = item.Mercadoria.NcmId; /*"22021000";*/
            var cestOpc = "0301100"; // cest mercadoria || string.empty
            var cfop = 5405;
            var uCom = "UN";
            var qCom = item.Quantidade.ToString(); //quantidade replace , por . toString
            var vUnCom = item.PrecoVenda.ToString();
            var vProd = (double)item.TotalMercadoria; // precisa ser o total bruto
            var uTrib = "UN";
            var qTrib = item.Quantidade.ToString();
            var vUntrib = item.PrecoVenda.ToString();

            return _nfeUtil.produto400(cProd, "SEM GTIN", xProd, ncm, string.Empty, cestOpc, string.Empty, string.Empty, string.Empty, string.Empty, cfop, uCom, qCom, vUnCom, vProd, "SEM GTIN", uTrib, qTrib, vUntrib, 0, 0, 0, 0, 1,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        }

        protected string ConverterParaImpostosProduto(NotaFiscalSaidaItem notaFiscalItem)
        {
            var icms = ConverterParaIcmsProduto();
            var pis = ConverterParaPisProduto();
            var cofins = ConverterParaCofinsProduto();

            return _nfeUtil.impostoNT2015003(0, icms, string.Empty, string.Empty, pis, string.Empty, cofins, string.Empty, string.Empty, string.Empty);
        }

        private string ConverterParaIcmsProduto()
        {
            return _nfeUtil.icms400("0", "500", 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        private string ConverterParaPisProduto()
        {
            return _nfeUtil.PIS("99", 0, 0, 0, 0, 0);
        }

        private string ConverterParaCofinsProduto()
        {
            return _nfeUtil.COFINS("99", 0, 0, 0, 0, 0);
        }

        #endregion Itens

        #region Totais
        public string ConverterParaTotais()
        {
            var icmsTot = ConverterParaTotalIcms();
            var issqnTot = string.Empty;
            var retTrib = string.Empty;

            return _nfeUtil.total(icmsTot, issqnTot, retTrib);
        }

        public string ConverterParaTotalIcms()
        {
            _notaFiscal.ValorTotalNota = _notaFiscal.Itens.Sum(c => c.TotalMercadoria);
            _notaFiscal.QuantidadeItensNota = _notaFiscal.Itens.Sum(c => c.NotaFiscalSaidaItemId);
            _notaFiscal.ValorMercadorias = _notaFiscal.ValorTotalNota;
            var vProd = (double)_notaFiscal.ValorTotalNota;
            var vNf = vProd;

            return _nfeUtil.totalICMS400(0, 0, 0, 0, vProd, 0, 0, 0, 0, 0, 0, 0, 0, vNf, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }


        #endregion

        #region Transporte

        public string ConverterParaTransporte()
        {
            return _nfeUtil.transportador2G("9", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        #endregion Transporte

        #region Pagamento

        public string ConverterParaPagamento()
        {
            var troco = 0; // valorpago - valor total
            var detPag = this.ConverterParaDetalhesPagamento();
            var vTroco = troco > 0 ? troco : 0D;

            return _nfeUtil.pagamento400(detPag, vTroco);
        }

        private string ConverterParaDetalhesPagamento()
        {
            var indPagOpc = "0"; // 0 vista 1 prazo
            var tPag = "01"; // dinheiro
            var vPag = (double)_notaFiscal.Itens.Sum(c => c.TotalMercadoria);  // valor pago da nota


            return _nfeUtil.detPag(indPagOpc, tPag, vPag, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        #endregion Pagamento

        #region Informações adicionais

        public string ConverterParaInformacoesAdicionais()
        {
            return _nfeUtil.infAdic2G(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        #endregion Informações adicionais
        #endregion


        public XmlDocument BaixarXmlNfe(string chave)
        {
            var xml = new XmlDocument();

            try
            {
                var util = new NFe_Util_2G.Util();
                var nomecertificado = "CERTIFICADO|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|02807779";
                string msgDados, msgRetWs, msgResultado, verAplic, nsuXml, schemaXml;
                int cStat;
                string result = "";
                Estabelecimento _estabelecimento;

                using (var contexto = new ArgoMiniContext())
                {
                    _estabelecimento = contexto.Estabelecimentos.ToList().First();
                }

                //var certTeste =
                //        "CERTIFICADO|MIIOsAIBAzCCDmwGCSqGSIb3DQEHAaCCDl0Egg5ZMIIOVTCCBb4GCSqGSIb3DQEHAaCCBa8EggWrMIIFpzCCBaMGCyqGSIb3DQEMCgECoIIE9jCCBPIwHAYKKoZIhvcNAQwBAzAOBAjWMiFn5VSVHwICB9AEggTQ8qC7IWI816lzIvV48BN4Vv06PV67ZRw5DAXEfyEG7XHJ6ue0FJQKhOol5yfE+RqkRsi5V2phPkSA1Z+iI2fF7rg1N/tzLLaTYXAds4gS1PUlARPoxtx7KiU4FztitobTKmXv8QQMunb2OYO/mJDvlniELZQiKmcgqiZ/B7qgH63JSkj7Qn1cc1JsutRTQPNCyxAAbUkCWjahh/dek2y/GRWfxjhYFRnssqQ6n0gM45wj7+/uaauEZaqdqxuySsHsdHv1QE9sV87b/g8us4yItTsCu6EgymSsfa58KCr4mFl/9oKrQNVmw7jJRjO+iUzrBQnxYQ48aAZnvznBw/i7pf4KjX5fitqcut5ibSWiXXbyAsJDt0Z9gSw0Rqdeapt1tKlmXD12In/E6tbr4SdyOJ9xop2Fdjz0BLyhUqnK+yk0YAcynfLPhlcQGgjrq0Uj2RupnUNa9dKaQm8iIfRyJedhNCyjsXgqnCWcnb/bPjpTCxM41Pe0GzEVCcVa/wsSHdZpy93PsIJMljutmL9Ps0Lq5AFdTlYuawF4zgR/F0ADL6+f7sgIndxRz9wDdyy42+RbE9pF2TI+c8Pnkv4RZdgGVSyEdf3l5W1Zvlz9kbo6898iln8zWqzz/OqOhJfpdZ/5Rv4hXEvLcdLbfCtkQ6lWWx7/R37qzWgtmQFcJmN6bNJqs3GKiifEhPTPQw58CUSv9gxXJPqV9EM7BqTKdeX2u349kYgpFnxGr9mBi90/nQMhaxCau6rpWonsRlmrPMJRVViKoV9SsmhfOtpea1Sg55XwSt1PpTaZ6hfofLqlz0uxw6QWIUFU6JUH1m2aL2Jo46h0Rt+Kxycii6bPEVw213RdXLtjJCYtjqWYWeFfol71h6KCrFBwCHnDjVRh7eD1Mw7GrgECEsjWqJ7oc5hxPOjUJ5vNfXF5KW36d8xBhHw+bl8bVVKwdDDgFf8Fu6gr5FZ6b9+urWb+75iORmbl8pvdb8GEklOGm54hqpKfFL+v0LVEIW+ErC+qQIQHlTv9VUBQUe1fOW3rXHs81IbLioG3X6g88wyay3vN3BeFpDCeo4vtbAdNuw5q083uQXgNsu4X0/bTLhFalWbI274PD9yl8FkUJeC6rtZQyUbmOEkM9Ndc03uVV7XSaoE9ZI3CxorbxvPj45MTtRcYfh4C1VjWuuUDfcZe56bpeXCJ9zxWc07IqtvLklFwk3NZPlgNHus9CdA7M+ApATS07unqE5tZdoGmeYjHaRHNVkhfLeXcI7W92qMYpmJQV4ucHr11S65bbk4xFtxOtA5IBeQmvSYY9i5T0R+cf02vfecTmjA4jQbgwsFVKK8WKYKAQ62YWHgiV2dAAlu+ZLsV8Y2Uw74UpnTC+fodO3Aw4K6IMnoOu3N012dMRhhzkF9b5gZ4HvAaGLO1kG2Q5j694LkLEcoBWS+2hzTb5uknOCbfpXRS6ibmXMXiIHwtMmKcu8/9V2g/jRics3sXUNlKzqDd7boRAF2qqAXvhjRzWIbGkOK2T9Z1Lig39uwon68RrEHyzWJ5tf0fGjh+w1Ulmg22mMprm6h/KAXf5eHe/Viol3EnHl2lC2J+Ka9Hp2Zfy0glG5vTQtNnUUzmC0YRE8dVqLV65x7OFOB/RIP049MxgZkwEwYJKoZIhvcNAQkVMQYEBAEAAAAwIwYJKoZIhvcNAQkUMRYeFAAxADAAMAAwADgAMAA5ADUAMwAyMF0GCSsGAQQBgjcRATFQHk4ATQBpAGMAcgBvAHMAbwBmAHQAIABTAHQAcgBvAG4AZwAgAEMAcgB5AHAAdABvAGcAcgBhAHAAaABpAGMAIABQAHIAbwB2AGkAZABlAHIwggiPBgkqhkiG9w0BBwagggiAMIIIfAIBADCCCHUGCSqGSIb3DQEHATAcBgoqhkiG9w0BDAEDMA4ECGXRvhI4IJVWAgIH0ICCCEiHp2pjh3uZ5irwKp1B43ddx9WutmwawC17vgvlZe44P70GDlEdqcOidkGKpEpRkpOCct/g4lIFfKAsf8MAoBPPM3x9RJWT77XInGU2r7p6nhKZOtMGZYP0ejDUNOhREFEMwLC/9y6KwwigOhX2F/2IwjJUaBUoQrqhJLKjKkkOjhDuTKHYe1fN90kQGcPt1ifwEix4bW94rxcW/nFBrOH25U/HeiF/MIPiUnM2uT5HqZ4XphMUuEKtTcj+IwUztew9ozJoCNflRhZqKeBizF49FWNZnKgBRuDVOyODgRI3l6R1+TOvVd4ffJC1BiPNIH2EKQuSmGwuXFBBeInePqoRWxsL+K6Ol90vcLZ0rnQgFocbNgYSZu/LEE1r/YkWpUwDaxGrftl7UzveaID+X7vB58Q/h7//I18QX6LksG49b3rshLGJ4zvRORGNRuM5l1bl27dqccqxBMV9MlGR37hLWySUpsBHGnrrRwlnagpq3ikiSKwLKsgQhawfCDFra0HSFlquiEOFiY1OryUmxZaHiBoUH/KSPXotEv6zyPQZzkzwi7MVYga6uQbj3NouoS1fPfPtVnGl8JnuXdAQk9ydJiRpmxahX2ERalurUeJ1ZE/jDEh+CpIVLjDFxf/qrqQCwHcZHD4TKHZYuA5bXr+TJ5auwmqOE/u+Sqx4jet1MTKbPcMQK2aEZ05mPWEnblbs+ua4GQXlBnGdkZkcl34FbG7NYWaVEzBsG4qD+qxQlAUwBSEb+xmyw1miXp8aBYWxSGPKwJBR33w6cXlb4EMzGOSYzLdip8KPgwG0bY1nbHz2n43sCe4AJVP4W0OZLIIn6zk/1rywtWfzgUWrfSjZ8qbGvF5nRSXsmbd9N8JGqSDWULSKEs9Qc7b4Dz92q8PVdtHgDJ6l8/GUG2FM66v5Y6lWP/vsFwnoNV9uX24CH5GbfTEBnx4LCNJiU70H2TkZBci4Vg3tKvcJWTqN05TkmGz3IZzeJL5BQ8Egz52qbJIvY5MBDe/tG0+zvqUkVEk+LbJ6Iz6hhiAfxTABQ2YZeBqk+CIrie0BFXcLhkOBC6k5ArbHK2dC5biRhhszQn8kL6KrdlpAWfRAHL3UY6fguOxlcVOo2yhkF+ObTWCubs1Q0B06emcCLSD35sReNUcApEwZJUe5urJ1wQXOZ9VMWlMf6CWNHX4ncDJqtXee0ScRwduxruFoBQMLBnG63x+8ux6eutWKbYkShCUcG3si7zkobwxND0mfQfACu4rHhPOh70IKBgeds2ZCOdonNfwVvzRYEeEa+yUm4vBhSZ02bDEBMOf03uJ3ahUMUCjUfN0S0W0gg10/uG+EaI7RxUV7ZSESvIuE6vKB1SH4fyZ8aP+IOUiwNe+XzLhWKXFj1JkgKGYJYnl0KT9RjDDKtu1HeR7CKGMQHXHRNQqDP3XomhFZ1JYZN97zrCGzG+4t9MXR+ifqzLzV72B54a+Z8aXbMMBSxFxV+U0oKz/v+kddWJiAH2jyz8ko91fx7W+kI4DaAXqijYzZ/ITM7kEif+X2jNimQJktP1SwY67IDWeLkSWKz3ofhYuo0pFda23+JeccCZUos5XX51hu3J3u2DPMbCSW4BQnpIIXNrmzb3fA9nHs5feX8hT+D/myzn/P4ujLNnn1KPdMFsWoJbsIrbgTNOZtG6S7NozrwHS6xjB5QYrUPgN0cwKDI9+aOAsJWw9ba3yFs3kZ4bs2USMOkYBPtY9kVBh9dElHF6htu7wniKgfyMaEFAUyGuUruH7tPdi6QTUa4t4YBFZI89w3mtBTVc9eBAG9CgmAZhakSjCRxIOmLAokxL51NCiNy2Eu3iA9Xf7OZVvVFBEzXywvHhndjc8Hb8SJX+DLJhuozm7qOW6HyUCa5sQutyZZPG0DYg1QVnhzw56X94nbexDAWWESK9zcNikfAypdIMCfO1PL7uvSzbeHnv4gV17HUndsQzxYQvE4ZjBgCcvk0cx9HSZW3oS4Qa9kTAKs6mYS/0/IGya3CApD9UNiwkeTY4Y1vNvGMElXXkxFzuX5x7EgO7G13o3Xj/9QUAZjEW1kq69VeqiCQ1gEAKu+mW97E17RTblumTOw9czSvgNPa60fd54aE+ufVIntlXKK+HSURRTBolvsQ6YfIgXaJV7WGIIMbXcgISsZpcP5WbnfLH0kyOb7R61lnTpuPxqDLGuhTvk6J8KsdP4x9uDsc+h0UTsdE1b/tfx0NFf1L58wUGnVxCLtxMVlMZFyYyIS4eezt4UW5XbRlzdEV3QVDPjD7BE/uHtSreUBI1DGflwXwIcZLRBzbTyibbkfEIUdWLU55kaSDdmSCZSj+Vubn3BTsx9yxlj+ckPodQHCWMkAVaqaEfJ73GogJkSMdEXJturLaCv7zvIto29k9UlqGAAFYd+3+sckX1K9Lq5NRGHYr8HpwFtRypVV1RpY0de4C5xK0GsgC2behHBoEAZhtZwhhDUkGAcFiEubC10aJoTs9jFOnkVvhH836bDV56+u7GRTSh9GKqYFQHwyEguybNHJM4G1Es5xBix2/HK+gpst1Ci5mD4Qt8F/ePYp9i7o50oE74wug3OYdQSOao2BcDiO/qiXOxC1JDYW+bGYPwf7/sTXGo73aUqgO+vM3RjSw36+zwpL19dPouo51OpOw98RmOqZEUybNsZD5eOr5yW5EPN0FqVKGG1edcNCnW+g1Y4GKXwIxduHRQ7uDxef1WFPEjo7eE1gjAzu9fbrGJnlUBFfhqgmooJkOd/4215emBJfMAeinalfrIa2XLuPqyJwAPxkogEeNOeOjJf1S6b1VNNvnIn+pRZVM9h96DA7MB8wBwYFKw4DAhoEFEsCramxPa+57G2B7UT2ROVDvMfhBBQKZHFWX3/c/w6f+MUsKI2hkCeTTAICB9A=|19848481";

                //var chaveTeste = "35181023020140000127550130000007501485320155";
                //var cnpjTeste = "07947921000105";

                string mensagemDados, mensagemResultado, mensagemRetornoWs, protocolo, dataProtocolo;
                int codigoStatusRetorno;
                //chave = "35181023020140000127550130000007541593447860"; //35181023020140000127550130000007541593447860 -  35181023020140000127550130000007521629538211 - 35181023020140000127550130000007501485320155

                util.EnviaManDest("AN",
                        2,
                        nomecertificado,
                        "4.00",
                        out mensagemDados,
                        out mensagemRetornoWs,
                        out codigoStatusRetorno,
                        out mensagemResultado,
                        chave,
                        _estabelecimento.Cnpj,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        0, //documento cofnirmado
                        "",
                        out protocolo,
                        out dataProtocolo,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        _licenca);
                switch (codigoStatusRetorno)
                {

                    case 135:
                    case 136:
                    case 573:
                        for (var i = 1; i < 3; i++)
                        {
                            result = util.consChNFe("AN",
                                2,
                                nomecertificado,
                                "4.00",
                                out msgDados,
                                out msgRetWs,
                                out cStat,
                                out msgResultado,
                                _estabelecimento.Cnpj,
                                chave,
                                out verAplic,
                                out nsuXml,
                                out schemaXml,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                String.Empty);

                            if (msgResultado == "138 - Documento localizado")
                                i = 4;
                        }

                        break;
                    default:
                        {
                            throw new ArgumentException(mensagemResultado);
                        }
                }

                xml.LoadXml(result);
            }
            catch (Exception ex)
            {
                //Log.Write.Error(ex.MensagemErroParaLog());
            }

            return xml;
        }


        public void CancelarNfce(NotaFiscalSaida notaSaida, string justificativaCancelamento)
        {
            try
            {
                
                using (var contexto = new ArgoMiniContext())
                {
                    notaSaida = contexto.NotasFiscalSaidas.OrderByDescending(c=> c.NotaFiscalSaidaId).FirstOrDefault(c=> c.Situacao == ESituacaoNotaFiscalSaida.Autorizada);

                    if (notaSaida == null)
                    {
                        throw new Exception("Houve um erro na tentativa de cancelamento, tente mais tarde.");
                    }

                    var dataHora = notaSaida.DataEmissao;


                    var tipoAmbiente = 2;
                    var nomeCertificado = _nomeCertificado;
                    var versao = "4.00"; /* configuracoes.VersaoEventoCancelamento;*/
                    var chave = notaSaida.ChaveNota;
                    var nroProtocolo = notaSaida.AutorizacaoNumeroProtocolo;
                    var justificativa = justificativaCancelamento;
                    var dhEvento = dataHora.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    var licenca = _licenca;

                    var procCancNFe = _nfeUtil.CancelaNFEvento(_siglaWs, tipoAmbiente, nomeCertificado, versao,
                        out var msgDados, out var msgRetWs, out var cStat, out var msgResultado, chave, nroProtocolo,
                        justificativa, dhEvento, out var nProtocoloCanc, out var dProtocoloCanc, _proxy, _funcionario,
                        _senha, licenca);

                    // Nota Cancelada
                    if (cStat == 135 || cStat == 155 || cStat == 573)
                    {

                        var reciboCancelamento = ConverterParaReciboNfe(msgRetWs);

                        notaSaida.Situacao = ESituacaoNotaFiscalSaida.Cancelada;
                        notaSaida.CancelamentoNumeroProtocolo = reciboCancelamento.Protocolo;
                        notaSaida.CancelamentoDataHoraProtocolo = reciboCancelamento.DataHora;
                        notaSaida.ChaveNota = reciboCancelamento.Chave;
                        MercadoriaEstoqueNegocio.CancelamentoNotaFiscalSaida(notaSaida, contexto);
                    }
                    else
                    {
                        var falha =
                            $"Falha ao cancelar a NFC-e {notaSaida.Numero} série {notaSaida.Serie}:{Environment.NewLine}Código falha: {cStat} - {msgResultado}";
                        throw new Exception($"Falha no cancelamento da NFC-e: {falha}");
                    }
                    contexto.Entry(notaSaida).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InutilizarNfce(NotaFiscalSaida notaSaida, string justificativaCancelamento)
        {
            try
            {
                _estabelecimento = _contexto.Estabelecimentos.ToList().First();

                using (var contexto = new ArgoMiniContext())
                {
                    notaSaida = contexto.NotasFiscalSaidas.FirstOrDefault(c => c.NotaFiscalSaidaId == 6);


                    if (notaSaida == null)
                    {
                        throw new Exception("Houve um erro na tentativa de inutilização, tente mais tarde.");
                    }

                    //if (notaSaida.Situacao != ESituacaoNotaFiscalSaida.ComErro)
                    //{
                    //    throw new Exception("Houve um erro na tentativa de inutilização, tente mais tarde.");
                    //}


                    var dataHora = notaSaida.DataEmissao;


                    var tipoAmbiente = 2;
                    var nomeCertificado = _nomeCertificado;
                    var versao = "4.00";
                    var cUf = "43";
                    var ano = dataHora.Value.ToString("yy");
                    var cnpj = _estabelecimento.Cnpj;
                    var modelo = "65";
                    var serie = /*notaSaida.Serie.ToString()*/"547";
                    var nroNfeInicial = "13"; /*notaSaida.Numero.ToString()*/
                    var nroNfeFinal = "13"; /*notaSaida.Numero.ToString()*/;
                    var justificativa = justificativaCancelamento;
                    var licenca = _licenca;

                    _nfeUtil.InutilizaNroNF2G(_siglaWs, tipoAmbiente, nomeCertificado, versao, out var msgDados,
                        out var msgRetWs, out var cStat, out var msgResultado, cUf, ano, cnpj, modelo, serie, nroNfeInicial, nroNfeFinal, justificativa, out var nProtocoloInut, out var dProtocoloInut,
                        _proxy, _funcionario, _senha, licenca);

                    // Nota Cancelada
                    if (cStat == 102 || cStat == 206 || cStat == 563)
                    {

                        var reciboInutilizacao = ConverterParaReciboNfe(msgRetWs);

                        notaSaida.Situacao = ESituacaoNotaFiscalSaida.Inutilizada;
                        notaSaida.CancelamentoNumeroProtocolo = reciboInutilizacao.Protocolo;
                        notaSaida.CancelamentoDataHoraProtocolo = reciboInutilizacao.DataHora;
                        notaSaida.ChaveNota = reciboInutilizacao.Chave;
                        SalvarXml(msgRetWs, 1, notaSaida.ChaveNota);
                        MercadoriaEstoqueNegocio.CancelamentoNotaFiscalSaida(notaSaida, contexto);
                    }
                    else
                    {
                        var falha = $"Falha ao inutilizar a NFC-e {notaSaida.Numero} série {notaSaida.Serie}:{Environment.NewLine}Código falha: {cStat} - {msgResultado}";
                        throw new Exception(falha);
                    }

                    contexto.Entry(notaSaida).State = EntityState.Modified;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public bool SefazOnline()
        {
            string msgDados;
            string retWs;
            string msgResultado;

            var util = new NFe_Util_2G.Util();

            int resultado = 0;
            FuncoesThread.RunMethodWithTimeout(() => util.ConsultaStatus2G(_siglaWs, "rs", 2, _nomeCertificado, ""/*Versao*/, out msgDados, out retWs, out msgResultado,
                String.Empty, String.Empty, String.Empty), 1000, out resultado);
            return resultado == 107;
        }

        public static ReciboNfe ConverterParaReciboNfe(string msgRetWs)
        {
            var xmlProtocolo = new XmlDocument();
            xmlProtocolo.LoadXml(msgRetWs);
            var nodoChave = xmlProtocolo.GetElementsByTagName("chNFe");
            var nodoDataHora = xmlProtocolo.GetElementsByTagName("dhRecbto");
            var dataHoraEvento = xmlProtocolo.GetElementsByTagName("dhRegEvento");
            var nodoProtocolo = xmlProtocolo.GetElementsByTagName("nProt");
            var nodoJustificativa = xmlProtocolo.GetElementsByTagName("xJust");

            return new ReciboNfe
            {
                Chave = nodoChave.Count > 0 ? nodoChave[0].InnerText : string.Empty,
                DataHora = nodoDataHora.Count > 0 ? nodoDataHora[nodoDataHora.Count - 1].InnerText.ToDateTime() : dataHoraEvento[0].InnerText.ToDateTime(),
                Protocolo = nodoProtocolo[0]?.InnerText,
                Justificativa = nodoJustificativa.Count > 0 ? nodoJustificativa[0].InnerText : string.Empty
            };
        }


    }


    public static class FuncoesThread
    {
        public static bool RunMethodWithTimeout<T>(Func<T> func, int timeoutInMilliseconds, out T result)
        {
            try
            {
                var t = default(T);
                var thread = new Thread(() => t = func());
                thread.Start();

                var taskCompleted = thread.Join(timeoutInMilliseconds);
                if (!taskCompleted)
                    thread.Interrupt();

                result = t;
                return taskCompleted;
            }
            catch (Exception)
            {
                result = default(T);
                throw;
            }
        }

        public static DateTime ToDateTime(this object value)
        {
            var result = DateTime.MinValue;

            if (value == null || value == DBNull.Value)
                return result;

            if (value is DateTime)
                result = Convert.ToDateTime(value);
            else
            {
                if (DateTime.TryParse(value.ToString(), out var valorConvertido))
                    result = valorConvertido;
            }

            return result;
        }


    }
}
