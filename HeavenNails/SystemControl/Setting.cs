using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace HeavenNails.SystemControl
{
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        public Setting()
        {
            InitializeComponent();
        }

       
        void LoadList()
        {
            List<String> ListPrinter = new List<string>();

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                ListPrinter.Add(printer);
            }

            lookUpEditDUC.Properties.DataSource = ListPrinter;
            lookUpEditRe.Properties.DataSource = ListPrinter;
        }
        private void Setting_Load(object sender, EventArgs e)
        {
           
            try
            {
                LoadList();
                lookUpEditDUC.EditValue = Properties.Settings.Default.DocumentsPrinter;
                lookUpEditRe.EditValue = Properties.Settings.Default.RecieptPrinter;
                textEditServer.Text = Properties.Settings.Default.Server;
                textEditAccount.Text = Properties.Settings.Default.AccountLOG;
                textEditPass.Text = Properties.Settings.Default.Password;
                textEditDatabase.Text = Properties.Settings.Default.Database;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not load data ." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void simpleButtonREfesh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadList();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not load data ." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.DocumentsPrinter = lookUpEditDUC.EditValue.ToString();
                Properties.Settings.Default.RecieptPrinter = lookUpEditRe.EditValue.ToString();
                Properties.Settings.Default.Server = textEditServer.Text.Trim();
                Properties.Settings.Default.AccountLOG = textEditAccount.Text.Trim();
                Properties.Settings.Default.Password = textEditPass.Text;
                Properties.Settings.Default.Database = textEditDatabase.Text.Trim();
                Properties.Settings.Default.Save();
                MessageBox.Show("This software will restart to save the setting.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(Application.ExecutablePath);
                Application.Exit();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Can not save. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonTestCon_Click(object sender, EventArgs e)
        {
            string ServerPas = textEditServer.Text.Trim();
            string AccountPas = textEditAccount.Text.Trim();
            string PassPass = textEditPass.Text;
            string DatabasePas = textEditDatabase.Text.Trim();

            
            Codes.DataAccess testadtaConnection = new Codes.DataAccess();
            if (testadtaConnection.TestConnection(ServerPas,AccountPas,PassPass,DatabasePas)==true)
            {
                MessageBox.Show("Connect to server successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButtonREAdvance_Click(object sender, EventArgs e)
        {
            SystemControl.ReceiptPrinterSetting frm = new ReceiptPrinterSetting();
            frm.ShowDialog();
        }
    }
}