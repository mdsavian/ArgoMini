using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ArgoMini.Enums;
using ArgoMini.Models;

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

        public static NotaFiscalCompra ConsultarNotaCompra(string chaveNota)
        {
            var xml = new FlexDocsNegocio().BaixarXmlNfe(chaveNota);

            return MontarNotaCompraComXml(xml);
        }

        public static NotaFiscalCompra MontarNotaCompraComXml(XmlDocument xml)
        {
            NotaFiscalCompra notaCompra = null;
            if (xml != null)
            {
                XmlNodeList nodoInfNFe = xml.GetElementsByTagName("infNFe");
                if (nodoInfNFe.Count > 0)
                {
                    var chave = nodoInfNFe[0].Attributes["Id"].InnerText.Substring(3);
                    var numeroDocumento = RetornarTag(xml, "nNF");
                    var serieDocumento = RetornarTag(xml, "serie");
                    var emissao = DateTime.Parse(RetornarTag(xml, "dhEmi"));
                    var dataEntrada = DateTime.Now;

                    notaCompra = new NotaFiscalCompra
                    {
                        Chave = chave,
                        Numero = numeroDocumento,
                        Serie = Convert.ToInt16(serieDocumento),
                        DataEmissao = emissao,
                        DataEntrada = dataEntrada,
                        Situacao = ESituacaoNotaFiscalCompra.Construcao
                    };

                    XmlNodeList nodoEmitente = xml.GetElementsByTagName("emit");
                    if (nodoEmitente.Count > 0)
                    {

                        var pessoaCnpj = nodoEmitente[0]["CNPJ"].InnerText;
                        var nome = nodoEmitente[0]["xNome"].InnerText;

                        notaCompra.Cnpj = pessoaCnpj;
                        notaCompra.NomeFornecedor = nome;
                    }

                    var itens = MontarItensNotaCompraComXml(xml.GetElementsByTagName("det"), notaCompra.Id);

                    if (itens.Any())
                        notaCompra.Itens = itens;
                }
            }

            return notaCompra;
        }

        public static List<NotaFiscalCompraItem> MontarItensNotaCompraComXml(XmlNodeList xmlItens, int notaCompraId)
        {
            List<NotaFiscalCompraItem> itens = new List<NotaFiscalCompraItem>();
            foreach (XmlNode nodoItemNota in xmlItens)
            {
                var i = 0;

                var codigoItem = nodoItemNota["prod"]["cProd"].InnerText;
                var quantidade = decimal.Parse(nodoItemNota["prod"]["qCom"].InnerText);
                var descricao = nodoItemNota["prod"]["xProd"].InnerText.ToUpper();

                string codigoBarras = string.Empty;
                string ncm = string.Empty;
                string cest = string.Empty;

                if (nodoItemNota["prod"]["cEAN"] != null)
                    codigoBarras = nodoItemNota["prod"]["cEAN"].InnerText;

                if (nodoItemNota["prod"]["NCM"] != null)
                    ncm = nodoItemNota["prod"]["NCM"].InnerText;

                if (nodoItemNota["prod"]["CEST"] != null)
                    cest = nodoItemNota["prod"]["CEST"].InnerText;

                var valorTotal = decimal.Parse(nodoItemNota["prod"]["vProd"].InnerText);
                var item = new NotaFiscalCompraItem
                {
                    Id = i++,
                    CodigoMercadoriaImportada = codigoItem,
                    CodigoBarrasMercadoriaImportada = codigoBarras,
                    DescricaoMercadoriaImportada = descricao,
                    Quantidade = quantidade,
                    TotalMercadoria = valorTotal,
                    NotaFiscalCompraId = notaCompraId,
                    PrecoCusto = Math.Round(valorTotal / quantidade, 8),
                    Cest = cest,
                    Ncm = ncm
                };

                itens.Add(item);
            }

            return itens;

        }

        public static string RetornarTag(XmlDocument arquivoXml, string tag)
        {
            var result = string.Empty;

            var nodo = arquivoXml.GetElementsByTagName(tag);
            if (nodo.Count > 0)
                result = nodo[0].InnerText;

            return result;
        }

        public static NotaFiscalCompra AtualizarValorNota(int notaCompraId)
        {
            using (var contexto = new ArgoMiniContext())
            {
                var notaCompra = contexto.NotaFiscalCompra.Include(c => c.Itens).First(c => c.Id == notaCompraId);

                notaCompra.ValorTotalNota = notaCompra.Itens.Sum(c => c.TotalMercadoria);

                contexto.Entry(notaCompra).State = EntityState.Modified;
                contexto.SaveChanges();

                return notaCompra;
            }
        }

        public static void SalvarNotaCompra(NotaFiscalCompra notaFiscalCompra)
        {
            using (var contexto = new ArgoMiniContext())
            {
                using (var contextoTransaction = contexto.Database.BeginTransaction())
                {
                    try
                    {
                        notaFiscalCompra.Situacao = ESituacaoNotaFiscalCompra.Importada;
                        contexto.NotaFiscalCompra.Add(notaFiscalCompra);
                        contexto.SaveChanges();
                        contextoTransaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        contextoTransaction.Rollback();
                    }
                }


            }
        }
    }
}