using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using HeavenNails.Codes;
using HeavenNails.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Printing;
using System.Printing;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using ZXing;
using static HeavenNails.Codes.PrinterSettingFuntions;



namespace HeavenNails
{
    delegate void AddrowCallBack(object CodeSC);
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
       private SerialPort port;
        private Thread MainScannerThead;
        public MainForm()
        {
            InitializeComponent();

        }



        private void accordionControlElementExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void accordionControlElementServices_Click(object sender, EventArgs e)
        {
            PrimaryGoals.Services frm = new PrimaryGoals.Services();
            frm.ShowDialog();
            pLinqInstantFeedbackSourceService.Refresh();

            DataTable TBLRC = (DataTable)gridControlReceiptDetails.DataSource;
            TBLRC.Rows.Clear();
            simpleButtonDel_Click(null, null);
            textEditTotal.EditValue = 0;
        }

        private void accordionControlElementItem_Click(object sender, EventArgs e)
        {
            PrimaryGoals.Items frm = new PrimaryGoals.Items();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            gridControlServies.DataSource = pLinqInstantFeedbackSourceService;





            // Name CostOrPrice import_price Qindex Sindex Type
            DataTable TableReceiptDetail = new DataTable("ReceptDetails");
            List<DataColumn> ListColums = new List<DataColumn>();
            ListColums.Add(new DataColumn("Type", typeof(string)));
            ListColums.Add(new DataColumn("QIIndex", typeof(decimal)));
            ListColums.Add(new DataColumn("SIndex", typeof(decimal)));
            ListColums.Add(new DataColumn("Name", typeof(string)));
            ListColums.Add(new DataColumn("ImportPrice", typeof(decimal)));
            ListColums.Add(new DataColumn("CostOrPrice", typeof(decimal)));
            foreach (DataColumn CL in ListColums)
            {
                TableReceiptDetail.Columns.Add(CL);
            }
            TableReceiptDetail.RowDeleted += new DataRowChangeEventHandler(Row_Changed);
            TableReceiptDetail.RowChanged += new DataRowChangeEventHandler(Row_Changed);
            gridControlReceiptDetails.DataSource = TableReceiptDetail;
            gridViewRecieptDetails.ExpandAllGroups();



            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.FieldName = "CostOrPrice";
            //item.ShowInGroupColumnFooter = gridViewRecieptDetails.Columns["CostOrPrice"];
            //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //item.DisplayFormat = "Sum = {0:c2}";
            //gridViewRecieptDetails.GroupSummary.Add(item);

            InitializeMainScanner();
        }

        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            DataTable TBLRC = (DataTable)gridControlReceiptDetails.DataSource;
            if (TBLRC.Rows.Count > 0)
                this.textEditTotal.EditValue = TBLRC.Compute("Sum(CostOrPrice)", string.Empty);
            else
                textEditTotal.EditValue = 0;
            textEditInput.EditValue = textEditChange.EditValue = 0;
            labelControlChange.Text = "Change:";

        }

        private void pLinqInstantFeedbackSourceService_DismissEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {
            ((HeavenNailsModelDataContext)e.Tag).Dispose();
        }

        private void pLinqInstantFeedbackSourceService_GetEnumerable(object sender, DevExpress.Data.PLinq.GetEnumerableEventArgs e)
        {
            DataAccess Access = new DataAccess();
            List<Object> ListDa = Access.SelectAllShownServices();
            e.Source = (IEnumerable<Object>)ListDa[0];//((DataTable)ListDa[0]).AsEnumerable().Select(pt => new { SIndex = (decimal) pt["SIndex"] });
            e.Tag = ListDa[1];
        }



        private void gridViewServices_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DataTable TBLRC = (DataTable)gridControlReceiptDetails.DataSource;
                DataRow NRW = ((DataTable)gridControlReceiptDetails.DataSource).NewRow();
                NRW[0] = "Services";
                NRW[1] = -1;//QIndex
                NRW[2] = gridViewServices.GetRowCellValue(e.RowHandle, "SIndex");
                NRW[3] = gridViewServices.GetRowCellValue(e.RowHandle, "NameService");
                NRW[4] = 0;//ImportPrice
                NRW[5] = gridViewServices.GetRowCellValue(e.RowHandle, "Cost");
                TBLRC.Rows.Add(NRW);
            }
        }

        private void gridViewRecieptDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (MessageBox.Show("Do you want to remove this row  Name:  ["
                    + gridViewRecieptDetails.GetRowCellValue(e.RowHandle, "Name").ToString() + "] With Cost:[" +

                    gridViewRecieptDetails.GetRowCellValue(e.RowHandle, "CostOrPrice").ToString() +

                    " ]", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    gridViewRecieptDetails.DeleteRow(e.RowHandle);
            }
        }

        private void gridViewRecieptDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            //MessageBox.Show("");
        }

        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = 0.0;
            textEditChange.EditValue = 0;
            textEditChange.BackColor = Color.PeachPuff;
        }

        private void simpleButtonOne_Click(object sender, EventArgs e)
        {

            textEditInput.EditValue = textEditInput.EditValue.ToString() + "1";
        }

        private void simpleButtonTwo_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "2";
        }

        private void simpleButtonDot_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + ".";
        }

        private void simpleButtonThree_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "3";
        }

        private void simpleButtonFor_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "4";
        }

        private void simpleButtonFive_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "5";
        }

        private void simpleButtonSix_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "6";
        }

        private void simpleButtonSeven_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "7";
        }

        private void simpleButtonEght_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "8";
        }

        private void simpleButtonNine_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "9";
        }

        private void simpleButtonZero_Click(object sender, EventArgs e)
        {
            textEditInput.EditValue = textEditInput.EditValue.ToString() + "0";
        }
        private void ClearReciept(decimal Change)
        {
            textEditTotal.EditValue = 0;
            textEditChange.BackColor = Color.PeachPuff;
            textEditChange.EditValue = Math.Round((Math.Round(Change * 20, MidpointRounding.AwayFromZero) / 20), 2);
            ((DataTable)gridControlReceiptDetails.DataSource).Rows.Clear();
        }
        private Decimal RoundingData(decimal InD)
        {
            return Math.Round((Math.Round(InD * 20, MidpointRounding.AwayFromZero) / 20), 2);
        }
        private void simpleButtonCash_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textEditInput.EditValue.ToString()) > 0 && Convert.ToDecimal(textEditTotal.EditValue.ToString()) > 0)
            {
                decimal Change = Convert.ToDecimal(textEditInput.EditValue) - Convert.ToDecimal(textEditTotal.EditValue);
                textEditChange.EditValue = Change;
                if (Math.Round((Math.Round(Change * 20, MidpointRounding.AwayFromZero) / 20), 2) >= 0)
                {
                    // Insert data

                    if (true)//insert data code here---------------------------------------------------- cash only
                    {
                        ClearReciept(Change);

                        Codes.Functions.CheckPrinter();
                        if (MessageBox.Show("Do you want to print the receipt.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //Print receipt
                            Codes.RawPrinterHelper.OpenCashDrawer(Properties.Settings.Default.RecieptPrinter);
                            //simpleButtonPrint_Click(null, null);
                            Codes.RawPrinterHelper.CutPaper(Properties.Settings.Default.RecieptPrinter);
                        }
                        else
                            Codes.RawPrinterHelper.OpenCashDrawer(Properties.Settings.Default.RecieptPrinter);

                    }
                }
                else
                {
                    textEditChange.BackColor = Color.Red;
                    if (MessageBox.Show("Pay the rest by EFTPOS [$" + Math.Abs(Math.Round(Change, 2)) + "]", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //EFTPOS payment
                        bool Paid = false;
                        PrimaryGoals.ShowDialog frm = new PrimaryGoals.ShowDialog();
                        frm.Mesag = "Pay $" + Math.Round(Math.Abs(Change), 2) + " by EFTPOS";
                        frm.ShowDialog();
                        Paid = frm.Paied;
                        if (Paid == true)
                        {
                            if (true) //InsertData EFTPOS------------------------------------------------ Cash and EFTPOS
                            {
                                ClearReciept(Change);
                                Codes.Functions.CheckPrinter();
                                if (MessageBox.Show("Do you want to print the receipt.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //print reciept
                                }

                                textEditChange.EditValue = Change;
                                labelControlChange.Text = "EFTPOS:";

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not enough money, Please check how much cash the costumer gave to you.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }



        private void simpleButtonEftpos_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textEditTotal.EditValue.ToString()) > 0)
            {

                //EFTPOS payment
                bool Paid = false;
                PrimaryGoals.ShowDialog frm = new PrimaryGoals.ShowDialog();
                frm.Mesag = "Pay $" + Math.Round(Convert.ToDecimal(textEditTotal.EditValue.ToString()), 2) + " by EFTPOS";
                frm.ShowDialog();
                Paid = frm.Paied;
                if (Paid == true)
                {
                    if (true) //InsertData EFTPOS--------------------------------------------------------------------EFTPOS only
                    {
                        Codes.Functions.CheckPrinter();
                        if (MessageBox.Show("Do you want to print the receipt.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //print reciept
                        }
                        textEditChange.EditValue = Convert.ToDecimal(textEditTotal.EditValue.ToString());
                        ClearReciept(Convert.ToDecimal(textEditTotal.EditValue.ToString()));
                        labelControlChange.Text = "EFTPOS:";

                    }
                }


            }
        }

        //-----------------------------

        private void simpleButtonOpenCashier_Click(object sender, EventArgs e)
        {

            if (Codes.Functions.CheckPrinter() == true)
                Codes.RawPrinterHelper.OpenCashDrawer(Properties.Settings.Default.RecieptPrinter);

        }

        private void simpleButtonCut_Click(object sender, EventArgs e)
        {
            if (Codes.Functions.CheckPrinter() == true)
                Codes.RawPrinterHelper.CutPaper(Properties.Settings.Default.RecieptPrinter);

        }

        private void accordionControlElementSetting_Click(object sender, EventArgs e)
        {
            SystemControl.Setting frm = new SystemControl.Setting();
            frm.ShowDialog();
        }


        //----------------------------------------------------------------------------------------------------------------------

        
      

        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            //cash
            DataTable TBLRC = (DataTable)gridControlReceiptDetails.DataSource;
            float SubTotal = (float)Convert.ToDecimal(textEditTotal.EditValue);
            float Discount = -(SubTotal * 0.05f);
            float rounding = SubTotal + Discount - (float)RoundingData(Convert.ToDecimal(SubTotal)+Convert.ToDecimal(Discount));
            PrintRecieptFuntion.PrintReceipt(
                TBLRC,
                "SUBTOTAL",
                SubTotal,
                "Discount 5%",
                Discount,
                "Rounding ",
                rounding,
                "TOTAL",
                SubTotal-rounding+Discount,
                "Cash",
                100,
                "EFTPOS",
                100,
                "Change",
                100,
                "Heaven Nails "+DateTime.Now,
                "Loyalty Card Details",
                "No.",
                "L001544",
                "Your Balance",
                5,
                5,
                "Congratulations! You have 5% discount"
                );

        }
       
        
       













        //HookSystem.KeyBoardHook.KeyboardHookClass keyboardHook;
        //private void simpleButtonScaner_Click(object sender, EventArgs e)
        //{


        //    // InitializeScanner();
        //    // MessageBox.Show(_scanBuffer);
        //    keyboardHook = new HookSystem.KeyBoardHook.KeyboardHookClass();
        //    keyboardHook.KeyDown += new HookSystem.KeyBoardHook.KeyboardHookClass.KeyboardHookCallback(keyboardHook_KeyDown);
        //    keyboardHook.KeyUp += new HookSystem.KeyBoardHook.KeyboardHookClass.KeyboardHookCallback(keyboardHook_KeyUp);
        //    keyboardHook.Install();
        //}
        //private void keyboardHook_KeyUp(HookSystem.KeyBoardHook.KeyboardHookClass.VKeys key)
        //{
        //    Console.WriteLine("[" + DateTime.Now.Millisecond.ToString() + "] KeyUp Event {" + key.ToString() + "}");

        //}
        //private void keyboardHook_KeyDown(HookSystem.KeyBoardHook.KeyboardHookClass.VKeys key)
        //{
        //    //Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] KeyDown Event {" + key.ToString() + "}");
        //}




















        //----------------------------------------------------

        void SendCode(object CodeScaned)
        {
            MessageBox.Show(CodeScaned.ToString());
           
        }
         void MyLoopMainScanner(object PortLS)
        {
            SerialPort MyPort = (SerialPort)PortLS;
            while (true)
            {
                string CodeSc = MyPort.ReadExisting();
                if (CodeSc.Length > 0)
                {
                    AddrowCallBack rw = new AddrowCallBack(SendCode);
                    this.Invoke(rw,new object[] { CodeSc });
                    MyPort.DiscardOutBuffer();
                    MyPort.DiscardInBuffer();
                }
            }
        }
        private void InitializeMainScanner()
        {
            port = null;
            try
            {
                //port = new SerialPort("COM" + iPort, 9600, Parity.None, 8, StopBits.One);
                port = new SerialPort(Properties.Settings.Default.MainScannerPort,
                    Properties.Settings.Default.BoudRate,
                    Properties.Settings.Default.Parity,
                    Properties.Settings.Default.DataBits,
                    Properties.Settings.Default.StopBits);
                port.Close();
                if (!port.IsOpen)
                    port.Open();
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                MainScannerThead = new Thread(MyLoopMainScanner);
                MainScannerThead.Name = "ScannerListionPort";
                MainScannerThead.Start(port);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Main Scanner Port Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SystemControl.Setting frm = new SystemControl.Setting();
                frm.ShowDialog();
            }
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainScannerThead != null)
                MainScannerThead.Abort();            
        }

       
    }
}