using System;
using System.Threading;

namespace ArgoMini.Negocio.Impressora
{
    public class ElginI9 : ElginBase
    {
        private static int _caracteresPorLinha = 64;

        public ElginI9(Models.Impressora impressora, bool throwExceptionAoFalharComando = false): base(impressora, _caracteresPorLinha, System.Text.Encoding.GetEncoding("IBM860"), throwExceptionAoFalharComando)
        {

        }

        public override void AcionarGuilhotina()
        {
            try
            {
                // #####NÃO REMOVER#####
                // Necessário para não acavalar as impressões.
                Thread.Sleep(1500);
                ElginHelper.LineFeed(this.ImpressoraComunicacao.Descricao, 10);
                ElginHelper.CutPaper(this.ImpressoraComunicacao.Descricao);
            }
            catch (Exception)
            {
                if (ThrowExceptionAoFalharComando)
                    throw;
            }
        }

        public override void ImprimirNfce(string nfe, string chaveNfe, string assinatura)
        {
            this.ImprimirLogo();
            base.ImprimirNfce(nfe, chaveNfe, assinatura);
        }

        private void ImprimirLogo()
        {
            //if (App.UnidadeDeNegocio.OpcoesAvancadas.ImprimirLogo && App.UnidadeDeNegocio.OpcoesAvancadas.LogoDocumentosEletronicos != null)
            //{
            //    try
            //    {
            //        // Imprime o logo salvo em memória na impressora.
            //        ElginHelper.PrintLogo(this.ImpressoraComunicacao.Descricao);
            //    }
            //    catch (Exception ex) { }
            //}
        }
    }
}
