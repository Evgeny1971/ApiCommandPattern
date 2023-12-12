USE [CommandDB]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[GetOperators]

SELECT	@return_value as 'Return Value'

GO
