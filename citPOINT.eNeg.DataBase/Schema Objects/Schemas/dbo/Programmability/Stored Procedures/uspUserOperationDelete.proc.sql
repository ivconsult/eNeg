CREATE PROC [dbo].[uspUserOperationDelete] 
    @OperationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UserOperation]
	WHERE  [OperationID] = @OperationID

	COMMIT