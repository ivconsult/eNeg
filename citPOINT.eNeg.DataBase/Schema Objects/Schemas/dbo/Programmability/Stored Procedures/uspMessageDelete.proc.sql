CREATE PROCEDURE [dbo].[uspMessageDelete]
 @MessageID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	UPDATE [dbo].[Message]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [MessageID] = @MessageID
	COMMIT