USE [CommandDB]
GO

/****** Object: SqlProcedure [dbo].[stp_ExecuteOperator] Script Date: 12/12/2023 7:43:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[stp_ExecuteOperator]
	@operator nvarchar(50),
	@operand1 int,
	@operand2 int
AS

	select @operator = replace(@operator, '@param1', @operand1)
	select @operator = replace(@operator, '@param2', @operand2)

	--set @operator= 'substring(''ABCD'',1,2)'
	execute('select a ='+ @operator)
	

RETURN 0
