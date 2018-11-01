using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using ArgoMini.Negocio.Impressora;

namespace ArgoMini.Negocio
{
    public class ImpressoraNegocio
    {

        private void ArgoButtonBuscarSerialHd_Click(object sender, EventArgs e)
        {
            var serial = this.BuscarSerialDiscoLocalC();
        }


        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        public string BuscarSerialDiscoLocalC()
        {
            try
            {
                using (var disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\""))
                {
                    disk.Get();
                    var serial = disk["VolumeSerialNumber"].ToString();

                    return serial;
                }
            }
            catch (Exception e)
            {
                //Log.Write.Info($"Erro ao buscar Serial HD: {e.MensagemErroParaLog()}");
                return "";
            }
        }

        private void TestarConexao()
        {

            var stringao =
                $"Argo Sistemas{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}|BARCODE|";

            var nomeImpressora = BuscarImpressoraClick();

            this.TestarImpressao(nomeImpressora, stringao);
        }

        public void TestarImpressao(string nome, string texto)
        {

            var impressora = new Models.Impressora
            {
                Descricao = "teste",
                SerialHd = this.BuscarSerialDiscoLocalC(),
                DataHoraUltimaImpressao = DateTime.Now,
                Imprimindo = false,
                Nome = nome
            };

            var barra = "1234567890123456789012";


            var elginI9 = new ElginI9(impressora, true);
            elginI9.ImprimirComBarra(texto, barra);
        }

        public string BuscarImpressoraClick()
        {
            var impressorasInstaladas = PrinterSettings.InstalledPrinters;

            if (impressorasInstaladas.Count > 0)
            {
                return CarregarCombo();
            }
            else
            {
                
            }

            return string.Empty;

        }

        public string CarregarCombo()
        {
            var impressoras = new List<string>();
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                impressoras.Add(installedPrinter);
            }

            return string.Empty;
        }
    }
}