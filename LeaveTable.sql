USE [AttendanceSystem]
GO

/****** Object:  Table [dbo].[LeaveRecord]    Script Date: 22/1/2019 5:06:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LeaveRecord](
	[StaffId] [int] NOT NULL,
	[From] [date] NULL,
	[To] [date] NULL,
	[HalfPeriod] [nvarchar](50) NULL,
	[Days] [decimal](3, 2) NOT NULL,
	[TypeOfLeave] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


