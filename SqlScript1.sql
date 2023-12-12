USE [CommandDB]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[stp_ExecuteOperator]
		@operator = N'''''',
		@operand1 = '',
		@operand2 = ''

SELECT	@return_value as 'Return Value'

GO
