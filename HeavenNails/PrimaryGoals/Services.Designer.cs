namespace HeavenNails.PrimaryGoals
{
    partial class Services
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
            this.components = new System.ComponentModel.Container();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditHide = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditCost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.textEditID = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonDel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonInsert = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControlListServices = new DevExpress.XtraGrid.GridControl();
            this.gridViewListServices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNameService = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEditMoney = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumnActice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pLinqInstantFeedbackSourceService = new DevExpress.Data.PLinq.PLinqInstantFeedbackSource();
            this.dxErrorProviderS = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHide.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEditHide);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.textEditCost);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.textEditName);
            this.groupControl1.Controls.Add(this.textEditID);
            this.groupControl1.Controls.Add(this.simpleButtonDel);
            this.groupControl1.Controls.Add(this.simpleButtonUpdate);
            this.groupControl1.Controls.Add(this.simpleButtonInsert);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(804, 114);
            this.groupControl1.TabIndex = 0;
            // 
            // checkEditHide
            // 
            this.checkEditHide.EditValue = true;
            this.checkEditHide.Enabled = false;
            this.checkEditHide.Location = new System.Drawing.Point(745, 32);
            this.checkEditHide.Name = "checkEditHide";
            this.checkEditHide.Properties.Caption = "Hiden";
            this.checkEditHide.Size = new System.Drawing.Size(47, 19);
            this.checkEditHide.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(596, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Cost:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(204, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Name Service:";
            // 
            // textEditCost
            // 
            this.textEditCost.EditValue = "0.00";
            this.textEditCost.Enabled = false;
            this.textEditCost.Location = new System.Drawing.Point(628, 32);
            this.textEditCost.Name = "textEditCost";
            this.textEditCost.Properties.Mask.EditMask = "c2";
            this.textEditCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEditCost.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.textEditCost.Size = new System.Drawing.Size(111, 20);
            this.textEditCost.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "ID Service:";
            // 
            // textEditName
            // 
            this.textEditName.Enabled = false;
            this.textEditName.Location = new System.Drawing.Point(279, 32);
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.textEditName.Properties.Appearance.Options.UseBackColor = true;
            this.textEditName.Size = new System.Drawing.Size(298, 20);
            this.textEditName.TabIndex = 1;
            this.textEditName.EditValueChanged += new System.EventHandler(this.textEditName_EditValueChanged);
            // 
            // textEditID
            // 
            this.textEditID.Enabled = false;
            this.textEditID.Location = new System.Drawing.Point(71, 32);
            this.textEditID.Name = "textEditID";
            this.textEditID.Properties.ReadOnly = true;
            this.textEditID.Size = new System.Drawing.Size(105, 20);
            this.textEditID.TabIndex = 1;
            // 
            // simpleButtonDel
            // 
            this.simpleButtonDel.ImageOptions.Image = global::HeavenNails.Properties.Resources.deletetablerows_16x16;
            this.simpleButtonDel.Location = new System.Drawing.Point(468, 69);
            this.simpleButtonDel.Name = "simpleButtonDel";
            this.simpleButtonDel.Size = new System.Drawing.Size(80, 38);
            this.simpleButtonDel.TabIndex = 0;
            this.simpleButtonDel.Text = "Remove";
            this.simpleButtonDel.Click += new System.EventHandler(this.simpleButtonDel_Click);
            // 
            // simpleButtonUpdate
            // 
            this.simpleButtonUpdate.ImageOptions.Image = global::HeavenNails.Properties.Resources.edit_16x16;
            this.simpleButtonUpdate.Location = new System.Drawing.Point(363, 69);
            this.simpleButtonUpdate.Name = "simpleButtonUpdate";
            this.simpleButtonUpdate.Size = new System.Drawing.Size(75, 38);
            this.simpleButtonUpdate.TabIndex = 0;
            this.simpleButtonUpdate.Text = "Edit";
            this.simpleButtonUpdate.Click += new System.EventHandler(this.simpleButtonUpdate_Click);
            // 
            // simpleButtonInsert
            // 
            this.simpleButtonInsert.ImageOptions.Image = global::HeavenNails.Properties.Resources.add_16x16;
            this.simpleButtonInsert.Location = new System.Drawing.Point(254, 69);
            this.simpleButtonInsert.Name = "simpleButtonInsert";
            this.simpleButtonInsert.Size = new System.Drawing.Size(75, 38);
            this.simpleButtonInsert.TabIndex = 0;
            this.simpleButtonInsert.Text = "Add";
            this.simpleButtonInsert.Click += new System.EventHandler(this.simpleButtonInsert_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControlListServices);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 114);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(804, 365);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Services List";
            // 
            // gridControlListServices
            // 
            this.gridControlListServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlListServices.Location = new System.Drawing.Point(2, 20);
            this.gridControlListServices.MainView = this.gridViewListServices;
            this.gridControlListServices.Name = "gridControlListServices";
            this.gridControlListServices.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEditMoney});
            this.gridControlListServices.Size = new System.Drawing.Size(800, 343);
            this.gridControlListServices.TabIndex = 0;
            this.gridControlListServices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewListServices});
            // 
            // gridViewListServices
            // 
            this.gridViewListServices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnIndex,
            this.gridColumnNameService,
            this.gridColumnCost,
            this.gridColumnActice});
            this.gridViewListServices.GridControl = this.gridControlListServices;
            this.gridViewListServices.Name = "gridViewListServices";
            this.gridViewListServices.OptionsBehavior.Editable = false;
            this.gridViewListServices.OptionsBehavior.ReadOnly = true;
            this.gridViewListServices.OptionsDetail.ShowDetailTabs = false;
            this.gridViewListServices.OptionsFind.AlwaysVisible = true;
            this.gridViewListServices.OptionsView.ColumnAutoWidth = false;
            this.gridViewListServices.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewListServices.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewListServices_RowClick);
            this.gridViewListServices.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewListServices_FocusedRowChanged);
            this.gridViewListServices.RowLoaded += new DevExpress.XtraGrid.Views.Base.RowEventHandler(this.gridViewListServices_RowLoaded);
            // 
            // gridColumnID
            // 
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.Width = 31;
            // 
            // gridColumnIndex
            // 
            this.gridColumnIndex.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnIndex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnIndex.Caption = "Code";
            this.gridColumnIndex.FieldName = "SIndex";
            this.gridColumnIndex.Name = "gridColumnIndex";
            this.gridColumnIndex.Visible = true;
            this.gridColumnIndex.VisibleIndex = 0;
            this.gridColumnIndex.Width = 72;
            // 
            // gridColumnNameService
            // 
            this.gridColumnNameService.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnNameService.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnNameService.Caption = "Name Service";
            this.gridColumnNameService.FieldName = "NameService";
            this.gridColumnNameService.Name = "gridColumnNameService";
            this.gridColumnNameService.Visible = true;
            this.gridColumnNameService.VisibleIndex = 1;
            this.gridColumnNameService.Width = 363;
            // 
            // gridColumnCost
            // 
            this.gridColumnCost.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnCost.Caption = "Cost";
            this.gridColumnCost.ColumnEdit = this.repositoryItemTextEditMoney;
            this.gridColumnCost.FieldName = "Cost";
            this.gridColumnCost.Name = "gridColumnCost";
            this.gridColumnCost.Visible = true;
            this.gridColumnCost.VisibleIndex = 2;
            this.gridColumnCost.Width = 130;
            // 
            // repositoryItemTextEditMoney
            // 
            this.repositoryItemTextEditMoney.AutoHeight = false;
            this.repositoryItemTextEditMoney.Mask.EditMask = "c2";
            this.repositoryItemTextEditMoney.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEditMoney.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEditMoney.Name = "repositoryItemTextEditMoney";
            // 
            // gridColumnActice
            // 
            this.gridColumnActice.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnActice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnActice.Caption = "Hiden";
            this.gridColumnActice.FieldName = "Active";
            this.gridColumnActice.Name = "gridColumnActice";
            this.gridColumnActice.Visible = true;
            this.gridColumnActice.VisibleIndex = 3;
            this.gridColumnActice.Width = 50;
            // 
            // pLinqInstantFeedbackSourceService
            // 
            this.pLinqInstantFeedbackSourceService.DefaultSorting = "SIndex ASC";
            this.pLinqInstantFeedbackSourceService.DesignTimeElementType = typeof(HeavenNails.Database.tb_Sevice);
            this.pLinqInstantFeedbackSourceService.GetEnumerable += new System.EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.pLinqInstantFeedbackSourceService_GetEnumerable);
            this.pLinqInstantFeedbackSourceService.DismissEnumerable += new System.EventHandler<DevExpress.Data.PLinq.GetEnumerableEventArgs>(this.pLinqInstantFeedbackSourceService_DismissEnumerable);
            // 
            // dxErrorProviderS
            // 
            this.dxErrorProviderS.ContainerControl = this;
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 479);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Services";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services";
            this.Load += new System.EventHandler(this.Services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHide.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlListServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewListServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.TextEdit textEditID;
        private DevExpress.XtraEditors.SimpleButton simpleButtonInsert;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEditCost;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDel;
        private DevExpress.XtraEditors.SimpleButton simpleButtonUpdate;
        private DevExpress.XtraGrid.GridControl gridControlListServices;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewListServices;
        private DevExpress.Data.PLinq.PLinqInstantFeedbackSource pLinqInstantFeedbackSourceService;
        private DevExpress.XtraEditors.CheckEdit checkEditHide;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIndex;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNameService;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCost;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnActice;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditMoney;
    }
}