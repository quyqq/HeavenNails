USE [HeavenNails]
GO
/****** Object:  Table [dbo].[tb_Receipt]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Receipt](
	[RIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Time] [nvarchar](10) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Total] [numeric](18, 2) NOT NULL,
	[Cash] [money] NOT NULL,
	[Eftpos] [money] NOT NULL,
	[GivenCash] [money] NOT NULL,
	[Change] [money] NOT NULL,
 CONSTRAINT [PK_tb_Receipt] PRIMARY KEY CLUSTERED 
(
	[RIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Card]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Card](
	[CIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BarCode] [nvarchar](200) NOT NULL,
	[ScanTime] [int] NOT NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_tb_Card] PRIMARY KEY CLUSTERED 
(
	[CIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_LoyaltyCard]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_LoyaltyCard](
	[LIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Addition] [money] NOT NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_tb_LoyaltyCard] PRIMARY KEY CLUSTERED 
(
	[LIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Supplier]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Supplier](
	[SUIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[NameCompany] [nvarchar](200) NOT NULL,
	[EMail] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NULL,
	[Adress] [nvarchar](350) NULL,
	[OtherInf] [nvarchar](max) NULL,
 CONSTRAINT [PK_tb_Supplier] PRIMARY KEY CLUSTERED 
(
	[SUIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Sevices]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Sevices](
	[SIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[NameService] [nvarchar](200) NOT NULL,
	[Cost] [money] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tb_Sevices] PRIMARY KEY CLUSTERED 
(
	[SIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ServiceReciept]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ServiceReciept](
	[SRIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Cost] [money] NOT NULL,
	[RIndex] [numeric](18, 0) NOT NULL,
	[SIndex] [numeric](18, 0) NOT NULL,
	[NameService] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_tb_ServiceReciept] PRIMARY KEY CLUSTERED 
(
	[SRIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ImportBills]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ImportBills](
	[IMIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SUIndex] [numeric](18, 0) NOT NULL,
	[Total] [money] NOT NULL,
	[ImageBill] [image] NULL,
 CONSTRAINT [PK_tb_ImportBills] PRIMARY KEY CLUSTERED 
(
	[IMIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Item]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Item](
	[IIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[NameItem] [nvarchar](200) NOT NULL,
	[ImportPrice] [money] NOT NULL,
	[SellingPrice] [money] NOT NULL,
	[IMIndex] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_tb_Item] PRIMARY KEY CLUSTERED 
(
	[IIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_QuantityItem]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_QuantityItem](
	[BarCode] [nvarchar](200) NOT NULL,
	[Sold] [bit] NOT NULL,
	[IIndex] [numeric](18, 0) NOT NULL,
	[QIIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QIIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNQ_BarCode] UNIQUE NONCLUSTERED 
(
	[BarCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ItemSoldReciept]    Script Date: 10/29/2018 20:36:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ItemSoldReciept](
	[SOIndex] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[SellingPrice] [money] NOT NULL,
	[QIIndex] [numeric](18, 0) NOT NULL,
	[RIndex] [numeric](18, 0) NOT NULL,
	[ImportPrice] [money] NOT NULL,
 CONSTRAINT [PK_tb_ItemSold] PRIMARY KEY CLUSTERED 
(
	[SOIndex] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_tb_Card_ScanTime]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_Card] ADD  CONSTRAINT [DF_tb_Card_ScanTime]  DEFAULT ((0)) FOR [ScanTime]
GO
/****** Object:  Default [DF_tb_LoyaltyCard_ScanTime]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_LoyaltyCard] ADD  CONSTRAINT [DF_tb_LoyaltyCard_ScanTime]  DEFAULT ((0)) FOR [Addition]
GO
/****** Object:  Default [DF_tb_QuantityItem_Sole]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_QuantityItem] ADD  CONSTRAINT [DF_tb_QuantityItem_Sole]  DEFAULT ((0)) FOR [Sold]
GO
/****** Object:  Default [DF_tb_Sevices_Active]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_Sevices] ADD  CONSTRAINT [DF_tb_Sevices_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  ForeignKey [FK_tb_ImportBills_tb_Supplier]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_ImportBills]  WITH CHECK ADD  CONSTRAINT [FK_tb_ImportBills_tb_Supplier] FOREIGN KEY([SUIndex])
REFERENCES [dbo].[tb_Supplier] ([SUIndex])
GO
ALTER TABLE [dbo].[tb_ImportBills] CHECK CONSTRAINT [FK_tb_ImportBills_tb_Supplier]
GO
/****** Object:  ForeignKey [FK_tb_Item_tb_ImportBills]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_Item]  WITH CHECK ADD  CONSTRAINT [FK_tb_Item_tb_ImportBills] FOREIGN KEY([IMIndex])
REFERENCES [dbo].[tb_ImportBills] ([IMIndex])
GO
ALTER TABLE [dbo].[tb_Item] CHECK CONSTRAINT [FK_tb_Item_tb_ImportBills]
GO
/****** Object:  ForeignKey [FK_tb_ItemSold_tb_Receipt]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_ItemSoldReciept]  WITH CHECK ADD  CONSTRAINT [FK_tb_ItemSold_tb_Receipt] FOREIGN KEY([RIndex])
REFERENCES [dbo].[tb_Receipt] ([RIndex])
GO
ALTER TABLE [dbo].[tb_ItemSoldReciept] CHECK CONSTRAINT [FK_tb_ItemSold_tb_Receipt]
GO
/****** Object:  ForeignKey [FK_tb_ItemSoldReciept_tb_QuantityItem]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_ItemSoldReciept]  WITH CHECK ADD  CONSTRAINT [FK_tb_ItemSoldReciept_tb_QuantityItem] FOREIGN KEY([QIIndex])
REFERENCES [dbo].[tb_QuantityItem] ([QIIndex])
GO
ALTER TABLE [dbo].[tb_ItemSoldReciept] CHECK CONSTRAINT [FK_tb_ItemSoldReciept_tb_QuantityItem]
GO
/****** Object:  ForeignKey [FK_tb_QuantityItem_tb_Item]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_QuantityItem]  WITH CHECK ADD  CONSTRAINT [FK_tb_QuantityItem_tb_Item] FOREIGN KEY([IIndex])
REFERENCES [dbo].[tb_Item] ([IIndex])
GO
ALTER TABLE [dbo].[tb_QuantityItem] CHECK CONSTRAINT [FK_tb_QuantityItem_tb_Item]
GO
/****** Object:  ForeignKey [FK_tb_ServiceReciept_tb_Receipt]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_ServiceReciept]  WITH CHECK ADD  CONSTRAINT [FK_tb_ServiceReciept_tb_Receipt] FOREIGN KEY([RIndex])
REFERENCES [dbo].[tb_Receipt] ([RIndex])
GO
ALTER TABLE [dbo].[tb_ServiceReciept] CHECK CONSTRAINT [FK_tb_ServiceReciept_tb_Receipt]
GO
/****** Object:  ForeignKey [FK_tb_ServiceReciept_tb_Sevices]    Script Date: 10/29/2018 20:36:43 ******/
ALTER TABLE [dbo].[tb_ServiceReciept]  WITH CHECK ADD  CONSTRAINT [FK_tb_ServiceReciept_tb_Sevices] FOREIGN KEY([SIndex])
REFERENCES [dbo].[tb_Sevices] ([SIndex])
GO
ALTER TABLE [dbo].[tb_ServiceReciept] CHECK CONSTRAINT [FK_tb_ServiceReciept_tb_Sevices]
GO
