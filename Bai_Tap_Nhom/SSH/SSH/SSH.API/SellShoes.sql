USE [master]
GO
/****** Object:  Database [SellShoes]    Script Date: 14/12/2019 3:28:48 PM ******/
CREATE DATABASE [SellShoes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SellShoes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SellShoes.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SellShoes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SellShoes_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
USE [SellShoes]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 14/12/2019 3:28:49 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 14/12/2019 3:28:49 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 14/12/2019 3:28:49 PM ******/
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
/****** Object:  StoredProcedure [dbo].[proc_DeleteBrandID]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Quy Dep Trai VCL
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[proc_DeleteBrandID]
	@ID INT
AS
BEGIN
	DECLARE @Result INT = 0
	IF NOT EXISTS(SELECT * FROM dbo.Brand WHERE ID = @ID)
		RETURN @Result
	BEGIN TRY
		BEGIN
		    DELETE FROM dbo.Brand
			WHERE ID = @ID
		END
		SET @Result = 1
    END TRY 
	BEGIN CATCH
		PRINT N'Lỗi!'
			PRINT N'Mã Lỗi: '   + CAST(ERROR_NUMBER() AS VARCHAR (50))  /*Xuất mã lỗi số*/
			PRINT N'Chi Tiết: ' + CAST(ERROR_MESSAGE() AS VARCHAR (MAX)) /*Xuất mã lỗi chuỗi*/
	END CATCH
	SELECT @Result
END

GO
/****** Object:  StoredProcedure [dbo].[proc_DeleteCategoryID]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_DeleteCategoryID] 
	@ID INT
AS
BEGIN
	IF EXISTS(SELECT * FROM dbo.Brand AS b JOIN dbo.Product AS p ON p.BrandID = b.ID 
										   WHERE b.ParenID = @ID AND p.CategoryID = @ID)
		RETURN 0
	BEGIN TRY
		DELETE FROM [dbo].[Category]
			   WHERE ID = @ID
    END TRY
	BEGIN CATCH
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[proc_GetBrandByCategory]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lê Ngọc Quý
-- Create date: 14/12/2019
-- Description:	GetByCategory
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetBrandByCategory]
	@ID INT
AS
BEGIN
	SELECT b.ID,b.BrandName, b.ParenID FROM dbo.Category c JOIN dbo.Brand b
	ON b.ParenID = c.ID
	WHERE b.ParenID = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetCategoryByID]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_GetCategoryByID]
	@ID INT
AS
BEGIN
	SELECT [ID],[CategoryName] FROM [dbo].[Category]
	WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[proc_GetDataBrand]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_GetDataBrand]
AS
BEGIN
	SELECT [ID]
		  ,[BrandName]
		  ,[ParenID]
	  FROM [dbo].[Brand]
END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetDataBrandID]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_GetDataBrandID]
	@ID INT
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM dbo.Brand WHERE ID = @ID)
		RETURN 0
	SELECT [ID]
		  ,[BrandName]
		  ,[ParenID]
	  FROM [dbo].[Brand]
	WHERE ID = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetDataCategory]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lê Ngọc Quý
-- Create date: 13/12/2019
-- Description:	CRUD
-- =============================================
CREATE PROCEDURE [dbo].[proc_GetDataCategory]
AS
BEGIN
	SELECT [ID],[CategoryName] FROM [dbo].[Category]
END

GO
/****** Object:  StoredProcedure [dbo].[proc_GetProductByCategory]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_GetProductByCategory]
	@ID INT
AS
BEGIN
	SELECT p.ProductName, p.BrandID, p.CategoryID, p.Price FROM dbo.Category c JOIN dbo.Product p
	ON p.CategoryID = c.ID
	WHERE p.CategoryID = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[proc_SaveBrand]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_SaveBrand] 
	@ID INT = 0,
	@BrandName NVARCHAR(100),
	@ParenID INT
AS
BEGIN
	DECLARE @Result INT = 0
	BEGIN TRY
		IF NOT EXISTS(SELECT * FROM dbo.Category WHERE ID = @ParenID)
				RETURN @Result
		IF(@ID = 0)
			BEGIN
				INSERT INTO [dbo].[Brand]
				(
				    [BrandName],
				    [ParenID]
				)
				VALUES
				(   @BrandName, -- BrandName - nvarchar(100)
				    @ParenID    -- ParenID - int
				    )
				SET @Result = 1
            END
		ELSE
			BEGIN
				UPDATE [dbo].[Brand]
				   SET [BrandName] = @BrandName,
				       [ParenID] = @ParenID
				WHERE ID = @ID
				SET @Result = 2
			END
    END TRY
	BEGIN CATCH
	END CATCH
	SELECT @Result AS Result
END

GO
/****** Object:  StoredProcedure [dbo].[proc_SaveCategory]    Script Date: 14/12/2019 3:28:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[proc_SaveCategory] 
	@ID INT = 0,
	@CategoryName NVARCHAR(100)
AS
BEGIN
	DECLARE @Result INT = 0,
			@Message NVARCHAR(100) = N'Đã xảy ra lỗi vui lòng kiểm tra lại!'
	IF EXISTS(SELECT * FROM dbo.Category WHERE CategoryName = @CategoryName)
		RETURN @Message
	BEGIN TRY
		IF( @ID = 0)
			BEGIN
			 INSERT INTO [dbo].[Category] ([CategoryName])
				    VALUES (@CategoryName)
			SET @Result = 1
			SELECT @Message = N'Khởi tạo thành công!'
			END
		ELSE
			BEGIN
			    UPDATE [dbo].[Category]
				   SET [CategoryName] = @CategoryName
				 WHERE ID = @ID
			SET @Result = 2
			SELECT @Message = N'Cập nhập thành công!'
			END
    END TRY
	BEGIN CATCH
	END CATCH
	SELECT @Result AS Result, @Message AS [Message]
END

GO
USE [master]
GO
ALTER DATABASE [SellShoes] SET  READ_WRITE 
GO
