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
using DevExpress.Utils.Text;
using static HeavenNails.Codes.PrinterSettingFuntions;
using System.Drawing.Imaging;

namespace HeavenNails.SystemControl
{
    public partial class ReceiptPrinterSetting : DevExpress.XtraEditors.XtraForm
    {
        private List<LineStructure> ListPropertiesOfLine;
        private List<LineStructure> ListPropertiesOfFooter;


        public ReceiptPrinterSetting()
        {
            InitializeComponent();
        }

        private void LoadSetting(RadioGroup TB, RadioGroup LTR, SpinEdit X, SpinEdit Y, bool TypeData)
        {
            if (TypeData == true)
            {
                LTR.EditValue = Properties.Settings.Default.LogoLeftToRight;
                TB.EditValue = Properties.Settings.Default.LogoTopBottom;
                X.EditValue = Properties.Settings.Default.LogoSizeX;
                Y.EditValue = Properties.Settings.Default.LogoSizeY;
            }
            else
            {
                LTR.EditValue = Properties.Settings.Default.BarCodeLeftToRight;
                TB.EditValue = Properties.Settings.Default.BarCodeTopAndBottom;
                X.EditValue = Properties.Settings.Default.BarCodeSizeX;
                Y.EditValue = Properties.Settings.Default.BarCodeSizeY;
            }
        }
        private void SaveDataLogoAndBar()
        {
            Properties.Settings.Default.LogoShow = checkEditShow.Checked;
            Properties.Settings.Default.LogoLeftToRight = Convert.ToInt32(radioGroupLogoLRC.EditValue);
            Properties.Settings.Default.LogoTopBottom = Convert.ToInt32(radioGroupLogoTB.EditValue);
            Properties.Settings.Default.LogoSizeX = Convert.ToInt32(spinEditSizeLogoX.EditValue);
            Properties.Settings.Default.LogoSizeY = Convert.ToInt32(spinEditSizeLogoY.EditValue);
            if (comboBoxEditTypeCode.EditValue.ToString().Trim().Length > 0)
                Properties.Settings.Default.BarCodeType = comboBoxEditTypeCode.EditValue.ToString();
            else
                Properties.Settings.Default.BarCodeType = "QR Code";
            Properties.Settings.Default.BarCodeLeftToRight = Convert.ToInt32(radioGroupBarLRC.EditValue);
            Properties.Settings.Default.BarCodeTopAndBottom = Convert.ToInt32(radioGroupBarTB.EditValue);
            Properties.Settings.Default.BarCodeSizeX = Convert.ToInt32(spinEditBarX.EditValue);
            Properties.Settings.Default.BarCodeSizeY = Convert.ToInt32(spinEditBarY.EditValue);

            Properties.Settings.Default.Save();
        }
        private void ReceiptPrinterSetting_Load(object sender, EventArgs e)
        {
            try
            {
                pictureEditLogo.Image = LoadImageToMemory(Application.StartupPath + "\\Logo.png");
                checkEditShow.Checked = Properties.Settings.Default.LogoShow;
                LoadSetting(radioGroupLogoTB, radioGroupLogoLRC, spinEditSizeLogoX, spinEditSizeLogoY, true);
                comboBoxEditTypeCode.EditValue = Properties.Settings.Default.BarCodeType;
                LoadSetting(radioGroupBarTB, radioGroupBarLRC, spinEditBarX, spinEditBarY, false);


                List<string> fonts = new List<string>();

                foreach (FontFamily font in System.Drawing.FontFamily.Families)
                {
                    if (font.Name.Length > 0)
                        fonts.Add(font.Name);
                }
                lookUpEditFont.Properties.DataSource = fonts;

                listBoxControlLines.Items.Clear();
                string DataLoading = Properties.Settings.Default.ReceiptPrinterSetting;
                string DataLoadFooter = Properties.Settings.Default.ReceiptPrinterFooter;
                string DataLoadDetails = Properties.Settings.Default.ReceiptPrinterDetails;
                if (DataLoading.Trim().Length > 0)
                {
                    List<object> Data = ListReturn(DataLoading);
                    ListPropertiesOfLine = (List<LineStructure>)Data[0];
                    if (Data[1] != null)
                        listBoxControlLines.DataSource = Data[1];
                    else
                        listBoxControlLines.DataSource = null;

                }
                else
                    listBoxControlLines.DataSource = null;
                if (DataLoadFooter.Trim().Length > 0)
                {
                    List<object> Data = ListReturn(DataLoadFooter);
                    ListPropertiesOfFooter = (List<LineStructure>)Data[0];
                    if (Data[1] != null)
                        listBoxControlFooter.DataSource = Data[1];
                    else
                        listBoxControlFooter.DataSource = null;



                }
                else
                    listBoxControlFooter.DataSource = null;

                if (DataLoadDetails.Trim().Length > 0)
                {
                    List<object> Data = ListReturn(DataLoadDetails);
                    LineStructure LineDataIn = ((List<LineStructure>)Data[0])[0];
                    lookUpEditFont.EditValue = LineDataIn.NameFont;
                    spinEditSize.EditValue = LineDataIn.SizeFont;
                    checkButtonU.Checked = LineDataIn.U;
                    checkButtonB.Checked = LineDataIn.B;
                    checkButtonI.Checked = LineDataIn.I;

                    listBoxControlDetails.Font = new Font(LineDataIn.NameFont, (float)LineDataIn.SizeFont, LineDataIn.TypeFont);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Properties.Settings.Default.ReceiptPrinterSetting = "";
                //Properties.Settings.Default.ReceiptPrinterFooter = "";
                //Properties.Settings.Default.ReceiptPrinterDetails = "";

                // Properties.Settings.Default.Save();
            }


        }
        private void DrawItem(ListBoxControl control, List<LineStructure> DataIn, ListBoxDrawItemEventArgs e)
        {
            e.Appearance.DrawBackground(e.Cache, e.Bounds);
            string text = control.GetItemText(e.Index);
            foreach (LineStructure RW in DataIn)
            {
                if (RW.TextData == text)
                {
                    StringFormat stringFormat = new StringFormat();

                    if (RW.PositionLine == "CT")
                    {
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                    }
                    else
                        if (RW.PositionLine == "N")
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                    }
                    else
                    {
                        stringFormat.Alignment = StringAlignment.Far;
                        stringFormat.LineAlignment = StringAlignment.Far;
                    }
                    TextUtils.DrawString(e.Graphics, text, new Font(RW.NameFont, (float)RW.SizeFont, RW.TypeFont),
                control.Appearance.ForeColor, e.Bounds, stringFormat);
                    e.Handled = true;
                    break;
                }
            }
        }
        private void listBoxControlLines_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {

            ListBoxControl control = (ListBoxControl)sender;
            if (ListPropertiesOfLine != null && control == listBoxControlLines)
                DrawItem(control, ListPropertiesOfLine, e);
            if (ListPropertiesOfFooter != null && control == listBoxControlFooter)
                DrawItem(control, ListPropertiesOfFooter, e);



        }

        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {

            string NewLIneData = string.Empty;//"Heavan Nails\tTime New Roman\t30\tU\tB\tI\tCT";
            NewLIneData += textEditEnter.Text.Trim() + "\t";
            NewLIneData += lookUpEditFont.Text.Trim() + "\t";
            NewLIneData += spinEditSize.Text.Trim() + "\t";
            if (checkButtonU.Checked == true)
                NewLIneData += "U\t";
            else
                NewLIneData += "\t";

            if (checkButtonB.Checked == true)
                NewLIneData += "B\t";
            else
                NewLIneData += "\t";

            if (checkButtonI.Checked == true)
                NewLIneData += "I\t";
            else
                NewLIneData += "\t";


            if (comboBoxEditComAL.Text == "Left")
                NewLIneData += "N";
            else
                if (comboBoxEditComAL.Text == "Center")
                NewLIneData += "CT";

            if (xtraTabControlDetails.SelectedTabPageIndex == 0 && textEditEnter.Text.Trim().Length > 0)
            {
                if (Properties.Settings.Default.ReceiptPrinterSetting.Length > 0)
                    Properties.Settings.Default.ReceiptPrinterSetting += "\n" + NewLIneData;
                else
                    Properties.Settings.Default.ReceiptPrinterSetting = NewLIneData;
                Properties.Settings.Default.Save();
            }
            else if (xtraTabControlDetails.SelectedTabPageIndex == 1)
            {

                Properties.Settings.Default.ReceiptPrinterDetails = NewLIneData;
                Properties.Settings.Default.Save();

            }
            else if (xtraTabControlDetails.SelectedTabPageIndex == 2 && textEditEnter.Text.Trim().Length > 0)
            {
                if (Properties.Settings.Default.ReceiptPrinterFooter.Length > 0)
                    Properties.Settings.Default.ReceiptPrinterFooter += "\n" + NewLIneData;
                else
                    Properties.Settings.Default.ReceiptPrinterFooter = NewLIneData;
                Properties.Settings.Default.Save();
            }
            ReceiptPrinterSetting_Load(null, null);




        }
        private void MeasureItem(ListBoxControl control, List<LineStructure> DataIn, MeasureItemEventArgs e)
        {
            string text = control.GetItemText(e.Index);
            foreach (LineStructure RW in DataIn)
            {
                if (RW.TextData == text)
                {

                    StringFormat stringFormat = new StringFormat();

                    if (RW.PositionLine == "CT")
                    {
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                    }
                    else
                        if (RW.PositionLine == "N")
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.LineAlignment = StringAlignment.Near;
                    }
                    else
                    {
                        stringFormat.Alignment = StringAlignment.Far;
                        stringFormat.LineAlignment = StringAlignment.Far;
                    }

                    Size textSize = TextUtils.GetStringSize(e.Graphics, text, new Font(RW.NameFont, (float)RW.SizeFont, RW.TypeFont),
                stringFormat, control.ClientRectangle.Width);
                    e.ItemHeight = textSize.Height + 5;
                    break;
                }
            }

        }
        private void listBoxControlLines_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBoxControl control = (ListBoxControl)sender;


            if (ListPropertiesOfLine != null && control == listBoxControlLines)
                MeasureItem(control, ListPropertiesOfLine, e);
            if (ListPropertiesOfFooter != null && control == listBoxControlFooter)
                MeasureItem(control, ListPropertiesOfFooter, e);

        }



        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            this.Close();

        }



        private void simpleButtonChange_Click(object sender, EventArgs e)
        {


            //MessageBox.Show(ConvertStructureToStringPrintReceipt(ListPropertiesOfLine));

            //MessageBox.Show(listBoxControlLines.SelectedValue.ToString());
            //for (int i = 0; i < ListPropertiesOfLine.Count; i++)
            //{
            //    if (ListPropertiesOfLine[i].TextData == listBoxControlLines.SelectedValue.ToString())
            //    {
            //        LineStructure nwl = ListPropertiesOfLine[i];
            //        nwl.TextData = textEditEnter.Text.Trim();
            //        nwl.NameFont = lookUpEditFont.Text;
            //        nwl.SizeFont = Convert.ToDouble(spinEditSize.EditValue);
            //        nwl.U = checkButtonU.Checked;
            //        nwl.B = checkButtonB.Checked;
            //        nwl.I = checkButtonI.Checked;



            //        if (comboBoxEditComAL.Text == "Left")
            //            nwl.PositionLine = "N";
            //        else
            //            if (comboBoxEditComAL.Text == "Center")
            //            nwl.PositionLine = "CT";
            //        else
            //            nwl.PositionLine = "F";
            //        ListPropertiesOfLine[i] = nwl;

            //    }
            //}
            int LastSelected = 0;
            LineStructure nwl = new LineStructure();
            nwl.TextData = textEditEnter.Text.Trim();
            nwl.NameFont = lookUpEditFont.Text;
            nwl.SizeFont = Convert.ToDouble(spinEditSize.EditValue);
            nwl.U = checkButtonU.Checked;
            nwl.B = checkButtonB.Checked;
            nwl.I = checkButtonI.Checked;
            if (comboBoxEditComAL.Text == "Left")
                nwl.PositionLine = "N";
            else
                if (comboBoxEditComAL.Text == "Center")
                nwl.PositionLine = "CT";
            else
                nwl.PositionLine = "F";



            if (xtraTabControlDetails.SelectedTabPageIndex == 0)
            {
                if (listBoxControlLines.SelectedIndex >= 0)
                {
                    ListPropertiesOfLine[listBoxControlLines.SelectedIndex] = nwl;
                    Properties.Settings.Default.ReceiptPrinterSetting = ConvertStructureToStringPrintReceipt(ListPropertiesOfLine);
                    Properties.Settings.Default.Save();
                    LastSelected = listBoxControlLines.SelectedIndex;
                    ReceiptPrinterSetting_Load(null, null);
                    listBoxControlLines.SelectedIndex = LastSelected;
                }
            }
            else
            if (xtraTabControlDetails.SelectedTabPageIndex == 1)
            {

                simpleButtonAdd_Click(null, null);

            }
            else
            if (xtraTabControlDetails.SelectedTabPageIndex == 2)
            {
                if (listBoxControlFooter.SelectedIndex >= 0)
                {
                    ListPropertiesOfFooter[listBoxControlFooter.SelectedIndex] = nwl;
                    Properties.Settings.Default.ReceiptPrinterFooter = ConvertStructureToStringPrintReceipt(ListPropertiesOfFooter);
                    Properties.Settings.Default.Save();
                    LastSelected = listBoxControlFooter.SelectedIndex;
                    ReceiptPrinterSetting_Load(null, null);
                    listBoxControlFooter.SelectedIndex = LastSelected;
                }
            }
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxControlLines.SelectedIndex >= 0 && xtraTabControlDetails.SelectedTabPageIndex == 0)
            {
                ListPropertiesOfLine.RemoveAt(listBoxControlLines.SelectedIndex);
                Properties.Settings.Default.ReceiptPrinterSetting = ConvertStructureToStringPrintReceipt(ListPropertiesOfLine);
                Properties.Settings.Default.Save();
                ReceiptPrinterSetting_Load(null, null);
            }
            if (listBoxControlFooter.SelectedIndex >= 0 && xtraTabControlDetails.SelectedTabPageIndex == 2)
            {
                ListPropertiesOfFooter.RemoveAt(listBoxControlFooter.SelectedIndex);
                Properties.Settings.Default.ReceiptPrinterFooter = ConvertStructureToStringPrintReceipt(ListPropertiesOfFooter);
                Properties.Settings.Default.Save();
                ReceiptPrinterSetting_Load(null, null);
            }
        }

        private void xtraTabControlDetails_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabControlDetails_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControlDetails.SelectedTabPageIndex == 1)
            {
                textEditEnter.Enabled = false;
                comboBoxEditComAL.Enabled = false;
                simpleButtonAdd.Enabled = false;
                simpleButtonDelete.Enabled = false;
                ReceiptPrinterSetting_Load(null, null);
            }
            else
            {
                textEditEnter.Enabled = true;
                comboBoxEditComAL.Enabled = true;
                simpleButtonAdd.Enabled = true;
                simpleButtonDelete.Enabled = true;
            }
        }

        private void simpleButtonLoadPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog MyopenFileDialog = new OpenFileDialog();
            MyopenFileDialog.Filter = "Logo files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            MyopenFileDialog.Title = "Select Logo";
            if (MyopenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Bitmap MDataM = LoadImageToMemory(MyopenFileDialog.FileName);
                    ResizeImage((MDataM), new Size(150, 150)).Save("Logo.png", ImageFormat.Png);
                    //ResizeImage(GrayScaleFilter(MDataM), new Size(150, 150)).Save("Logo.png", ImageFormat.Png);
                    pictureEditLogo.Image = LoadImageToMemory(Application.StartupPath + "\\Logo.png");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void checkEditShow_CheckedChanged(object sender, EventArgs e)
        {

            pictureEditLogo.Enabled = spinEditSizeLogoX.Enabled = spinEditSizeLogoY.Enabled =
                radioGroupLogoLRC.Enabled = radioGroupLogoTB.Enabled = simpleButtonLoadPic.Enabled = checkEditShow.Checked;
        }

        private void ReceiptPrinterSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDataLogoAndBar();
        }

        private void listBoxControlLines_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxControlLines_MouseClick(object sender, MouseEventArgs e)
        {
            ListBoxControl ListBoxM = (ListBoxControl)sender;
            if (ListBoxM.SelectedIndex >= 0)
            {
                if (ListBoxM == listBoxControlLines)
                {
                    textEditEnter.Text = ListPropertiesOfLine[ListBoxM.SelectedIndex].TextData;
                    lookUpEditFont.EditValue = ListPropertiesOfLine[ListBoxM.SelectedIndex].NameFont;
                    spinEditSize.EditValue = ListPropertiesOfLine[ListBoxM.SelectedIndex].SizeFont;
                    comboBoxEditComAL.EditValue = ListPropertiesOfLine[ListBoxM.SelectedIndex].PositionLine == "N" ? "Left" :
                        ListPropertiesOfLine[ListBoxM.SelectedIndex].PositionLine == "CT" ? "Center" : "Right";
                    checkButtonU.Checked = ListPropertiesOfLine[ListBoxM.SelectedIndex].U;
                    checkButtonB.Checked = ListPropertiesOfLine[ListBoxM.SelectedIndex].B;
                    checkButtonI.Checked = ListPropertiesOfLine[ListBoxM.SelectedIndex].I;
                }
                else
                {
                    textEditEnter.Text = ListPropertiesOfFooter[ListBoxM.SelectedIndex].TextData;
                    lookUpEditFont.EditValue = ListPropertiesOfFooter[ListBoxM.SelectedIndex].NameFont;
                    spinEditSize.EditValue = ListPropertiesOfFooter[ListBoxM.SelectedIndex].SizeFont;
                    comboBoxEditComAL.EditValue = ListPropertiesOfFooter[ListBoxM.SelectedIndex].PositionLine == "N" ? "Left" :
                        ListPropertiesOfFooter[ListBoxM.SelectedIndex].PositionLine == "CT" ? "Center" : "Right";
                    checkButtonU.Checked = ListPropertiesOfFooter[ListBoxM.SelectedIndex].U;
                    checkButtonB.Checked = ListPropertiesOfFooter[ListBoxM.SelectedIndex].B;
                    checkButtonI.Checked = ListPropertiesOfFooter[ListBoxM.SelectedIndex].I;
                }

            }
        }
    }
}