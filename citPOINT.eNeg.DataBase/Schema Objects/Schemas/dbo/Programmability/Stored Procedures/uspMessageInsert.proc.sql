CREATE PROCEDURE [dbo].[uspMessageInsert]
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
	
	INSERT INTO [dbo].[Message] ([MessageID], [ConversationID], [MessageContent], [MessageSubject], [MessageSender], [MessageReceiver], [MessageDate], [ChannelID], [IsSent],[Deleted], [DeletedBy], [DeletedOn],[IsAppsMessage])
	SELECT @MessageID, @ConversationID, @MessageContent, @MessageSubject, @MessageSender, @MessageReceiver, @MessageDate, @ChannelID,@IsSent, @Deleted, @DeletedBy, @DeletedOn,@IsAppsMessage
	
	-- Begin Return Select <- do not remove
	SELECT [MessageID], [ConversationID], [MessageContent], [MessageSubject], [MessageSender], [MessageReceiver], [MessageDate], [ChannelID], [IsSent], [Deleted], [DeletedBy], [DeletedOn],[IsAppsMessage]
	FROM   [dbo].[Message]
	WHERE  [MessageID] = @MessageID

	-- End Return Select <- do not remove
               
	COMMIT