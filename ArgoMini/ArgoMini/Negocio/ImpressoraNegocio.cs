using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ArgoMini.Negocio
{
    public class ImpressoraNegocio
    {

        public void Imprimir(string texto)
        {
            try
            {
                var linhas = new LinkedList<string>();

                    //Lines(texto);

                if (linhas.Count > 200)
                {
                    var parteTexto = new StringBuilder();
                    var quantidadeProcessada = 0;
                    foreach (var linha in linhas)
                    {
                        parteTexto.AppendLine(linha);
                        quantidadeProcessada++;

                        if (quantidadeProcessada < 200)
                            continue;

                        this.ImprimirTexto(parteTexto.ToString());
                        parteTexto.Clear();
                        quantidadeProcessada = 0;
                    }

                    this.ImprimirTexto(parteTexto.ToString());
                }
                else
                {
                    this.ImprimirTexto(texto);
                }
            }
            catch (Exception)
            {
                //if (_throwExceptionAoFalharComando)
                //    throw;
            }
        }

        public void AbrirComunicacao(string enderecoIp)
        {
            InterfaceEpsonNf.IniciaPorta(string.IsNullOrWhiteSpace(enderecoIp) ? "USB" : enderecoIp);
        }

        public void FecharComunicacao()
        {
            InterfaceEpsonNf.FechaPorta();
            //ImpressoraNfceNegocio.AtualizarStatusImprimindo(ImpressoraComunicacao, false);
        }

        public bool TestarComunicacao()
        {
            var status = InterfaceEpsonNf.Le_Status();
            switch (status)
            {
                case 0:
                    return false;
                case 5:
                    return true;
                case 9:
                    return false;
                case 24:
                    return true;
                case 32:
                    return false;
            }

            return false;
        }

        public string Status()
        {
            var status = InterfaceEpsonNf.Le_Status();

            switch (status)
            {
                case 0:
                    return "Erro de comunicação com a impressora.";
                case 5:
                    return "Impressora com pouco papel.";
                case 9:
                    return "Impressora com a tampa aberta.";
                case 24:
                    return "Impressora ONLINE";
                case 32:
                    return "Impressora sem papel.";
            }

            return "Não foi possível ler o status da impressora.";
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
                        this.ImprimirBarCode(codigoBarras);
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
                //if (_throwExceptionAoFalharComando)
                    throw;
            }
        }

        public void AcionarGuilhotina()
        {
            try
            {
                // Tag <sl> salto de linha
                // Tag <gui> guilhotinar
                InterfaceEpsonNf.ImprimeTextoTag("<sl>02</sl><gui></gui>");
            }
            catch (Exception)
            {
                //if (_throwExceptionAoFalharComando)
                //    throw;
            }
        }

        private void ImprimirBarCode(string barCode)
        {
            if (barCode.Length <= 22)
            {
                InterfaceEpsonNf.ConfiguraCodigoBarras(75, 1, 0, 0, 12);
            }
            else
            {
                InterfaceEpsonNf.ConfiguraCodigoBarras(75, 0, 0, 1, 12);
            }

            InterfaceEpsonNf.ImprimeCodigoBarrasCODE128(barCode);
        }

        private void ImprimirTexto(string texto)
        {
            try
            {
                // a Tag <c> serve para condensar o texto.
                var textoFormatado = $"<cespl>38</cespl><c>{texto}</c>";
                InterfaceEpsonNf.ImprimeTextoTag(textoFormatado);
            }
            catch (Exception)
            {
                //if (_throwExceptionAoFalharComando)
                //    throw;
            }
        }

        public static LinkedList<string> Lines(StringReader text)
        {
            string line;
            var lines = new LinkedList<string>();

            while ((line = text.ReadLine()) != null)
            {
                lines.AddLast(line);
            }

            return lines;
        }
    }
}