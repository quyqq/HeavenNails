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

namespace HeavenNails.PrimaryGoals
{
    public partial class ShowDialog : DevExpress.XtraEditors.XtraForm
    {
        public bool Paied = false;
        public string Mesag = string.Empty;
        public ShowDialog()
        {
            InitializeComponent();
        }

        private void simpleButtonCancal_Click(object sender, EventArgs e)
        {
            Paied = false;
            this.Close();
        }

        private void simpleButtonChange_Click(object sender, EventArgs e)
        {
            Paied = true;
            this.Close();
        }

        private void ShowDialog_Load(object sender, EventArgs e)
        {
            labelControlMes.Text = Mesag;
        }
    }
}