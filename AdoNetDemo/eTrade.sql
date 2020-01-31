USE [eTrade]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 31.01.2020 14:23:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[StockAmount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount]) VALUES (1, N'Pencil', 10.0000, 100)
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount]) VALUES (2, N'Mouse', 12.0000, 100)
INSERT [dbo].[Products] ([Id], [Name], [UnitPrice], [StockAmount]) VALUES (3, N'Screen', 15.0000, 50)
SET IDENTITY_INSERT [dbo].[Products] OFF
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 31.01.2020 14:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddProduct]
-- Add the parameters for the stored procedure here
	@name NVARCHAR(30),
	@unitPrice MONEY,
	@stockAmount INTEGER
AS
BEGIN
	INSERT INTO Products
	VALUES
	  (
	    @name,
	    @unitPrice,
	    @stockAmount
	  );
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ListProduct]    Script Date: 31.01.2020 14:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_ListProduct]
AS
BEGIN
	SELECT * FROM  Products
END
GO
