using System;
using System.Linq;
using System.Text;
using System.Threading;
using ArgoMini.Models;
using NFe_Util_2G;

namespace ArgoMini.Negocio
{

    public class FlexDocsNegocio
    {
        private readonly Util _nfeUtil = new Util();
        private string _cNf = string.Empty;
        private string _cDv = string.Empty;

        private NotaFiscalSaida _notaFiscal;
        private Estabelecimento _estabelecimento;
        private readonly ArgoMiniContext _contexto = new ArgoMiniContext();

        public void EmitirNfe(NotaFiscalSaida notaFiscal)
        {
            _notaFiscal = notaFiscal;
            _estabelecimento = _contexto.Estabelecimentos.ToList().First();
            var nfce = MontarXmlNfce();
            //salvar xml em arquivo temp

            var siglaWs = "rs";

            var nomeCertificado = "CERTIFICADO|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|02807779";
            var versao = "4.00";
            var proxy = string.Empty; ;
            var funcionario = string.Empty; ;
            var senha = string.Empty;
            var licenca = "aaaa";

            var contingencia = false;
            // testa contigencia 
            if (contingencia)
            {

            }
            else
            {

                string msgDados;
                if (contingencia)
                {
                    //Log.Write.Info("Sem conectividade com os servidores da Secretaria da Fazenda.");
                    throw new Exception("Sem conectividade com os servidores da Secretaria da Fazenda.");
                }
                else
                {
                    string procNFe;
                    string msgRetWs = null;
                    var cStat = 0;
                    string msgResultado = null;
                    string nroProtocolo = null;
                    string dhProtocolo = null;

                    try
                    {
                        //enviando nfce
                        
                        var requisicaoConcluida = FuncoesThread.RunMethodWithTimeout(() => _nfeUtil.EnviaNFSincrono(siglaWs, nfce, nomeCertificado, versao, out msgDados, out msgRetWs, out cStat, out msgResultado, out nroProtocolo, out dhProtocolo, out string nfeAssinada, proxy, funcionario, senha, licenca), 15000, out procNFe);

                        if (!requisicaoConcluida)
                        {
                            
                            //Log.Write.Info("[408] Sem resposta da Secretaria da Fazenda.");
                            throw new TimeoutException("[408] Sem resposta da Secretaria da Fazenda.");
                        }
                    }
                    catch (Exception)
                    {
                        //Log.Write.Info("Salvando dados do XML assinado.");
                        //new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosXmlAssinado(contexto, notaSaida, nfce, _url, _chave);

                        //var falha = $"Falha ao transmitir a NFC-e {notaSaida.Numero} série {notaSaida.Serie}:{Environment.NewLine}Código falha: {ex.MensagemErroParaLog()}";
                        //if (_xmlMontadoComSucesso)
                        //{
                        //    Log.Write.Info("Salvando motivo falha.");
                        //    new NotaFiscalSaidaNegocio().SalvarMotivoFalhaEmissao(notaSaida, $"{falha}{Environment.NewLine}XML: {nfce}");
                        //}

                        //Log.Write.Error(falha);
                        //Log.Write.Info($"XML: {nfce}");
                        throw new Exception("Falha na transmissão da NFC-e: Sem conectividade com a Secretaria da Fazenda.");
                    }

                    // 100 NFC-e autorizada | 150 - NFC-e Autorizada com atraso |
                    // 204 Duplicidade de NFC-e com mesma chave, neste caso só ajusta o status, a chave e a data do protocolo de autorização. |
                    // 539 Duplicidade de NF-e, com diferença na Chave de Acesso
                    if (cStat == 100 || cStat == 150 || cStat == 204 || cStat == 539)
                    {
                        
                        //var reciboAutorizacao = ParseNfe.ConverterParaReciboNfe(msgRetWs);

                        //Log.Write.Info($"NFC-e transmitida status: {cStat}");
                        //if ((cStat == 204 || cStat == 539) && notaSaida.TentativasDeTransmissao == 0)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(notaFiscalConsumidorEletronica?.XmlAssinado))
                        //    {
                        //        Log.Write.Info("Recuperando XML autorizado.");
                        //        var xmlAssinado = notaFiscalConsumidorEletronica.XmlAssinado.RemoverQuebraDeLinha(" | ");
                        //        procNFe = _nfeUtil.CriaProcNFe2G(siglaWs, ref xmlAssinado, out nroProtocolo, out var retCanc, out var resultado, nomeCertificado, out msgResultado, proxy, funcionario, senha);

                        //        // 6201 = nfeProc montado com sucesso e o xml autorizado não esta em branco.
                        //        if (resultado == 6201 && !procNFe.IsNullOrWhiteSpace())
                        //        {
                        //            var dadosProtocolo = nroProtocolo.Split(' ');

                        //            notaSaida.TentativasDeTransmissao = notaSaida.TentativasDeTransmissao + 1;
                        //            var recibo = new ReciboNfe
                        //            {
                        //                Chave = _chave,
                        //                Protocolo = dadosProtocolo[0],
                        //                DataHora = $"{dadosProtocolo[1]} {dadosProtocolo[2]}".ToDateTime()
                        //            };

                        //            new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosAutorizacao(contexto, notaSaida, recibo, recibo.Protocolo, recibo.DataHora, procNFe, true, _url);
                        //            return;
                        //        }
                        //        else if (cStat == 204)
                        //        {
                        //            notaSaida.TentativasDeTransmissao = notaSaida.TentativasDeTransmissao + 1;
                        //            Log.Write.Info("Salvando dados da transmissão.");
                        //            new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosAutorizacao(contexto, notaSaida, reciboAutorizacao, nroProtocolo, dhProtocolo.ToDateTimeOrNull(), xmlAssinado, true, _url);
                        //            Log.Write.Info("Dados da transmissão salvos.");
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            Log.Write.Error($"Já existe uma NFC-e com o número {notaSaida.Numero} e série {notaSaida.Serie} na Secretaria da Fazenda | {msgResultado}");
                        //            throw new Exception($"Já existe uma NFC-e com o número {notaSaida.Numero} e série {notaSaida.Serie} na Secretaria da Fazenda.");
                        //        }
                        //    }
                        //    else if (cStat == 204)
                        //    {
                        //        notaSaida.TentativasDeTransmissao = notaSaida.TentativasDeTransmissao + 1;
                        //        Log.Write.Info("Salvando dados da transmissão.");
                        //        var xml = notaFiscalConsumidorEletronica?.XmlAssinado.RemoverQuebraDeLinha(" | ");
                        //        new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosAutorizacao(contexto, notaSaida, reciboAutorizacao, nroProtocolo, dhProtocolo.ToDateTimeOrNull(), xml, !xml.IsNullOrWhiteSpace(), _url);
                        //        Log.Write.Info("Dados da transmissão salvos.");
                        //        return;
                        //    }

                        //    Log.Write.Error($"Já existe uma NFC-e com o número {notaSaida.Numero} e série {notaSaida.Serie} na Secretaria da Fazenda | {msgResultado}");
                        //    throw new Exception($"Já existe uma NFC-e com o número {notaSaida.Numero} e série {notaSaida.Serie} na Secretaria da Fazenda.");
                        //}

                        //notaSaida.TentativasDeTransmissao = notaSaida.TentativasDeTransmissao + 1;
                        //Log.Write.Info("Salvando dados da transmissão.");
                        //new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosAutorizacao(contexto, notaSaida, reciboAutorizacao, nroProtocolo, dhProtocolo.ToDateTimeOrNull(), procNFe, (cStat != 204), _url);
                        //Log.Write.Info("Dados da transmissão salvos.");

                        //if (configuracoes.Ambiente == EAmbiente.Homologacao)
                        //{
                        //    throw new NotaEmAmbienteHomologacaoException("ATENÇÃO: NFC-e emitido em ambiente de homologação.");
                        //}
                    }
                    //NFC-e já está cancelada
                    else if (cStat == 218)
                    {
                        //Log.Write.Info("Documento já cancelado na SEFAZ.");
                        //var reciboCancelamento = ParseNfe.ConverterParaReciboNfe(msgRetWs);
                        //new NotaFiscalSaidaNegocio().CancelarNotaSaida(contexto, notaSaida, "DOCUMENTO JÁ CANCELADO NA SECRETARIA DA FAZENDA.", EStatusDocumentoSaida.Cancelada, dataHora);
                        //new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosCancelamento(contexto, notaFiscalConsumidorEletronica, reciboCancelamento, procNFe);
                    }
                    //NFC-e já está inutilizada
                    else if (cStat == 206)
                    {
                        //Log.Write.Info("Numeração inutilizada na SEFAZ.");
                        //var reciboInutilizacao = ParseNfe.ConverterParaReciboNfe(msgRetWs);
                        //new NotaFiscalSaidaNegocio().CancelarNotaSaida(contexto, notaSaida, "DOCUMENTO JÁ INUTILIZADO NA SECRETARIA DA FAZENDA.", EStatusDocumentoSaida.Inutilizada, dataHora);
                        //new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosInutilizacao(contexto, notaSaida, notaFiscalConsumidorEletronica, reciboInutilizacao);
                    }
                    else
                    {
                        //Log.Write.Info("Salvando XML assinado.");
                        //new NotaFiscalConsumidorEletronicaNegocio().SalvarDadosXmlAssinado(contexto, notaSaida, nfce, _url, _chave);

                        //var falha = $"Falha ao transmitir a NFC-e {notaSaida.Numero} série {notaSaida.Serie}:{Environment.NewLine}Código falha: {cStat} - {msgResultado}";
                        //if (_xmlMontadoComSucesso)
                        //{
                        //    Log.Write.Info($"Salvando motivo falha. {falha}{Environment.NewLine}XML: {nfce}");
                        //    new NotaFiscalSaidaNegocio().SalvarMotivoFalhaEmissao(notaSaida, $"{falha}{Environment.NewLine}XML: {nfce}");
                        //}

                        //Log.Write.Error(falha);
                        //Log.Write.Info($"XML: {nfce}");
                        //throw new Exception($"Falha na transmissão da NFC-e: {msgResultado}");
                    }
                }
            }
        }

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

            var xml = _nfeUtil.NFe310(versao, chave, identificador, emitente, string.Empty, destinatario, string.Empty, string.Empty, detalhesItens, totais, transporte, string.Empty, pagamento, informacoesAdicionais, string.Empty,string.Empty, string.Empty, string.Empty);


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


            var _url = string.Empty;

            _nfeUtil.geraUrlNFCe(idTokenCsc, tokenCsc, versaoQrCode, xmlNfce, ref _url, out msgResultado);

            if (cStat == 5300 && !msgResultado.StartsWith("8400"))
            {
                // problema na nfe
            }

            return xmlNfce;
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

            _nfeUtil.CriaChaveNFe2G(cUf, ano, mes, cnpj, modelo, serie, numero, tpEmis, codigoSeguranca, out var msgResultado, out _cNf, out _cDv, out var chave);

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
            var vProd = (double)_notaFiscal.Itens.Sum(c => c.TotalMercadoria);
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
    }
}
