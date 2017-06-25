CREATE PROC [dbo].[uspApplicationDelete] 
    @ApplicationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Application]
	WHERE  [ApplicationID] = @ApplicationID

	COMMIT