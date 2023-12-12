USE [CommandDB]
GO

/****** Object: Table [dbo].[tblOperators] Script Date: 12/12/2023 7:56:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblOperators] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Operator] NVARCHAR (50) NULL
);


USE [CommandDB]
GO

/****** Object: SqlProcedure [dbo].[GetOperators] Script Date: 12/12/2023 7:57:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOperators]
	
AS
	SELECT * from tblOperators
RETURN 0
go
USE [CommandDB]
GO

/****** Object: SqlProcedure [dbo].[InsertNewOperator] Script Date: 12/12/2023 7:57:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertNewOperator]
	@operator nvarchar(50) = ''
AS
	insert into tblOperators values(@operator)
	
RETURN 0
go
USE [CommandDB]
GO

/****** Object: SqlProcedure [dbo].[stp_ExecuteOperator] Script Date: 12/12/2023 7:57:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[stp_ExecuteOperator]
	@operator nvarchar(50),
	@operand1 int,
	@operand2 int
AS

	select @operator = replace(@operator, '@param1', @operand1)
	select @operator = replace(@operator, '@param2', @operand2)

	--set @operator= 'substring(''ABCD'',1,2)'
	execute('select a ='+ @operator)
	

RETURN 0

