USE [master]
GO
/****** Object:  Database [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF]    Script Date: 04/29/2014 17:25:29 ******/
CREATE DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] ON  PRIMARY 
( NAME = N'Messages', FILENAME = N'C:\Development Files\Messages.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Messages_log', FILENAME = N'C:\Development Files\Messages_log.ldf' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ANSI_NULLS OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ANSI_PADDING OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ARITHABORT OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET AUTO_CLOSE ON
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET AUTO_SHRINK ON
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET  DISABLE_BROKER
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET  READ_WRITE
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET RECOVERY SIMPLE
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET  MULTI_USER
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF] SET DB_CHAINING OFF
GO
USE [C:\USERS\VD2\DOCUMENTS\PERSONAL CODES\DLSUCOOP\WEBSITETRIAL\APP_DATA\MESSAGES.MDF]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[EmpNo] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](70) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](150) NULL,
	[Birthday] [date] NULL,
	[College] [nvarchar](30) NULL,
	[Dept] [nvarchar](30) NULL,
	[PhoneNumber] [nvarchar](13) NOT NULL,
	[DateConfirmed] [date] NULL,
	[MemberStatus] [nvarchar](50) NOT NULL,
	[DateHired] [nvarchar](10) NOT NULL,
	[Picture] [nvarchar](max) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Users] ([EmpNo], [Name], [Email], [Password], [Address], [Birthday], [College], [Dept], [PhoneNumber], [DateConfirmed], [MemberStatus], [DateHired], [Picture]) VALUES (N'100001', N'Valiant Dudan1', N'valiantdudz@yahoo.com', N'123456', N'fsdfsd', CAST(0x6A380B00 AS Date), N'1', N'1', N'9155642343', CAST(0x5D380B00 AS Date), N'Probationary', N'15/04/2014', NULL)
INSERT [dbo].[Users] ([EmpNo], [Name], [Email], [Password], [Address], [Birthday], [College], [Dept], [PhoneNumber], [DateConfirmed], [MemberStatus], [DateHired], [Picture]) VALUES (N'100002', N'Valiant Dudan2', N'valiantdudz@yahoo.com', N'123456', N'werwe', CAST(0x73380B00 AS Date), N'1', N'1', N'9155642343', CAST(0x5D380B00 AS Date), N'Probationary', N'29/04/2014', NULL)
INSERT [dbo].[Users] ([EmpNo], [Name], [Email], [Password], [Address], [Birthday], [College], [Dept], [PhoneNumber], [DateConfirmed], [MemberStatus], [DateHired], [Picture]) VALUES (N'100003', N'Valiant Dudan3', N'valiantdudz@yahoo.com', N'123456', N'werewrwe', CAST(0x73380B00 AS Date), N'1', N'1', N'9155642343', CAST(0x5D380B00 AS Date), N'Probationary', N'17/04/2014', NULL)
INSERT [dbo].[Users] ([EmpNo], [Name], [Email], [Password], [Address], [Birthday], [College], [Dept], [PhoneNumber], [DateConfirmed], [MemberStatus], [DateHired], [Picture]) VALUES (N'100004', N'apple', N'apple@dlsud.edu.ph', N'123456', N'laguna', CAST(0x09370B00 AS Date), N'3', N'3', N'9155642343', CAST(0x5D380B00 AS Date), N'Part Time', N'10/04/2014', NULL)
/****** Object:  Table [dbo].[UnconfirmedUsers]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnconfirmedUsers](
	[EmpNo] [nvarchar](10) NOT NULL,
	[DateRegistered] [date] NOT NULL,
	[ConfirmationCode] [nvarchar](35) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[UnconfirmedUsers] ([EmpNo], [DateRegistered], [ConfirmationCode]) VALUES (N'100001', CAST(0x5B380B00 AS Date), N'123123')
INSERT [dbo].[UnconfirmedUsers] ([EmpNo], [DateRegistered], [ConfirmationCode]) VALUES (N'100002', CAST(0x5C380B00 AS Date), N'123123')
INSERT [dbo].[UnconfirmedUsers] ([EmpNo], [DateRegistered], [ConfirmationCode]) VALUES (N'100003', CAST(0x5C380B00 AS Date), N'123123')
INSERT [dbo].[UnconfirmedUsers] ([EmpNo], [DateRegistered], [ConfirmationCode]) VALUES (N'100004', CAST(0x72380B00 AS Date), NULL)
/****** Object:  Table [dbo].[UnconfirmedLoan]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnconfirmedLoan](
	[TransactionID] [int] NOT NULL,
	[ConfirmedByMaker] [bit] NULL,
	[ConfirmedByCoMaker] [bit] NULL,
	[DateFiled] [date] NOT NULL,
	[ConfirmCodeMakerSMS] [nvarchar](6) NULL,
	[ConfirmCodeCoMakerSMS] [nvarchar](6) NULL,
	[ConfirmCodeMakerEmail] [nvarchar](35) NULL,
	[ConfirmCodeCoMakerEmail] [nvarchar](35) NULL,
	[ConfirmedByCoMaker2] [bit] NULL,
	[ConfirmCodeCoMaker2SMS] [nvarchar](6) NULL,
	[ConfirmCodeCoMaker2Email] [nvarchar](35) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[UnconfirmedLoan] ([TransactionID], [ConfirmedByMaker], [ConfirmedByCoMaker], [DateFiled], [ConfirmCodeMakerSMS], [ConfirmCodeCoMakerSMS], [ConfirmCodeMakerEmail], [ConfirmCodeCoMakerEmail], [ConfirmedByCoMaker2], [ConfirmCodeCoMaker2SMS], [ConfirmCodeCoMaker2Email]) VALUES (1, 0, 0, CAST(0x77380B00 AS Date), N'WTKA5F', N'XMXUPR', N'XhWN4}_rwWy!LK-mRrpN;az:m;VZ!;E&d);', N'}Wsko1-C5Zfa7#..wXq)o+lN9$V$m}_q[D-', 0, N'52DYLT', N'*Xc72#6fN_h{98B6]F.@WB7Swb%^IQ/6(Np')
/****** Object:  Table [dbo].[Payments]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[RecordID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionID] [int] NOT NULL,
	[PayAmount] [float] NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Payments] ON
INSERT [dbo].[Payments] ([RecordID], [TransactionID], [PayAmount], [Note]) VALUES (1, 1, 1200, N'nonoenoenoee')
INSERT [dbo].[Payments] ([RecordID], [TransactionID], [PayAmount], [Note]) VALUES (2, 1, 5000, N'')
INSERT [dbo].[Payments] ([RecordID], [TransactionID], [PayAmount], [Note]) VALUES (3, 1, 5000, N'werteter')
SET IDENTITY_INSERT [dbo].[Payments] OFF
/****** Object:  Table [dbo].[MSGS]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSGS](
	[ID] [nvarchar](50) NULL,
	[Source] [nvarchar](50) NULL,
	[Msg] [nvarchar](max) NULL,
	[UDH] [nvarchar](50) NULL,
	[DateReceived] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanApplication]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanApplication](
	[TransactionID] [int] NOT NULL,
	[EmpNo] [nvarchar](10) NOT NULL,
	[CoMakerEmpNo] [nvarchar](10) NULL,
	[TypeOfLoan] [nvarchar](50) NOT NULL,
	[Reason] [text] NULL,
	[Amount] [money] NOT NULL,
	[NoOfMonths] [int] NOT NULL,
	[Confirmed] [bit] NULL,
	[DateApproved] [date] NULL,
	[DateDue] [date] NULL,
	[CoMaker2EmpNo] [nvarchar](10) NULL,
	[Done] [bit] NULL,
	[Declined] [bit] NULL,
	[Balance] [money] NULL,
 CONSTRAINT [PK_LoanApplication] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[LoanApplication] ([TransactionID], [EmpNo], [CoMakerEmpNo], [TypeOfLoan], [Reason], [Amount], [NoOfMonths], [Confirmed], [DateApproved], [DateDue], [CoMaker2EmpNo], [Done], [Declined], [Balance]) VALUES (1, N'100003', N'100001', N'Regular', N'none', 5000.0000, 3, 0, NULL, NULL, N'100002', 0, 0, NULL)
/****** Object:  Table [dbo].[Department]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[CollegeID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (1, 1, N'Accountancy')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (1, 2, N'Allied Business')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (1, 3, N'Business Management')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (1, 4, N'Marketing')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (1, 6, N'College of Business Ad,inistration Graduate Studies')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (2, 1, N'Professional Education')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (2, 2, N'Religious Education')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (2, 3, N'Physical Education')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (2, 4, N'COE Graduate Studies')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (3, 1, N'Architecture')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (3, 2, N'Engineering')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (3, 3, N'Technology')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (4, 1, N'Hotel and Restaurant Management')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (4, 2, N'Tourism Management')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (5, 1, N'Criminal Justice Education')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 1, N'Behavioral Sciences')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 2, N'Social Sciences')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 3, N'Languages and Literature')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 4, N'Communication Arts')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 5, N'Kagawaran ng Filipino at Panitikan')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (6, 6, N'CLA Graduate Studies')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (7, 1, N'Biological Sciences')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (7, 2, N'Computer Studies')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (7, 3, N'Mathematics')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (7, 4, N'Physical Sciences')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (7, 5, N'COS Graduate Studies')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 1, N'Accreditation Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 2, N'Accounting Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 3, N'Alumni Relations and Placement Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 4, N'Aklatang Emilio Aguinaldo')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 5, N'Buildings and Facilities Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 6, N'Campus Ministry Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 7, N'Cavite Studies Center')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 8, N'Cultural Arts Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 9, N'Compliance Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 10, N'Controller')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 11, N'DLSU-D Development Cooperative')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 12, N'Environment and Resource Managemenr Center and Campus Development Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 13, N'Faculty Association')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 14, N'Human Resource Manageent Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 15, N'Information and Communications Technology Center')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 16, N'Institutional Testing and Evaluation Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 17, N'Jubilee Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 18, N'Language Learning Center')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 19, N'Lasallian Community Development Center')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 20, N'Legal Counsel')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 21, N'Marketing Communication Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 22, N'Materials Reproduction Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 23, N'Museo De La Salle')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 24, N'National Service Training Program')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 25, N'Office of Student Services')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 26, N'Planning Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 27, N'Purchasing Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 28, N'POLCA')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 29, N'Registrar''s Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 30, N'Student Scholarship Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 31, N'School Clinic')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 32, N'Sports Development Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 33, N'Student Admissions Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 34, N'Student Development and Activities Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 35, N'Student Publications Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 36, N'SWAFO')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 37, N'Student Wellness Center')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 38, N'Treasury Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 39, N'University Advancement Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 40, N'University Lasallian Family Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 41, N'University Linkages Office')
INSERT [dbo].[Department] ([CollegeID], [DepartmentID], [DepartmentName]) VALUES (8, 42, N'University Research Office')
/****** Object:  Table [dbo].[College]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[College](
	[CollegeID] [int] IDENTITY(1,1) NOT NULL,
	[CollegeName] [nvarchar](25) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[College] ON
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (1, N'CBA')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (2, N'COE')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (3, N'CEAT')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (4, N'CIHM')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (5, N'CCJE')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (6, N'CLA')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (7, N'COS')
INSERT [dbo].[College] ([CollegeID], [CollegeName]) VALUES (8, N'Offices')
SET IDENTITY_INSERT [dbo].[College] OFF
/****** Object:  Table [dbo].[AdminAccount]    Script Date: 04/29/2014 17:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminAccount](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[AdminAccount] ([Username], [Password]) VALUES (N'Admin', N'Admin')
