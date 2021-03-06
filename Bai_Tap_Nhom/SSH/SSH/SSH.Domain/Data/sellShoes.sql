USE [master]
GO
/****** Object:  Database [SellShoes]    Script Date: 12/17/2019 8:40:33 PM ******/
CREATE DATABASE [SellShoes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SellShoes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SellShoes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SellShoes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SellShoes_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SellShoes] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SellShoes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SellShoes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SellShoes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SellShoes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SellShoes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SellShoes] SET ARITHABORT OFF 
GO
ALTER DATABASE [SellShoes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SellShoes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SellShoes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SellShoes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SellShoes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SellShoes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SellShoes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SellShoes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SellShoes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SellShoes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SellShoes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SellShoes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SellShoes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SellShoes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SellShoes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SellShoes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SellShoes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SellShoes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SellShoes] SET  MULTI_USER 
GO
ALTER DATABASE [SellShoes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SellShoes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SellShoes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SellShoes] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SellShoes] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SellShoes', N'ON'
GO
ALTER DATABASE [SellShoes] SET QUERY_STORE = OFF
GO
USE [SellShoes]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](100) NULL,
	[ParenID] [int] NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[BrandID] [int] NULL,
	[CategoryID] [int] NULL,
	[Price] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Brand]  WITH CHECK ADD  CONSTRAINT [FK_Brand_Category] FOREIGN KEY([ParenID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Brand] CHECK CONSTRAINT [FK_Brand_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
/****** Object:  StoredProcedure [dbo].[ListBrand]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 15/12/2019
-- Description:	Get Category
-- =============================================
CREATE PROCEDURE [dbo].[ListBrand]
	-- Add the parameters for the stored procedure here
AS
BEGIN
SELECT [ID],
	   [BrandName],
	   [ParenID],
	   (SELECT COUNT(*) FROM Product WHERE BrandID = ID) AS LoaiSanPham
FROM [dbo].[Brand]
END
GO
/****** Object:  StoredProcedure [dbo].[ListCategory]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 15/12/2019
-- Description:	Get Category
-- =============================================
CREATE PROCEDURE [dbo].[ListCategory]
	-- Add the parameters for the stored procedure here
AS
BEGIN
SELECT [ID],
	   [CategoryName],
	   (SELECT COUNT(*) FROM Product WHERE CategoryID = ID) AS TongSoSanPham
FROM [dbo].[Category]
END
GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteProductById]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 11/12/2019
-- Description:	Delete Product By Id
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteProductById]
	-- Add the parameters for the stored procedure here
	@ID INT
AS
BEGIN
	DECLARE @Result INT = 0

	BEGIN TRY
			
		DELETE FROM  [dbo].[Product] WHERE ID = @ID
		
		SET @Result = 1
			
	END TRY
	BEGIN CATCH
	END CATCH

	SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[proc_GetProductById]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 11/12/2019
-- Description:	Get Product By Id
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetProductById]
	-- Add the parameters for the stored procedure here
	@ID INT
AS
BEGIN
SELECT [ID]
      ,[ProductName]
      ,[BrandID]
      ,[CategoryID]
      ,[Price]
  FROM [dbo].[Product]
  WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[proc_GetProductByName]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 11/12/2019
-- Description:	Get Product By Name
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetProductByName] 
	-- Add the parameters for the stored procedure here
	@ProductName NVARCHAR(100)
AS
BEGIN
	SELECT * FROM [dbo].[Product]
	WHERE ProductName = @ProductName

END
GO
/****** Object:  StoredProcedure [dbo].[proc_GetProducts]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 11/12/2019
-- Description:	Get Products
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetProducts]
	-- Add the parameters for the stored procedure here

AS
BEGIN
SELECT [Product].[ID]
      ,[ProductName]
      ,[BrandID]
      ,[CategoryID]
      ,[Price]
	  ,[Category].[CategoryName]
	  ,[Brand].BrandName
  FROM [dbo].[Product]
  JOIN [dbo].[Category]
  ON Product.CategoryID = Category.ID
  JOIN [dbo].[Brand]
  ON Product.[BrandID] = Brand.ID
END
GO
/****** Object:  StoredProcedure [dbo].[proc_SaveProduct]    Script Date: 12/17/2019 8:40:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thanh Thanh
-- Create date: 11/12/2019
-- Description:	Create/ Update Product
-- =============================================
CREATE PROCEDURE [dbo].[proc_SaveProduct]
	-- Add the parameters for the stored procedure here
	@ID INT = 0,
	@ProductName NVARCHAR(100),
	@BrandID INT,
	@CategoryID INT,
	@Price DECIMAL(18,2)
AS
BEGIN
	DECLARE @Result INT = 0

	BEGIN TRY
		IF(@ID =0)
			BEGIN
				INSERT INTO [dbo].[Product]
						   ([ProductName]
						   ,[BrandID]
						   ,[CategoryID]
						   ,[Price])
				VALUES
						   (@ProductName
						   ,@BrandID
						   ,@CategoryID
						   ,@Price)

				SET @Result = 1
			END
		ELSE
			BEGIN
				UPDATE [dbo].[Product]
				   SET ProductName = @ProductName
					  ,BrandID = @BrandID
					  ,CategoryID = @CategoryID
					  ,Price = @Price
				 WHERE ID = @ID

				 SET @Result = 2
			END
	END TRY
	BEGIN CATCH
	END CATCH

	SELECT @Result AS Result
END
GO
USE [master]
GO
ALTER DATABASE [SellShoes] SET  READ_WRITE 
GO
