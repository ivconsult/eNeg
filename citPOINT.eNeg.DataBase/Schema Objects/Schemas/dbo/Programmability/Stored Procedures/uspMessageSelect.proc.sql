CREATE PROCEDURE [dbo].[uspMessageSelect]
  @MessageID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [MessageID], [ConversationID], [MessageContent], [MessageSubject], [MessageSender], [MessageReceiver], [MessageDate], [ChannelID], [IsSent], [Deleted], [DeletedBy], [DeletedOn] ,[IsAppsMessage]
	FROM   [dbo].[Message] 
	WHERE  ([MessageID] = @MessageID OR @MessageID IS NULL) 

	COMMIT