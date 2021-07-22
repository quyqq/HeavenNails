using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static HeavenNails.Codes.PrinterSettingFuntions;

namespace HeavenNails.Codes
{
    public static class PrinterSettingFuntions
    {
        public struct LineStructure
        {
            public string TextData;
            public string NameFont;
            public double SizeFont;
            public FontStyle TypeFont;
            public String PositionLine;
            public bool U;
            public bool B;
            public bool I;
        }

        public static List<object> ListReturn(string DataIn)
        {
            List<string> Lines = new List<string>();
            List<object> DataOut = new List<object>();
            string[] ArrayLines = DataIn.Split('\n');
            List<LineStructure> LoaddingList = new List<LineStructure>();

            if (ArrayLines.Length > 0)
                foreach (string line in ArrayLines)
                {
                    LineStructure newLine = new LineStructure();
                    string[] PraList = line.Split('\t');
                    newLine.TextData = PraList[0];
                    Lines.Add(PraList[0]);
                    newLine.NameFont = PraList[1];
                    newLine.SizeFont = Convert.ToDouble(PraList[2].Trim().ToString());
                    newLine.PositionLine = PraList[6];
                    FontStyle Myfont = new FontStyle();

                    if (PraList[3].ToString().Trim() != "U" &&
                        PraList[4].ToString().Trim() != "B" &&
                        PraList[5].ToString().Trim() != "I")
                        Myfont = FontStyle.Regular;

                    if (PraList[3].ToString().Trim() == "U")
                    {
                        Myfont = Myfont | FontStyle.Underline;
                        newLine.U = true;
                    }

                    if (PraList[4].ToString().Trim() == "B")
                    {
                        Myfont = Myfont | FontStyle.Bold;
                        newLine.B = true;
                    }

                    if (PraList[5].ToString().Trim() == "I")
                    {
                        Myfont = Myfont | FontStyle.Italic;
                        newLine.I = true;
                    }

                    newLine.TypeFont = Myfont;

                    LoaddingList.Add(newLine);
                }
            DataOut.Add(LoaddingList);
            if (Lines.Count > 0)
                DataOut.Add(Lines);
            else
                DataOut.Add(null);



            return DataOut;
        }
        public static string ConvertStructureToStringPrintReceipt(List<LineStructure> ListStruct)
        {

            string DataOut = string.Empty;
            foreach (LineStructure rw in ListStruct)
            {
                string NewLine = rw.TextData + "\t" + rw.NameFont + "\t" + rw.SizeFont + "\t" +
                    (rw.U == true ? "U" : "").ToString() + "\t" +
                    (rw.B == true ? "B" : "").ToString() + "\t" +
                    (rw.I == true ? "I" : "").ToString() + "\t" +
                    rw.PositionLine;
                if (DataOut.Length > 0)
                {
                    DataOut += "\n" + NewLine;
                }
                else
                {
                    DataOut = NewLine;
                }
            }

            return DataOut;
        }
        public static Bitmap LoadImageToMemory(string file_name)
        {
            try
            {
                using (Bitmap bm = new Bitmap(file_name))
                {
                    return new Bitmap(bm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not load the picture : "+file_name+" "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
           
        }
        public static Bitmap GrayScaleFilter(Bitmap image)
        {
            Bitmap grayScale = new Bitmap(image.Width, image.Height);

            for (Int32 y = 0; y < grayScale.Height; y++)
                for (Int32 x = 0; x < grayScale.Width; x++)
                {
                    Color c = image.GetPixel(x, y);

                    Int32 gs = (Int32)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);

                    grayScale.SetPixel(x, y, Color.FromArgb(gs, gs, gs));
                }
            return grayScale;
        }
        public static Bitmap ResizeImage(Bitmap mg, Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;

            Bitmap bp;


            if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height /
            Convert.ToDouble(newSize.Height)))
                ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
            else
                ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
            myThumbHeight = Math.Ceiling(mg.Height / ratio);
            myThumbWidth = Math.Ceiling(mg.Width / ratio);

            Size thumbSize = new Size((int)myThumbWidth, (int)myThumbHeight);
            bp = new Bitmap(newSize.Width, newSize.Height);
            x = (newSize.Width - thumbSize.Width) / 2;
            y = (newSize.Height - thumbSize.Height);

            System.Drawing.Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);

            return bp;
        }

    }
    public static class PrintRecieptFuntion
    {
        private static PrintDocument pd;
        private static int LoY = 0;
        private static bool[] HeadderCheck;
        private static bool[] DetailsCheck;
        private static bool[] FootterCHeck;
        private static bool LogoCheck;
        private static bool BarCodeCheck;
        private static float HeightAR = 0;
        private static bool[] ListEnding;       

        private static DataTable ReceiptList;
        private static string SubTotalW, DiscountW, RoundingW, TotalW, CashW, EftposW, ChangeW,  LoyaltyTitleW, LoyaltyCardW, LoyaltyCardN,LoyaltyBalanceW,EndingDateW,MessageLoyaltyW;
        private static float SubTotalN, DiscountN, RoundingN, TotalN, CashN, EftposN, ChangeN, LoyaltyBalanceN, LoyaltyRequidN;
        public static void PrintReceipt(DataTable DataIn,
            string SubTotalWords,float SubTotalNumber,
            string DiscountWords, float DiscountNumber,
            string RoundingWords, float RoundingNumber,
            string TotalWords,float TotalNumber,
            string CashWords,float CashNumber,
            string EftposWords,float EftposNumber,
            string ChangeWords,float ChangeNumber,
            string EndingDateWords,
            string LoyaltyTitleWords,
            string LoyaltyCardNoWords,
            string LoyaltyCardNoNumber,
            string LoyaltyBalanceWord,
            float LoyaltyBalanceNumber,
            float LoyaltyRequidNumber,
            string MessageLoyaltyWords
            
            
            )
        {
            try
            {

                pd = new PrintDocument();
                pd.PrintController = new StandardPrintController();
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                pd.PrinterSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                pd.PrinterSettings.PrinterName = Properties.Settings.Default.RecieptPrinter;
                pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("custom", 285, 1000);//315
                HeightAR = pd.DefaultPageSettings.PrintableArea.Height;

                CreateListCheck(DataIn.Rows.Count);
                if (DataIn.Rows.Count > 0)
                {
                    ReceiptList = DataIn;
                    SubTotalW = SubTotalWords;
                    SubTotalN = SubTotalNumber;
                    DiscountW = DiscountWords;
                    DiscountN = DiscountNumber;
                    RoundingW = RoundingWords;
                    RoundingN = RoundingNumber;
                    TotalW = TotalWords;
                    TotalN = TotalNumber;
                    CashW = CashWords;
                    CashN = CashNumber;
                    EftposW = EftposWords;
                    EftposN = EftposNumber;
                    ChangeW = ChangeWords;
                    ChangeN = ChangeNumber;
                    EndingDateW = EndingDateWords;
                    LoyaltyTitleW = LoyaltyTitleWords;
                    LoyaltyCardW = LoyaltyCardNoWords;
                    LoyaltyCardN = LoyaltyCardNoNumber;
                    LoyaltyBalanceW = LoyaltyBalanceWord;
                    LoyaltyBalanceN = LoyaltyBalanceNumber;
                    LoyaltyRequidN = LoyaltyRequidNumber;
                    MessageLoyaltyW = MessageLoyaltyWords;
                    pd.PrintPage += printPage;
                    pd.Print();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
      
        private static void CreateListCheck(int ItemsCount)
        {
            LoY = 0;
            LogoCheck = false;
            BarCodeCheck = false;
            HeadderCheck = new bool[((List<LineStructure>)ListReturn(Properties.Settings.Default.ReceiptPrinterSetting)[0]).Count];
            FootterCHeck = new bool[((List<LineStructure>)ListReturn(Properties.Settings.Default.ReceiptPrinterFooter)[0]).Count];
            DetailsCheck = new bool[ItemsCount];
            ListEnding = new bool[] { false,false, false, false, false, false, false, false,false,false,false ,false,false,false,false};
            for (int i = 0; i < HeadderCheck.Length; i++)
            {
                HeadderCheck[i] = false;
            }
            for (int i = 0; i < FootterCHeck.Length; i++)
            {
                FootterCHeck[i] = false;
            }
            for (int i = 0; i < DetailsCheck.Length; i++)
            {
                DetailsCheck[i] = false;
            }
        }
        private static int PrintHeadderF(PrintPageEventArgs args, PrintDocument pd, Font FontIn, String DrawString, ref int Y_Location, int typeCt)
        {
            string HeaderString = DrawString;
            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;
            Font HeaderFont = FontIn;//new Font("Curlz MT", 30, FontStyle.Bold);
            PaperSize PPSize = pd.DefaultPageSettings.PaperSize; ;


            StringFormat stringFormat = new StringFormat();
            if (typeCt == 1)
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
            }
            else
                if (typeCt == 2)
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            else
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
            }

            Size HeaderSize = args.Graphics.MeasureString(HeaderString, HeaderFont, WithOfText, stringFormat).ToSize();

            args.Graphics.DrawString(HeaderString, HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText, HeaderSize.Height)), stringFormat);
            //args.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText,HeaderSize.Height)));
            Y_Location += HeaderSize.Height;
            return HeaderSize.Height;
        }
        private static int HeadderSize(PrintPageEventArgs args, PrintDocument pd, Font FontIn, String DrawString, int typeCt)
        {
            string HeaderString = DrawString;
            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;
            Font HeaderFont = FontIn;//new Font("Curlz MT", 30, FontStyle.Bold);

            StringFormat stringFormat = new StringFormat();
            if (typeCt == 1)
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
            }
            else
                if (typeCt == 2)
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            else
            {
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
            }

            Size HeaderSize = args.Graphics.MeasureString(HeaderString, HeaderFont, WithOfText, stringFormat).ToSize();

            return HeaderSize.Height;
        }
        private static int PrintDetailsF(PrintPageEventArgs args, PrintDocument pd, Font FontIn, String DrawString, float Price, ref int Y_Location)
        {
            string HeaderString = DrawString;
            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;
            Font HeaderFont = FontIn;//new Font("Curlz MT", 30, FontStyle.Bold);
            Size HeaderSize = args.Graphics.MeasureString(HeaderString, HeaderFont, WithOfText - 35).ToSize();


            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            args.Graphics.DrawString(HeaderString, HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText - 35, HeaderSize.Height)), stringFormat);

            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;
            if (DrawString != "|")
            {
                string[] priceIn = Price.ToString().Split('.');
                if(priceIn.Length>1)
                    args.Graphics.DrawString("$" + Math.Round(Price, 2).ToString(), HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText, HeaderSize.Height)), stringFormat);
                else
                    args.Graphics.DrawString("$" + Math.Round(Price, 2).ToString()+".00", HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText, HeaderSize.Height)), stringFormat);

            }
            //args.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText,HeaderSize.Height)));
            Y_Location += HeaderSize.Height;
            return HeaderSize.Height;

        }
        private static int PrintDetailsF(PrintPageEventArgs args, PrintDocument pd, Font FontIn, String DrawString, string No, ref int Y_Location)
        {
            string HeaderString = DrawString;
            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;
            Font HeaderFont = FontIn;//new Font("Curlz MT", 30, FontStyle.Bold);
            Size HeaderSize = args.Graphics.MeasureString(HeaderString, HeaderFont, WithOfText - 35).ToSize();


            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            args.Graphics.DrawString(HeaderString, HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText - 35, HeaderSize.Height)), stringFormat);

            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;
            if (DrawString != "|")            {
               
                    args.Graphics.DrawString(No, HeaderFont, Brushes.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText, HeaderSize.Height)), stringFormat);

            }
            //args.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(0, Y_Location), new Size(WithOfText,HeaderSize.Height)));
            Y_Location += HeaderSize.Height;
            return HeaderSize.Height;

        }
        private static int CheckSize(PrintPageEventArgs args, PrintDocument pd, Font FontIn, String DrawString)
        {
            string HeaderString = DrawString;
            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;
            Font HeaderFont = FontIn;//new Font("Curlz MT", 30, FontStyle.Bold);
            Size HeaderSize = args.Graphics.MeasureString(HeaderString, HeaderFont, WithOfText - 35).ToSize();
            return HeaderSize.Height;

        }
        private static int PrintBreakLine(PrintPageEventArgs args, PrintDocument pd, int Space, ref int Y_Location)
        {

            int WithOfText = pd.DefaultPageSettings.PaperSize.Width;

            args.Graphics.DrawLine(Pens.Black, 0, Y_Location, WithOfText, Y_Location);
            Y_Location += Space;
            return Space;
        }

        private static void printPage(object sndr, PrintPageEventArgs args)
        {
            //System.Drawing.Image i = System.Drawing.Image.FromFile("C:\\Users\\QuyIT\\Desktop\\1.jpg");

            string LoadHeadDerString = Properties.Settings.Default.ReceiptPrinterSetting;
            string LoadDetailProString = Properties.Settings.Default.ReceiptPrinterDetails;
            string LoadFooterString = Properties.Settings.Default.ReceiptPrinterFooter;
            List<LineStructure> ListHeaderPro = (List<LineStructure>)ListReturn(LoadHeadDerString)[0];
            List<LineStructure> ListFooterPro = (List<LineStructure>)ListReturn(LoadFooterString)[0];
            LineStructure ListDetailsPro = ((List<LineStructure>)ListReturn(LoadDetailProString)[0])[0];
            //float PageHeight = args.PageSettings.PrintableArea.Height;

            //-------------------------Top logo group-----------------
            if (Properties.Settings.Default.LogoTopBottom == 0 && Properties.Settings.Default.LogoShow == true
                && LogoCheck == false)
            {
                Image DataIMG = LoadImageToMemory(Application.StartupPath + "\\Logo.png");
                if (Properties.Settings.Default.LogoSizeX < pd.DefaultPageSettings.PaperSize.Width)
                {
                    if (Properties.Settings.Default.LogoLeftToRight == 1)
                        args.Graphics.DrawImage(DataIMG,
                            pd.DefaultPageSettings.PaperSize.Width / 2 - Properties.Settings.Default.LogoSizeX / 2, LoY,
                            Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);
                    else if (Properties.Settings.Default.LogoLeftToRight == 0)
                        args.Graphics.DrawImage(DataIMG,
                        0, LoY,
                        Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);
                    else
                        args.Graphics.DrawImage(DataIMG,
                               pd.DefaultPageSettings.PaperSize.Width - Properties.Settings.Default.LogoSizeX, LoY,
                               Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);

                    if (DataIMG != null)
                        LoY += Properties.Settings.Default.LogoSizeY;
                }
                else
                {
                    args.Graphics.DrawImage(DataIMG,
                        0, LoY,
                        pd.DefaultPageSettings.PaperSize.Width, Properties.Settings.Default.LogoSizeY);
                    if (DataIMG != null)
                        LoY += Properties.Settings.Default.LogoSizeY;
                }
                LogoCheck = true;
            }

            //----------------------Barcode top Group-----------
            if (Properties.Settings.Default.BarCodeTopAndBottom == 0 && BarCodeCheck == false)
            {
                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                Image codeIM = barcodeWriter.Write("1234 1123");


                if (Properties.Settings.Default.BarCodeSizeX < pd.DefaultPageSettings.PaperSize.Width)
                {
                    if (Properties.Settings.Default.BarCodeLeftToRight == 1)
                        args.Graphics.DrawImage(codeIM, pd.DefaultPageSettings.PaperSize.Width / 2 - Properties.Settings.Default.BarCodeSizeX / 2, LoY,
                            Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                    else if (Properties.Settings.Default.BarCodeLeftToRight == 0)
                        args.Graphics.DrawImage(codeIM, 0, LoY,
                        Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                    else
                        args.Graphics.DrawImage(codeIM, pd.DefaultPageSettings.PaperSize.Width - Properties.Settings.Default.BarCodeSizeX, LoY,
                        Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                }
                else
                {
                    args.Graphics.DrawImage(codeIM, 0, LoY,
                         pd.DefaultPageSettings.PaperSize.Width, Properties.Settings.Default.BarCodeSizeY);

                }
                LoY += Properties.Settings.Default.BarCodeSizeY;
                BarCodeCheck = true;
            }

            //-------------------------------Headder Group-----------------------
            //foreach (LineStructure rw in ListHeaderPro)
            //{
            //    PrintHeadderF(args, pd, new Font(rw.NameFont, (float)rw.SizeFont, rw.TypeFont), rw.TextData, ref LoY, 2);
            //}
            for (int i = 0; i < ListHeaderPro.Count; i++)
            {
                if (HeadderCheck[i] == false)
                {

                    int HieghtRW = HeadderSize(args, pd, new Font(ListHeaderPro[i].NameFont, (float)ListHeaderPro[i].SizeFont, ListHeaderPro[i].TypeFont), ListHeaderPro[i].TextData, 2);
                    if (LoY + HieghtRW >= HeightAR)
                    {
                        args.HasMorePages = true;
                        PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                        LoY = 0;
                        return;
                    }
                    else
                    {
                        args.HasMorePages = false;
                        PrintHeadderF(args, pd, new Font(ListHeaderPro[i].NameFont, (float)ListHeaderPro[i].SizeFont, ListHeaderPro[i].TypeFont), ListHeaderPro[i].TextData, ref LoY, 2);
                        HeadderCheck[i] = true;

                    }
                }
            }
            //-------------------------Break Line--------------------
            if (ListEnding[6] == false)
            {
                int HieghtRW = PrintBreakLine(args, pd, 10, ref LoY);
                ListEnding[6] = true;
                if (LoY + HieghtRW >= HeightAR)
                {
                    args.HasMorePages = true;
                    LoY = HieghtRW;
                    return;
                }
                else
                    args.HasMorePages = false;
            }
            //------------------------------Details---------------------
            for (int ii = 0; ii < DetailsCheck.Length; ii++)
            {
                if (DetailsCheck[ii] == false)
                {
                    int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), ReceiptList.Rows[ii][3].ToString());


                    if (LoY + HieghtRW >= HeightAR)
                    {
                        args.HasMorePages = true;
                        PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                        LoY = 0;
                        return;
                    }
                    else
                    {
                        args.HasMorePages = false;
                        DetailsCheck[ii] = true;
                        PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), ReceiptList.Rows[ii][3].ToString(), (float)Convert.ToDouble(ReceiptList.Rows[ii][5].ToString()), ref LoY);
                    }
                }
            }
            //----------------------------------- anding group-------
            if (ListEnding[0] == false)
            {
                LoY += 10;
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), " "+SubTotalW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    args.HasMorePages = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    LoY = 0;
                    return;
                }
                else
                {
                    ListEnding[0] = true;
                    args.HasMorePages = false;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), " "+SubTotalW,SubTotalN, ref LoY);

                }
            }
            if (ListEnding[7] == false && DiscountN!=0)
            {
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), " "+DiscountW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[7] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, FontStyle.Regular), " " + DiscountW, DiscountN, ref LoY);

                }
            }
            if (ListEnding[1] == false)
            {
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), " " +RoundingW+ ReceiptList.Rows.Count);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[1] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont , FontStyle.Regular), " " +RoundingW+ ReceiptList.Rows.Count , RoundingN, ref LoY);

                }
            }
            if (ListEnding[2] == false)
            {
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 2, FontStyle.Bold|FontStyle.Underline), " "+TotalW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 2, FontStyle.Bold | FontStyle.Underline), " "+TotalW, TotalN, ref LoY);
                    ListEnding[2] = true;

                }
            }
            if (ListEnding[3] == false && CashN != 0)
            {
                LoY += 10;
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " "+CashW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + CashW, CashN, ref LoY);
                    ListEnding[3] = true;

                }
            }
            if (ListEnding[8] == false&& EftposN!=0)
            {
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + EftposW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[8] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + EftposW, EftposN, ref LoY);

                }
            }
            if (ListEnding[4] == false)
            {
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " "+ ChangeW);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[4] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " "+ChangeW,ChangeN, ref LoY);

                }
            }
            //------------------------loyalty card-----------------
            if (ListEnding[13] == false)
            {
                LoY += 10;
                int HieghtRW = PrintBreakLine(args, pd, 10, ref LoY);
                ListEnding[13] = true;
                if (LoY+ HieghtRW >= HeightAR)
                {
                    args.HasMorePages = true;
                    LoY = HieghtRW;
                    return;
                }
                else
                    args.HasMorePages = false;
            }

            if (ListEnding[11] == false && LoyaltyCardN.Length > 0)
            {
                int HieghtRW = HeadderSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont+1, FontStyle.Bold), LoyaltyTitleW, 2);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    ListEnding[11] = true;
                    args.HasMorePages = false;
                    PrintHeadderF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont+1, FontStyle.Bold), LoyaltyTitleW, ref LoY, 2);

                }
            }
            if (ListEnding[10] == false && LoyaltyCardN.Length>0)
            {
                
                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + LoyaltyCardW);
                if (LoY+ HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[10] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + LoyaltyCardW,LoyaltyCardN , ref LoY);

                }
            }
            if (ListEnding[12] == false && LoyaltyCardN.Length > 0)
            {

                int HieghtRW = CheckSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " +LoyaltyBalanceW );
                if (LoY+ HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    args.HasMorePages = false;
                    ListEnding[12] = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Regular), " " + LoyaltyBalanceW, LoyaltyBalanceN+"/"+LoyaltyRequidN, ref LoY);

                }
            }
            if (ListEnding[14] == false && LoyaltyCardN.Length > 0 && LoyaltyBalanceN==LoyaltyRequidN)
            {
                LoY += 10;
                int HieghtRW = HeadderSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), MessageLoyaltyW, 2);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    ListEnding[14] = true;
                    args.HasMorePages = false;
                    PrintHeadderF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont + 1, FontStyle.Bold), MessageLoyaltyW, ref LoY, 2);

                }
            }
            //---------------------------------------------------------
            if (ListEnding[9] == false)
            {
                LoY += 10;
                int HieghtRW = HeadderSize(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, FontStyle.Italic), EndingDateW,  2);
                if (LoY + HieghtRW >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    ListEnding[9] = true;
                    args.HasMorePages = false;
                    PrintHeadderF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, FontStyle.Italic), EndingDateW , ref LoY, 2);

                }
            }

            if (ListEnding[5] == false)
            {
                int HieghtRW = PrintBreakLine(args, pd, 10, ref LoY);
                ListEnding[5] = true;
                if (LoY + HieghtRW >= HeightAR)
                {
                    args.HasMorePages = true;
                    LoY = HieghtRW;
                    return;
                }
                else
                    args.HasMorePages = false;
            }
            
            //------------------------------------Footer group--------------------

            //foreach (LineStructure rw in ListFooterPro)
            //{
            //    PrintHeadderF(args, pd, new Font(rw.NameFont, (float)rw.SizeFont, rw.TypeFont), rw.TextData, ref LoY, 2);
            //}
            for (int i = 0; i < ListFooterPro.Count; i++)
            {
                if (FootterCHeck[i] == false)
                {
                    int HieghtRW = HeadderSize(args, pd, new Font(ListFooterPro[i].NameFont, (float)ListFooterPro[i].SizeFont, ListFooterPro[i].TypeFont), ListFooterPro[i].TextData, 2);
                    if (LoY + HieghtRW >= HeightAR)
                    {
                        PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                        args.HasMorePages = true;
                        LoY = 0;
                        return;
                    }
                    else
                    {
                        FootterCHeck[i] = true;
                        args.HasMorePages = false;
                        PrintHeadderF(args, pd, new Font(ListFooterPro[i].NameFont, (float)ListFooterPro[i].SizeFont, ListFooterPro[i].TypeFont), ListFooterPro[i].TextData, ref LoY, 2);

                    }
                }
            }

            //----------------Logo Group--------------------------------------
            if (Properties.Settings.Default.LogoTopBottom == 1 && Properties.Settings.Default.LogoShow == true && LogoCheck == false)
            {
                Image DataIMG = LoadImageToMemory(Application.StartupPath + "\\Logo.png");
                if (LoY + Properties.Settings.Default.LogoSizeY >= HeightAR)
                {
                    args.HasMorePages = true;
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    LoY = 0;
                    return;
                }
                else
                {

                    if (Properties.Settings.Default.LogoSizeX < pd.DefaultPageSettings.PaperSize.Width)
                    {
                        if (Properties.Settings.Default.LogoLeftToRight == 1)
                            args.Graphics.DrawImage(DataIMG,
                                pd.DefaultPageSettings.PaperSize.Width / 2 - Properties.Settings.Default.LogoSizeX / 2, LoY,
                                Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);
                        else if (Properties.Settings.Default.LogoLeftToRight == 0)
                            args.Graphics.DrawImage(DataIMG,
                            0, LoY,
                            Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);
                        else
                            args.Graphics.DrawImage(DataIMG,
                                   pd.DefaultPageSettings.PaperSize.Width - Properties.Settings.Default.LogoSizeX, LoY,
                                   Properties.Settings.Default.LogoSizeX, Properties.Settings.Default.LogoSizeY);
                        if (DataIMG != null)
                            LoY += Properties.Settings.Default.LogoSizeY;
                    }
                    else
                    {
                        args.Graphics.DrawImage(DataIMG,
                            0, LoY,
                            pd.DefaultPageSettings.PaperSize.Width, Properties.Settings.Default.LogoSizeY);
                        if (DataIMG != null)
                            LoY += Properties.Settings.Default.LogoSizeY;
                    }
                    LogoCheck = true;
                    args.HasMorePages = false;

                }
            }
            //------------BarCode Group------------------------
            if (Properties.Settings.Default.BarCodeTopAndBottom == 1 && BarCodeCheck == false)
            {
                var barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.DATA_MATRIX;
                Image codeIM = barcodeWriter.Write("501054530107");
                if (LoY + Properties.Settings.Default.BarCodeSizeY >= HeightAR)
                {
                    PrintDetailsF(args, pd, new Font(ListDetailsPro.NameFont, (float)ListDetailsPro.SizeFont, ListDetailsPro.TypeFont), "|", -1, ref LoY);
                    args.HasMorePages = true;
                    LoY = 0;
                    return;
                }
                else
                {
                    if (Properties.Settings.Default.BarCodeSizeX < pd.DefaultPageSettings.PaperSize.Width)
                    {
                        if (Properties.Settings.Default.BarCodeLeftToRight == 1)
                            args.Graphics.DrawImage(codeIM, pd.DefaultPageSettings.PaperSize.Width / 2 - Properties.Settings.Default.BarCodeSizeX / 2, LoY,
                                Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                        else if (Properties.Settings.Default.BarCodeLeftToRight == 0)
                            args.Graphics.DrawImage(codeIM, 0, LoY,
                            Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                        else
                            args.Graphics.DrawImage(codeIM, pd.DefaultPageSettings.PaperSize.Width - Properties.Settings.Default.BarCodeSizeX, LoY,
                            Properties.Settings.Default.BarCodeSizeX, Properties.Settings.Default.BarCodeSizeY);
                    }
                    else
                    {
                        args.Graphics.DrawImage(codeIM, 0, LoY,
                             pd.DefaultPageSettings.PaperSize.Width, Properties.Settings.Default.BarCodeSizeY);

                    }
                    LoY += Properties.Settings.Default.BarCodeSizeY;
                    BarCodeCheck = true;
                    args.HasMorePages = false;

                }

            }
        }

    }
}
