USE [master]
GO

/****** Object:  Database [IlCats]    Script Date: 2023-05-23 10:52:19 ******/
CREATE DATABASE [IlCats]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IlCats', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IlCats.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IlCats_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IlCats_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IlCats].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IlCats] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IlCats] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IlCats] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IlCats] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IlCats] SET ARITHABORT OFF 
GO

ALTER DATABASE [IlCats] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IlCats] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IlCats] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IlCats] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IlCats] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IlCats] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IlCats] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IlCats] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IlCats] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IlCats] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IlCats] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IlCats] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IlCats] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IlCats] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [IlCats] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IlCats] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [IlCats] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IlCats] SET RECOVERY FULL 
GO

ALTER DATABASE [IlCats] SET  MULTI_USER 
GO

ALTER DATABASE [IlCats] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IlCats] SET DB_CHAINING OFF 
GO

ALTER DATABASE [IlCats] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [IlCats] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [IlCats] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [IlCats] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [IlCats] SET QUERY_STORE = ON
GO

ALTER DATABASE [IlCats] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [IlCats] SET  READ_WRITE 
GO


USE [IlCats]
GO

/****** Object:  Table [dbo].[Manufacturers]    Script Date: 2023-05-23 10:52:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Manufacturers](
	[id] [nchar](30) NOT NULL,
	[name] [nchar](50) NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [IlCats]
GO

/****** Object:  Table [dbo].[Models]    Script Date: 2023-05-23 10:52:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Models](
	[manufacturer_id] [nchar](30) NOT NULL,
	[id] [nchar](10) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[production_start_date] [date] NOT NULL,
	[production_end_date] [date] NULL,
	[model_code] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Models] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Models]  WITH CHECK ADD  CONSTRAINT [FK_Models_Manufacturers] FOREIGN KEY([manufacturer_id])
REFERENCES [dbo].[Manufacturers] ([id])
GO

ALTER TABLE [dbo].[Models] CHECK CONSTRAINT [FK_Models_Manufacturers]
GO

USE [IlCats]
GO

/****** Object:  Table [dbo].[Complectations]    Script Date: 2023-05-23 10:53:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Complectations](
	[model_id] [nchar](10) NOT NULL,
	[id] [nchar](20) NOT NULL,
	[production_start_date] [date] NOT NULL,
	[production_end_date] [date] NULL,
	[AtmMtm] [nchar](10) NULL,
	[BackDoor] [nchar](10) NULL,
	[Body] [nchar](10) NULL,
	[Body1] [nchar](10) NULL,
	[Body2] [nchar](10) NULL,
	[BodyShape] [nchar](10) NULL,
	[BuildingCondition] [nchar](10) NULL,
	[Cab] [nchar](10) NULL,
	[Category] [nchar](10) NULL,
	[Cooler] [nchar](10) NULL,
	[Deck] [nchar](10) NULL,
	[DeckCab] [nchar](10) NULL,
	[DeckMaterial] [nchar](10) NULL,
	[Destination] [nchar](10) NULL,
	[Destination1] [nchar](10) NULL,
	[Destination2] [nchar](10) NULL,
	[DriversPosition] [nchar](10) NULL,
	[EmissionRegulation] [nchar](10) NULL,
	[Engine1] [nchar](10) NULL,
	[Engine2] [nchar](10) NULL,
	[FuelInduction] [nchar](10) NULL,
	[GearShiftType] [nchar](10) NULL,
	[Grade] [nchar](10) NULL,
	[GradeMark] [nchar](10) NULL,
	[IncompleteVehicles] [nchar](10) NULL,
	[LoadingCapacity] [nchar](10) NULL,
	[ModelMark] [nchar](10) NULL,
	[NoOfDoors] [nchar](10) NULL,
	[Product] [nchar](10) NULL,
	[RearSuspention] [nchar](10) NULL,
	[RearTire] [nchar](10) NULL,
	[Rollbar] [nchar](10) NULL,
	[Roof] [nchar](10) NULL,
	[SeatType] [nchar](10) NULL,
	[SeatingCapacity] [nchar](10) NULL,
	[SideWindow] [nchar](10) NULL,
	[TopBackDoor] [nchar](10) NULL,
	[TransmissionModel] [nchar](10) NULL,
	[TruckOrVan] [nchar](10) NULL,
	[VehicleModel] [nchar](10) NULL,
 CONSTRAINT [PK_Complectations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Complectations]  WITH CHECK ADD  CONSTRAINT [FK_Complectations_Models] FOREIGN KEY([model_id])
REFERENCES [dbo].[Models] ([id])
GO

ALTER TABLE [dbo].[Complectations] CHECK CONSTRAINT [FK_Complectations_Models]
GO


