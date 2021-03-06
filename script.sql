USE [master]
GO
/****** Object:  Database [Transport]    Script Date: 10.3.2021. 0:05:27 ******/
CREATE DATABASE [Transport]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Transport', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Transport.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Transport_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Transport_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Transport] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Transport].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Transport] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Transport] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Transport] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Transport] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Transport] SET ARITHABORT OFF 
GO
ALTER DATABASE [Transport] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Transport] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Transport] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Transport] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Transport] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Transport] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Transport] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Transport] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Transport] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Transport] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Transport] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Transport] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Transport] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Transport] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Transport] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Transport] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Transport] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Transport] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Transport] SET  MULTI_USER 
GO
ALTER DATABASE [Transport] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Transport] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Transport] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Transport] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Transport] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Transport] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Transport] SET QUERY_STORE = OFF
GO
USE [Transport]
GO
/****** Object:  Table [dbo].[AuditFile]    Script Date: 10.3.2021. 0:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditFile](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timeSheetKey] [nvarchar](50) NOT NULL,
	[day] [nvarchar](50) NOT NULL,
	[field] [nvarchar](50) NOT NULL,
	[oldValue] [nvarchar](50) NOT NULL,
	[newValue] [nvarchar](50) NOT NULL,
	[dateOfChange] [date] NOT NULL,
	[changedBy] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_auditFile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[personalNo] [int] NULL,
	[userName] [nvarchar](50) NOT NULL,
	[PW] [int] NULL,
	[name] [nvarchar](50) NOT NULL,
	[FM#] [nvarchar](50) NOT NULL,
	[level_] [nvarchar](50) NOT NULL,
	[entryDate] [date] NOT NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheets]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NULL,
	[start_time] [nvarchar](50) NULL,
	[end_time] [nvarchar](50) NULL,
	[breaks] [nvarchar](50) NULL,
	[work_time] [nvarchar](50) NULL,
	[m3] [int] NULL,
	[km_stand] [int] NULL,
	[privat] [int] NULL,
	[fuel] [int] NULL,
	[adblue] [int] NULL,
	[notes] [nvarchar](max) NULL,
	[employeeId] [int] NOT NULL,
	[entryDate] [date] NULL,
	[timeSheetKey] [nvarchar](50) NULL,
	[timeSheetStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_TimeSheets] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AuditFile] ON 

INSERT [dbo].[AuditFile] ([id], [timeSheetKey], [day], [field], [oldValue], [newValue], [dateOfChange], [changedBy]) VALUES (42, N'User1-3-2021', N'02', N'Start Time', N'16:30', N'16:45', CAST(N'2021-03-09' AS Date), N'Admin')
SET IDENTITY_INSERT [dbo].[AuditFile] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([id], [personalNo], [userName], [PW], [name], [FM#], [level_], [entryDate], [email], [password]) VALUES (64, NULL, N'Admin', NULL, N'Admin', N'123', N'Admin', CAST(N'2021-03-09' AS Date), NULL, N'123')
INSERT [dbo].[Employee] ([id], [personalNo], [userName], [PW], [name], [FM#], [level_], [entryDate], [email], [password]) VALUES (66, NULL, N'SuperUser1', NULL, N'SuperUser1', N'123', N'SuperUser', CAST(N'2021-03-09' AS Date), NULL, N'123')
INSERT [dbo].[Employee] ([id], [personalNo], [userName], [PW], [name], [FM#], [level_], [entryDate], [email], [password]) VALUES (68, NULL, N'User1', NULL, N'User1', N'123', N'User', CAST(N'2021-03-09' AS Date), NULL, N'123')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeSheets] ON 

INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (182, N'work', N'7:00', N'16:30', N'0:15', N'9:30', 1, 12, 1230, 121, 12, N'Done', 68, CAST(N'2021-03-01' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (183, N'work', N'16:45', N'4:30', N'0:45', N'12:15', 15, 15, 15, 1, 5, N'done', 68, CAST(N'2021-03-02' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (184, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-03' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (185, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-04' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (186, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-05' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (187, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-06' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (188, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-07' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (189, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-08' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (190, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-09' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (191, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-10' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (192, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-11' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (193, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-12' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (194, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-13' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (195, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-14' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (196, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-15' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (197, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-16' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (198, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-17' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (199, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-18' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (200, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-19' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (201, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-20' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (202, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-21' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (203, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-22' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (204, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-23' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (205, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-24' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (206, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-25' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (207, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-26' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (208, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-27' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (209, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-28' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (210, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-29' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (211, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-30' AS Date), N'User1-3-2021', N'working progress')
INSERT [dbo].[TimeSheets] ([id], [type], [start_time], [end_time], [breaks], [work_time], [m3], [km_stand], [privat], [fuel], [adblue], [notes], [employeeId], [entryDate], [timeSheetKey], [timeSheetStatus]) VALUES (212, N'-1', N'-1', N'-1', N'-1', N'-1', -1, -1, -1, -1, -1, N'-1', 68, CAST(N'2021-03-31' AS Date), N'User1-3-2021', N'working progress')
SET IDENTITY_INSERT [dbo].[TimeSheets] OFF
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_TimeSheets] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_TimeSheets]
GO
/****** Object:  StoredProcedure [dbo].[spCreateAuditFile]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateAuditFile]
	@timeSheetKey nvarchar(50),
	@day nvarchar(50),
	@field nvarchar(50),
	@oldValue nvarchar(50),
	@newValue nvarchar(50),
	@dateOfChange date,
	@changedBy nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    insert into AuditFile(timeSheetKey, day, field, oldValue, newValue, dateOfChange, changedBy)
	values(@timeSheetKey, @day, @field, @oldValue, @newValue, @dateOfChange, @changedBy)
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateTimeSheetsByEmpId]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCreateTimeSheetsByEmpId]
	@id int,
	@timeSheetKey nvarchar(50),
	@entryDate date,
	@timeSheetStatus nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    insert into TimeSheets(employeeId, timeSheetKey, type, start_time, end_time, breaks, work_time, m3, km_stand, privat, fuel, adblue, notes, entryDate, timeSheetStatus)
	values(@id, @timeSheetKey, -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1, @entryDate, @timeSheetStatus)
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateUser]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateUser]
	@userName nvarchar(50),     
    @name nvarchar(50),    
    @FM# nvarchar(50),    
    @password nvarchar(50),
	@entrydate date,
	@level nvarchar(50),
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

   insert into Employee (userName, name, FM#, password, entrydate, level_)
   values (@userName, @name, @FM#, @password, @entrydate,@level)
   select @id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteUser]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDeleteUser]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    delete from Employee where id =@Id
END
GO
/****** Object:  StoredProcedure [dbo].[spEditTimeSheetById]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditTimeSheetById]
	@id int,
	@startTime nvarchar(50),
	@endTime nvarchar(50),
	@breaks nvarchar(50),
	@workTime nvarchar(50)
AS
BEGIN

	SET NOCOUNT ON;

	update TimeSheets
	set start_time = @startTime, work_time = @workTime
	where id = @id and start_time != @startTime
	update TimeSheets
	set end_time = @endTime, work_time = @workTime
	where id = @id and end_time != @endTime
	update TimeSheets
	set breaks = @breaks
	where id = @id and breaks != @breaks
END
GO
/****** Object:  StoredProcedure [dbo].[spEditTimeSheetByUser]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spEditTimeSheetByUser] 
	@id int,
	@type nvarchar(50),
	@start_time nvarchar(50),
	@end_time nvarchar(50),
	@breaks nvarchar(50),
	@work_time nvarchar(50),
	@m3 int,
	@km_stand int,
	@privat int,
	@fuel int,
	@adblue int,
	@notes nvarchar(50),
	@employeeId int,
	@entryDate date,
	@timeSheetKey nvarchar(50)


AS
BEGIN
	SET NOCOUNT ON;

	update TimeSheets
	set type = @type, start_time = @start_time, end_time = @end_time, breaks = @breaks, work_time = @work_time, m3 = @m3,
			   km_stand = @km_stand, privat = @privat, fuel = @fuel, adblue = @adblue, notes = @notes, employeeId = @employeeId,
			   entryDate = @entryDate, timeSheetKey = @timeSheetKey
	where id = @id
   
END
GO
/****** Object:  StoredProcedure [dbo].[spEditUser]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spEditUser]
	@id int,
	@personalNo int,
	@userName nvarchar(50),     
    @name nvarchar(50),
	@PW int,
    @FM# nvarchar(50),
	@level nvarchar(50),
	@email nvarchar(50)
	
AS
BEGIN
	
	SET NOCOUNT ON;

    update Employee
	set personalNo = @personalNo, userName = @userName, PW = @PW, name = @name, FM# = @FM#, level_ = @level, email = @email
	where id =  @id
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllUsers]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllUsers]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * from Employee;
END
GO
/****** Object:  StoredProcedure [dbo].[spGetTimeSheetByIdDate]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetTimeSheetByIdDate]
	@id int,
	@monthStart date,
	@monthEnd date
AS
BEGIN
	SET NOCOUNT ON;

	select * from TimeSheets where employeeId = @id and (entryDate >= @monthStart and entryDate <= @monthEnd)
END
GO
/****** Object:  StoredProcedure [dbo].[spGetTimeSheetByStatus]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetTimeSheetByStatus]
	@id int,
	@status nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	select * from TimeSheets where employeeId = @id and timeSheetStatus = @status
END
GO
/****** Object:  StoredProcedure [dbo].[spGetTimeSheetRecordById]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetTimeSheetRecordById] 
	@id int
AS
BEGIN
	SET NOCOUNT ON;

    select start_time, end_time, breaks
	from TimeSheets  
	where id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStatus]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spUpdateStatus]
	@userId int,
	@monthStart date,
	@monthEnd date,
	@status nvarchar(50)
AS
BEGIN
	update TimeSheets
	set timeSheetStatus = @status
	where employeeId = @userId and (entryDate >= @monthStart and entryDate <= @monthEnd)
END
GO
/****** Object:  StoredProcedure [dbo].[spValidateUser]    Script Date: 10.3.2021. 0:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spValidateUser]
@UserName nvarchar(50),
@Password int
AS
BEGIN
	
	SET NOCOUNT ON;
	declare @UserId int
	select id, userName, level_
	from Employee where userName = @UserName and password = @Password

END
GO
USE [master]
GO
ALTER DATABASE [Transport] SET  READ_WRITE 
GO
