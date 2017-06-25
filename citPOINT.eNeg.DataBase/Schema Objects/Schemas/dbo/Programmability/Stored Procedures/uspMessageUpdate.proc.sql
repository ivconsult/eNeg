CREATE PROCEDURE [dbo].[uspMessageUpdate]
 @MessageID uniqueidentifier,
    @ConversationID uniqueidentifier,
    @MessageContent ntext,
    @MessageSubject nvarchar(200),
    @MessageSender nvarchar(300),
    @MessageReceiver nvarchar(300),
    @MessageDate datetime,
    @ChannelID uniqueidentifier,
	@IsSent bit,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime,
	@IsAppsMessage bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Message]
	SET    [MessageID] = @MessageID, [ConversationID] = @ConversationID, [MessageContent] = @MessageContent, [MessageSubject] = @MessageSubject, [MessageSender] = @MessageSender, [MessageReceiver] = @MessageReceiver, [MessageDate] = @MessageDate, [ChannelID] = @ChannelID,[IsSent] = @IsSent, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn,[IsAppsMessage]=@IsAppsMessage
	WHERE  [MessageID] = @MessageID
	
	-- Begin Return Select <- do not remove
	SELECT [MessageID], [ConversationID], [MessageContent], [MessageSubject], [MessageSender], [MessageReceiver], [MessageDate], [ChannelID], [IsSent], [Deleted], [DeletedBy], [DeletedOn],[IsAppsMessage]
	FROM   [dbo].[Message]
	WHERE  [MessageID] = @MessageID	
	-- End Return Select <- do not remove

	COMMIT TRAN