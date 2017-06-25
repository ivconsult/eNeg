CREATE PROCEDURE [dbo].[uspAttachementUpdate]
 @AttachementID uniqueidentifier,
    @AttachementName nvarchar(100),
    @AttachementContent varbinary(MAX),
    @MessageID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Attachement]
	SET    [AttachementID] = @AttachementID, [AttachementName] = @AttachementName, [AttachementContent] = @AttachementContent, [MessageID] = @MessageID
	WHERE  [AttachementID] = @AttachementID
	
	-- Begin Return Select <- do not remove
	SELECT [AttachementID], [AttachementName], [AttachementContent], [MessageID]
	FROM   [dbo].[Attachement]
	WHERE  [AttachementID] = @AttachementID	
	-- End Return Select <- do not remove

	COMMIT TRAN