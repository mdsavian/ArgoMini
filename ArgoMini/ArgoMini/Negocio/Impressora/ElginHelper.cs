using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgoMini.Negocio.Impressora
{
    internal static class ElginHelper
    {
        #region Dll imports

        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        #endregion

        //if you want to use decimal values in your methods.
        public static byte[] IntTobyte(int[] data)
        {

            byte[] byteData = data.Select(x => (byte)x).ToArray(); // coonvert int array to byte
            return byteData;
        }



        //initialize printer

        //ESC @
        public static void InitializePrinter(string szPrinterName)
        {
            int[] command = { 27, 64 };
            SendBytesToPrinter(szPrinterName, IntTobyte(command));
        }


        //print text

        public static bool PrintText(string szPrinterName, string data, Encoding encoding)
        {
            SendBytesToPrinter(szPrinterName, encoding.GetBytes(data));

            return true;
        }

        // Print Position Commands
        //ESC a n
        public static bool SelectJustification(string szPrinterName, int justificationCode)
        {
            //0= default 
            //48 left
            //1,49 centering
            //2,50 right

            int[] align = { 27, 97, justificationCode };
            SendBytesToPrinter(szPrinterName, IntTobyte(align));

            return true;
        }

        //Character Control Commands

        //use this mode to cancel another mode.
        public static bool NormalModeText(string szPrinterName)
        {
            int[] normal = { 27, 33, 0 };
            SendBytesToPrinter(szPrinterName, IntTobyte(normal));

            return true;
        }

        public static bool LeStatus(string szPrinterName)
        {
            int[] normal = { 16, 4, 1 };
            return SendBytesToPrinter(szPrinterName, IntTobyte(normal));
        }

        //Character font A (12 × 24) selected.
        public static bool CharFontAText(string szPrinterName)
        {
            int[] fontA = { 27, 33, 0 };
            SendBytesToPrinter(szPrinterName, IntTobyte(fontA));

            return true;
        }

        public static bool CharFontBText(string szPrinterName)
        {
            int[] fontB = { 27, 33, 1 };
            SendBytesToPrinter(szPrinterName, IntTobyte(fontB));

            return true;
        }

        //Emphasized mode is turned on
        public static bool EmphasizedModeText(string szPrinterName)
        {
            int[] mode = { 27, 33, 8 };
            SendBytesToPrinter(szPrinterName, IntTobyte(mode));
            return true;
        }

        //Double-height selected.
        public static bool DoubleHeightText(string szPrinterName)
        {
            int[] height = { 27, 33, 16 };
            SendBytesToPrinter(szPrinterName, IntTobyte(height));

            return true;
        }

        //Double-width selected.
        public static bool DoubleWidthText(string szPrinterName)
        {
            int[] width = { 27, 33, 32 };
            SendBytesToPrinter(szPrinterName, IntTobyte(width));
            return true;
        }


        //Underline mode is turned on
        public static bool UnderlineModeText(string szPrinterName)
        {
            int[] underline = { 27, 33, 128 };
            SendBytesToPrinter(szPrinterName, IntTobyte(underline));
            return true;
        }


        //print and Line feed
        public static bool LineFeed(string szPrinterName, int numLines)
        {
            // fucntion LF 
            int[] lf = { 10 };
            for (int i = 1; i <= numLines; i++)
            {
                SendBytesToPrinter(szPrinterName, IntTobyte(lf));
            }

            return true;
        }

        //Generate pulse in Real Time
        public static bool DrawerKick(string szPrinterName)
        {
            // function DLE DC4 fn m t （fn=1）

            int[] pulse = { 27, 112, 0, 100, 200 };
            SendBytesToPrinter(szPrinterName, IntTobyte(pulse));

            return true;
        }

        //execute test print
        public static bool TestPrint(string szPrinterName)
        {
            //function GS ( A pL pH n m

            int[] test = { 29, 40, 65, 2, 0, 0, 2 };
            SendBytesToPrinter(szPrinterName, IntTobyte(test));

            return true;
        }

        //Select an international character set
        public static bool CharSet(string szPrinterName, int language)
        {
            //function ESC R n
            //0-USA
            //12-Latin America
            //
            int[] charSet = { 27, 82, language };

            SendBytesToPrinter(szPrinterName, IntTobyte(charSet));

            return true;
        }


        //select character code table
        public static bool CodeTable(string szPrinterName, int language)
        {
            //function Esc t n
            // 0 - PC437 (USA: Standard Europe)]
            // 40 [ISO8859-15 (Latin9)]
            // 3 [PC860 (Portuguese)]

            int[] code = { 27, 116, language };
            SendBytesToPrinter(szPrinterName, IntTobyte(code));

            return true;
        }

        //Select cut mode and cut paper
        public static bool CutPaper(string szPrinterName)
        {
            //hex 1D 56 m, m =0,1,48,49
            int[] cut = { 29, 86, 0 };
            SendBytesToPrinter(szPrinterName, IntTobyte(cut));

            return true;
        }


        //activate printer buzzer
        public static bool Buzzer(string szPrinterName)
        {
            //hex data = "1b 28 41 05 00 61 64 03 0a 0a";
            int[] buzzer = { 27, 40, 65, 5, 0, 97, 100, 3, 10, 10 };

            SendBytesToPrinter(szPrinterName, IntTobyte(buzzer));
            // RawPrinterHelper.SendASCiiToPrinter(szPrinterName, data);
            return true;
        }

        //*************************** barcode  commands **********************************

        //GS h n - sets bar the height of bar code to n dots.
        public static bool BarcodeHeight(string szPrinterName, int range = 162) // default = 162
        {

            //range 1 ≤ n ≤ 255
            int[] height = { 29, 104, range };
            SendBytesToPrinter(szPrinterName, IntTobyte(height));
            return true;
        }

        // GS w n Set bar code width
        public static bool BarcodeWidth(string szPrinterName, int range = 3)
        {
            //range = 2 ≤ n ≤ 6
            int[] width = { 29, 119, range };
            SendBytesToPrinter(szPrinterName, IntTobyte(width));
            return true;
        }

        //GS f n Select a font for the HRI characters when printing a bar code.
        public static bool barcodeHRI_chars(string szPrinterName, int fontCode = 0)
        {
            //default 0 
            //[Range] n = 0, 1, 48, 49
            int[] hri = { 29, 102, fontCode };

            SendBytesToPrinter(szPrinterName, IntTobyte(hri));
            return true;
        }


        //GS H n Select print position of HRI characters
        public static bool BarcodeHriPostion(string szPrinterName, int positionCode = 1)
        {
            //default = 0
            //[Range] 0 ≤ n ≤ 3, 48 ≤ n ≤ 51 

            int[] printPosition = { 29, 72, positionCode };

            SendBytesToPrinter(szPrinterName, IntTobyte(printPosition));
            return true;
        }

        //GS k Print barcode
        //<Function A>
        public static bool PrintBarcode(string szPrinterName, string data, int type = 2)
        {
            //for this example 2 = JAN/EAN13
            int[] barcode = { 29, 107, type };
            SendBytesToPrinter(szPrinterName, IntTobyte(barcode));

            SendStringToPrinter(szPrinterName, data);
            int[] nul = { 0 }; // null char at the end.
            SendBytesToPrinter(szPrinterName, IntTobyte(nul));
            return true;
        }

        //GS k Print Barcode
        // <Function B>
        public static bool PrintBarcode128(string szPrinterName, string data)
        {
            if (!data.StartsWith("{A"))
                data = $"{{A{data}";

            int size = data.Length; //  the number of bytes of bar code data
            int[] barcode = { 29, 107, 73, size };
            SendBytesToPrinter(szPrinterName, IntTobyte(barcode));
            SendStringToPrinter(szPrinterName, data);

            return true;
        }

        //*************************** barcode  commands **********************************

        //function to print Qrcode
        public static bool PrintQrcode(string strdata, string szPrinterName)
        {
            int length = strdata.Length + 3; //  string size  + 3
            //int length = Strdata.Length; 
            byte lengthLowByte = 0, lengthHighByte = 0;
            lengthLowByte = (byte)(length & 0xff); //low byte used in function 180 
            lengthHighByte = (byte)((length >> 8) & 0xff); //high byte in function 180 

            //if you don't want to use shift operator:
            //int length_low_byte = length % 256;
            //int length_high_byte = length / 256;
            InitializePrinter(szPrinterName);

            //<Function ESC a n> Select justification  
            int[] escAn = { 27, 97, 1 };
            SendBytesToPrinter(szPrinterName, IntTobyte(escAn));

            //<Function GS L> Set left margin
            int[] fGsl = { 29, 76, 0, 0 };
            SendBytesToPrinter(szPrinterName, IntTobyte(fGsl));

            //<Function 165> GS ( k p L p H cn fn n (cn = 49,fn = 65)  QR Code: Select the model
            int[] f165 = { 29, 40, 107, 4, 0, 49, 65, 50, 0 };
            SendBytesToPrinter(szPrinterName, IntTobyte(f165));

            //<Function 167> GS ( k pL pH cn fn n (cn = 49, fn = 67) QR Code: Set the size of module
            int[] f167 = { 29, 40, 107, 3, 0, 49, 67, 6 }; //  size of qrcode:  1-16
            SendBytesToPrinter(szPrinterName, IntTobyte(f167));

            //<Function 169> GS ( k pL pH cn fn n (cn = 49, fn = 69) QR Code: Select the error correction level
            int[] f169 = { 29, 40, 107, 3, 0, 49, 69, 48 };
            SendBytesToPrinter(szPrinterName, IntTobyte(f169));

            //<Function 180> GS ( k pL pH cn fn m d1…dk (cn = 49, fn = 80) QR Code: Store the data in the symbol storage area
            //pL and pH are the low- and high-order bytes of a 16-bit integer value that specifies the length in bytes of the following data  

            int[] f180 = { 29, 40, 107, lengthLowByte, lengthHighByte, 49, 80, 48 };
            SendBytesToPrinter(szPrinterName, IntTobyte(f180));

            //send string/url to printer
            //RawPrinterHelper.SendASCiiToPrinter(szPrinterName, Strdata);
            SendStringToPrinter(szPrinterName, strdata);

            //<Function 181> GS ( k pL pH cn fn m (cn = 49, fn = 81) QR Code: Print the symbol data in the symbol storage area
            int[] f181 = { 29, 40, 107, 3, 0, 49, 81, 48 };
            SendBytesToPrinter(szPrinterName, IntTobyte(f181));

            return true;
        }

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the printer queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
        {
            int dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "i9 RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            byte[] bytes = new byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        public static bool SendBytesToPrinter(string szPrinterName, byte[] data)
        {
            bool retval = false;

            IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(data.Length); // Allocate unmanaged memory
            Marshal.Copy(data, 0, pUnmanagedBytes, data.Length); // copy bytes into unmanaged memory
            retval = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, data.Length);//Send bytes to printer

            Marshal.FreeCoTaskMem(pUnmanagedBytes); // Free the allocated unmanaged memory
            return retval;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            int dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        //if you want a wrapper function for you strings :
        public static bool SendASCiiToPrinter(string szPrinterName, string data)
        {
            bool retval = false;

            //if  you are using UTF-8 and get wrong values in qrcode printing, you must use ASCII instead.
            //retval = SendBytesToPrinter(szPrinterName, Encoding.UTF8.GetBytes(data));

            retval = SendBytesToPrinter(szPrinterName, Encoding.ASCII.GetBytes(data));

            return retval;
        }

        public static bool PrintLogo(string szPrinterName)
        {
            //function GS ( pL pH m fn kc1 kc2 x y (fn = 69)

            int[] printLogoNv00 = { 29, 40, 76, 6, 0, 48, 69, 48, 48, 1, 1 };
            SendBytesToPrinter(szPrinterName, IntTobyte(printLogoNv00));

            return true;
        }
    }
}