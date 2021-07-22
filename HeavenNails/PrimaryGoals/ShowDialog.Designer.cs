namespace HeavenNails.PrimaryGoals
{
    partial class ShowDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControlMes = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonCancal = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonChange = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // labelControlMes
            // 
            this.labelControlMes.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControlMes.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControlMes.Appearance.Options.UseFont = true;
            this.labelControlMes.Appearance.Options.UseForeColor = true;
            this.labelControlMes.Location = new System.Drawing.Point(142, 59);
            this.labelControlMes.Name = "labelControlMes";
            this.labelControlMes.Size = new System.Drawing.Size(248, 25);
            this.labelControlMes.TabIndex = 1;
            this.labelControlMes.Text = "Pay $100.00 by EFTPOS";
            // 
            // simpleButtonCancal
            // 
            this.simpleButtonCancal.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonCancal.Appearance.Options.UseFont = true;
            this.simpleButtonCancal.ImageOptions.Image = global::HeavenNails.Properties.Resources.reset_32x32;
            this.simpleButtonCancal.Location = new System.Drawing.Point(12, 121);
            this.simpleButtonCancal.Name = "simpleButtonCancal";
            this.simpleButtonCancal.Size = new System.Drawing.Size(115, 36);
            this.simpleButtonCancal.TabIndex = 0;
            this.simpleButtonCancal.Text = "Cancel";
            this.simpleButtonCancal.Click += new System.EventHandler(this.simpleButtonCancal_Click);
            // 
            // simpleButtonChange
            // 
            this.simpleButtonChange.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonChange.Appearance.Options.UseFont = true;
            this.simpleButtonChange.ImageOptions.Image = global::HeavenNails.Properties.Resources.wifi_32x32;
            this.simpleButtonChange.Location = new System.Drawing.Point(405, 121);
            this.simpleButtonChange.Name = "simpleButtonChange";
            this.simpleButtonChange.Size = new System.Drawing.Size(122, 36);
            this.simpleButtonChange.TabIndex = 0;
            this.simpleButtonChange.Text = "Chaged";
            this.simpleButtonChange.Click += new System.EventHandler(this.simpleButtonChange_Click);
            // 
            // ShowDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 169);
            this.ControlBox = false;
            this.Controls.Add(this.labelControlMes);
            this.Controls.Add(this.simpleButtonCancal);
            this.Controls.Add(this.simpleButtonChange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ShowDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonChange;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancal;
        private DevExpress.XtraEditors.LabelControl labelControlMes;
    }
}