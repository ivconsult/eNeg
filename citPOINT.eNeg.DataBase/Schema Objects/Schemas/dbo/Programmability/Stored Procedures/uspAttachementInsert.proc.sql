CREATE PROCEDURE [dbo].[uspAttachementInsert]
  @AttachementID uniqueidentifier,
    @AttachementName nvarchar(100),
    @AttachementContent varbinary(MAX),
    @MessageID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Attachement] ([AttachementID], [AttachementName], [AttachementContent], [MessageID])
	SELECT @AttachementID, @AttachementName, @AttachementContent, @MessageID
	
	-- Begin Return Select <- do not remove
	SELECT [AttachementID], [AttachementName], [AttachementContent], [MessageID]
	FROM   [dbo].[Attachement]
	WHERE  [AttachementID] = @AttachementID
	-- End Return Select <- do not remove
               
	COMMIT