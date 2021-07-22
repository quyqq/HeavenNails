using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Printing;
using System.Windows.Forms;

namespace HeavenNails.Codes
{
    public static class Functions
    {
        public static List<string> GetNetworkComputerNames()
        {
            List<string> NameList = new List<string>();
            DirectoryEntry root = new DirectoryEntry("WinNT:");
            foreach (DirectoryEntry computers in root.Children)
            {
                foreach (DirectoryEntry computer in computers.Children)
                {
                    NameList.Add(computer.Name);
                }
            }
            return NameList;
        }
        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string CutString(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;
            return source.Substring(start, len);
        }

        public static bool CheckPrinter()
        {
            PrintServer myPrintServer = new PrintServer();
            PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
            foreach (PrintQueue pq in myPrintQueues)
            {
                if (pq.Name == Properties.Settings.Default.RecieptPrinter)
                {
                    
                    return Codes.Functions.SpotTroubleUsingQueueAttributes(pq);
                }
            }
            MessageBox.Show("There is no printer :" + Properties.Settings.Default.RecieptPrinter, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        private static bool SpotTroubleUsingQueueAttributes( PrintQueue pq)
        {
            string statusReport = "";
            if ((pq.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem)
            {
                statusReport = statusReport + "Has a paper problem. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner)
            {
                statusReport = statusReport + "Is out of toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen)
            {
                statusReport = statusReport + "Has an open door. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error)
            {
                statusReport = statusReport + "Is in an error state. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable)
            {
                statusReport = statusReport + "Is not available. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
            {
                statusReport = statusReport + "Is off line. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory)
            {
                statusReport = statusReport + "Is out of memory. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut)
            {
                statusReport = statusReport + "Is out of paper. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull)
            {
                statusReport = statusReport + "Has a full output bin. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam)
            {
                statusReport = statusReport + "Has a paper jam. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.Paused) == PrintQueueStatus.Paused)
            {
                statusReport = statusReport + "Is paused. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.TonerLow) == PrintQueueStatus.TonerLow)
            {
                statusReport = statusReport + "Is low on toner. ";
            }
            if ((pq.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
            {
                statusReport = statusReport + "Needs user intervention. ";
            }
            if (statusReport.Length > 0)
            {
                MessageBox.Show("Printer status: "+statusReport, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }
    }
    public class RawPrinterHelper

    {

        // Structure and API declarions:

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA

        {

            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;

            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;

            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;

        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true,
        CharSet = CharSet.Ansi, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]

        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)]string szPrinter, out IntPtr hPrinter, long pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true,
        CharSet = CharSet.Ansi, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level,
        [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32
        dwCount, out Int32 dwWritten);

        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        public static extern Int32 GetLastError();

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes,
        Int32 dwCount)

        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "SendBytes";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))

            {

                if (StartDocPrinter(hPrinter, 1, di))

                {

                    if (StartPagePrinter(hPrinter))

                    {

                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);

                        EndPagePrinter(hPrinter);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)

            {

                dwError = GetLastError();

            }

            return bSuccess;

        }

        public static bool SendFileToPrinter(string szPrinterName, string
        szFileName)

        {

            FileStream fs = new FileStream(szFileName, FileMode.Open);

            BinaryReader br = new BinaryReader(fs);

            Byte[] bytes = new Byte[fs.Length];

            bool bSuccess = false;

            IntPtr pUnmanagedBytes = new IntPtr(0);

            int nLength;

            nLength = Convert.ToInt32(fs.Length);

            bytes = br.ReadBytes(nLength);

            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);

            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);

            Marshal.FreeCoTaskMem(pUnmanagedBytes);

            return bSuccess;

        }

        public static bool SendStringToPrinter(string szPrinterName, string
        szString)

        {

            IntPtr pBytes;

            Int32 dwCount;

            dwCount = szString.Length;

            pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            SendBytesToPrinter(szPrinterName, pBytes, dwCount);

            Marshal.FreeCoTaskMem(pBytes);

            return true;

        }

        public static bool OpenCashDrawer(string szPrinterName)

        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false;

            di.pDocName = "OpenDrawer";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))

            {

                if (StartDocPrinter(hPrinter, 1, di))

                {

                    if (StartPagePrinter(hPrinter))

                    {

                        int nLength;
                        //Cut paper 27, 109
                        byte[] DrawerOpen = new byte[] { 27, 112, 48, 55, 121 };

                        nLength = DrawerOpen.Length;

                        IntPtr p = Marshal.AllocCoTaskMem(nLength);

                        Marshal.Copy(DrawerOpen, 0, p, nLength);

                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);

                        EndPagePrinter(hPrinter);

                        Marshal.FreeCoTaskMem(p);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)

            {

                dwError = GetLastError();

            }

            return bSuccess;

        }
        public static bool CutPaper(string szPrinterName)

        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false;

            di.pDocName = "CutPaper";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))

            {

                if (StartDocPrinter(hPrinter, 1, di))

                {

                    if (StartPagePrinter(hPrinter))

                    {

                        int nLength;
                        //Cut paper 27, 109
                        byte[] DrawerOpen = new byte[] { 27, 109 };

                        nLength = DrawerOpen.Length;

                        IntPtr p = Marshal.AllocCoTaskMem(nLength);

                        Marshal.Copy(DrawerOpen, 0, p, nLength);

                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);

                        EndPagePrinter(hPrinter);

                        Marshal.FreeCoTaskMem(p);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)

            {

                dwError = GetLastError();

            }

            return bSuccess;

        }

        public static bool FullCut(string szPrinterName)

        {

            Int32 dwError = 0, dwWritten = 0;

            IntPtr hPrinter = new IntPtr(0);

            DOCINFOA di = new DOCINFOA();

            bool bSuccess = false;

            di.pDocName = "FullCut";

            di.pDataType = "RAW";

            if (OpenPrinter(szPrinterName, out hPrinter, 0))

            {

                if (StartDocPrinter(hPrinter, 1, di))

                {

                    if (StartPagePrinter(hPrinter))

                    {

                        int nLength;

                        byte[] DrawerOpen = new byte[] { 27, 100, 51, 27, 109 };

                        nLength = DrawerOpen.Length;

                        IntPtr p = Marshal.AllocCoTaskMem(nLength);

                        Marshal.Copy(DrawerOpen, 0, p, nLength);

                        bSuccess = WritePrinter(hPrinter, p, DrawerOpen.Length, out dwWritten);

                        EndPagePrinter(hPrinter);

                        Marshal.FreeCoTaskMem(p);

                    }

                    EndDocPrinter(hPrinter);

                }

                ClosePrinter(hPrinter);

            }

            if (bSuccess == false)

            {

                dwError = GetLastError();

            }

            return bSuccess;

        }

    }

}
