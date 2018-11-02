using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using ArgoMini.Models;
using ArgoMini.Negocio.Impressora;

namespace ArgoMini.Negocio
{
    public class ImpressoraNegocio
    {

        public bool ExisteImpressoraSerial(string serialHd)
        {
            using (var contexto = new ArgoMiniContext())
            {
                
                   var impressora = contexto.Impressoras.FirstOrDefault(c => c.SerialHd == serialHd);

                return impressora != null;
            }
        }

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

        public void TestarConexao()
        {

            var stringao =
                $"Argo Sistemas{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão" +
                $"{Environment.NewLine}Teste de impressão{Environment.NewLine}Teste de impressão{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}|BARCODE|";

            var nomeImpressora = BuscarImpressoras();


            var escolhida = nomeImpressora.First(c=> c.Equals("ELGIN i9(USB)"));

            this.TestarImpressao(escolhida, stringao);


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

        public List<string> BuscarImpressoras()
        {
            var impressorasInstaladas = PrinterSettings.InstalledPrinters;

            if (impressorasInstaladas.Count > 0)
            {
                return CarregarCombo();
            }

            return new List<string>();
        }

        public List<string> CarregarCombo()
        {
            var impressoras = new List<string>();
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                impressoras.Add(installedPrinter);
            }

            return impressoras;
        }
    }
}