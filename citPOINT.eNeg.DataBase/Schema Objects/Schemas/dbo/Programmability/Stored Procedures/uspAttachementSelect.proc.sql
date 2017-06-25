CREATE PROCEDURE [dbo].[uspAttachementSelect]
 @AttachementID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [AttachementID], [AttachementName], [AttachementContent], [MessageID] 
	FROM   [dbo].[Attachement] 
	WHERE  ([AttachementID] = @AttachementID OR @AttachementID IS NULL) 

	COMMIT