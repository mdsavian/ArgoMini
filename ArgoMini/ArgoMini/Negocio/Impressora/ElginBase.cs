using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgoMini.Negocio.Impressora
{
    public class ElginBase
    {
        protected readonly bool ThrowExceptionAoFalharComando;

        protected ElginBase(ArgoMini.Models.Impressora impressora, int caracteresPorLinha, Encoding encoding, bool throwExceptionAoFalharComando = false)
        {
            this.CaracteresPorLinha = caracteresPorLinha;
            this.Encoding = encoding;
            ImpressoraComunicacao = impressora;
            this.AbrirComunicacao(impressora.Descricao);
            ThrowExceptionAoFalharComando = throwExceptionAoFalharComando;
        }

        public Models.Impressora ImpressoraComunicacao { get; }

        public int CaracteresPorLinha { get; private set; }
        public int CaracteresExpandidosPorLinha => 21;

        protected Encoding Encoding { get; private set; }

        public void AbrirComunicacao(string nomeImpressora)
        {

        }

        public void FecharComunicacao()
        {
            //ImpressoraNfceNegocio.AtualizarStatusImprimindo(ImpressoraComunicacao, false);
        }

        public bool TestarComunicacao()
        {
            return ElginHelper.LeStatus(this.ImpressoraComunicacao.Descricao);
        }

        public string Status()
        {
            if (ElginHelper.LeStatus(this.ImpressoraComunicacao.Descricao))
            {
                return "Impressora ONLINE";
            }
            else
            {
                return "Erro de comunicação com a impressora.";
            }
        }

        public virtual void AcionarGuilhotina()
        {
            try
            {
                ElginHelper.LineFeed(this.ImpressoraComunicacao.Descricao, 8);
                ElginHelper.CutPaper(this.ImpressoraComunicacao.Descricao);
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void AbrirGaveta()
        {
            try
            {
                ElginHelper.DrawerKick(this.ImpressoraComunicacao.Descricao);
            }
            catch (Exception ex)
            {
                //Log.Write.Error($"Falha ao abrir a gaveta: {ex}");

                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void ConfigurarPadroesDeImpressao()
        {

        }

        public virtual void Imprimir(string texto)
        {
            try
            {
                //"\x1B\x21\x01" Texto condensado "\x1B\x33\x10" Menor espaçamento entre linhas.
                texto = $"\x1B\x21\x01\x1B\x33\x10{(texto.Replace("<b>", "\x1B\x45\x01").Replace("</b>", "\x1B\x45\x00"))}";
                texto = texto.Replace("<e>", "\x1B\x21\x80").Replace("</e>", "\x1B\x21\x00\x1B\x21\x01");

                // Texto Condensado
                //ElginHelper.CharFontBText(this.ImpressoraComunicacao.Descricao);
                ElginHelper.PrintText(this.ImpressoraComunicacao.Descricao, texto, this.Encoding);
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void ImprimirComBarra(string texto, string codigoBarras)
        {
            try
            {
                var blocoTexto = new StringBuilder();
                var lines = texto.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (var line in lines)
                {
                    if (line.Equals("|BARCODE|"))
                    {
                        this.Imprimir(blocoTexto.ToString());
                        blocoTexto.Clear();
                        this.ImprimirBarras(codigoBarras);
                    }
                    else
                    {
                        blocoTexto.AppendLine(line);
                    }
                }

                this.Imprimir(blocoTexto.ToString());
                blocoTexto.Clear();

                this.AcionarGuilhotina();
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        private void ImprimirBarras(string barras)
        {
            ElginHelper.BarcodeHeight(this.ImpressoraComunicacao.Descricao, 50);
            ElginHelper.BarcodeWidth(this.ImpressoraComunicacao.Descricao, 1);
            ElginHelper.barcodeHRI_chars(this.ImpressoraComunicacao.Descricao, 1);
            ElginHelper.BarcodeHriPostion(this.ImpressoraComunicacao.Descricao, 0);
            ElginHelper.CharFontAText(this.ImpressoraComunicacao.Descricao);
            ElginHelper.SelectJustification(this.ImpressoraComunicacao.Descricao, 1);
            ElginHelper.PrintBarcode128(this.ImpressoraComunicacao.Descricao, barras);
            ElginHelper.LineFeed(this.ImpressoraComunicacao.Descricao, 1);
        }

        public virtual void ImprimirNfce(string nfce, string url, string assinatura)
        {
            try
            {
                //var opcpesGlobais = OpcoesGlobaisNegocio.CarregarRegistroUnico();
                var blocoTexto = new StringBuilder();
                var lines = nfce.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                var shotUrl = url;
                if (!string.IsNullOrWhiteSpace(shotUrl))
                {
                    url = shotUrl;
                }

                foreach (var line in lines)
                {
                    if (line.Equals("|QRCODE|"))
                    {
                        this.Imprimir(blocoTexto.ToString());
                        blocoTexto.Clear();
                        //if (OpcoesGlobaisNegocio.CarregarRegistroUnico().ImprimeQrCode)
                        //{
                        //    ElginHelper.PrintQrcode(url, this.ImpressoraComunicacao.Descricao);
                        //}
                    }
                    else
                    {
                        blocoTexto.AppendLine(line);
                    }
                }

                this.Imprimir(blocoTexto.ToString());
                blocoTexto.Clear();

                this.AcionarGuilhotina();
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void ImprimirSatCfe(string satCfe, string textoQrCode, string chaveCfe, string assinatura)
        {
            try
            {
                //var opcpesGlobais = OpcoesGlobaisNegocio.CarregarRegistroUnico();
                var blocoTexto = new StringBuilder();
                var lines = satCfe.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (var line in lines)
                {
                    if (line.Equals("|QRCODE|"))
                    {
                        this.Imprimir(blocoTexto.ToString());
                        blocoTexto.Clear();
                        ElginHelper.PrintQrcode(textoQrCode, this.ImpressoraComunicacao.Descricao);
                    }
                    else if (line.Equals("|BARCODE|"))
                    {
                        this.Imprimir(blocoTexto.ToString());
                        blocoTexto.Clear();
                        this.ImprimirBarras(chaveCfe);
                    }
                    else
                    {
                        blocoTexto.AppendLine(line);
                    }
                }

                this.Imprimir(blocoTexto.ToString());
                blocoTexto.Clear();

                this.AcionarGuilhotina();
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void ImprimirNfse(string nfse, string chaveNfse, string linkQrcode, string assinatura)
        {
            this.ImprimirNfce(nfse, linkQrcode, assinatura);
        }

        public void ImprimirRelatorioOuComprovante(string texto, int alinhamento = 0)
        {
            try
            {
                this.Imprimir(texto);
                this.AcionarGuilhotina();
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void ImprimirTextos(List<string> textos)
        {
            try
            {
                if (textos.Any())
                {
                    foreach (var texto in textos)
                    {
                        this.Imprimir(texto);
                        this.AcionarGuilhotina();
                    }
                }
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public void Dispose()
        {
            this.FecharComunicacao();
        }
    }


}
