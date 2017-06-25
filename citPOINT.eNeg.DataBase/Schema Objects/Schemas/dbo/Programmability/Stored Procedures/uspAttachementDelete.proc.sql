CREATE PROCEDURE [dbo].[uspAttachementDelete]
 @AttachementID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Attachement]
	WHERE  [AttachementID] = @AttachementID

	COMMIT