USE [CommandDB]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[InsertNewOperator]
		@operator = N'substring(@param1,1,@param2)'

SELECT	@return_value as 'Return Value'

GO

select *, substring('123',1,2) from tblOperators