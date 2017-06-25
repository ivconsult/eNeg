CREATE PROCEDURE [dbo].[uspConversationInsert]
 @ConversationID uniqueidentifier,
    @ConversationName nvarchar(100),
    @NegotiationID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Conversation] ([ConversationID], [ConversationName], [NegotiationID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @ConversationID, @ConversationName, @NegotiationID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationID], [ConversationName], [NegotiationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[Conversation]
	WHERE  [ConversationID] = @ConversationID
	-- End Return Select <- do not remove
               
	COMMIT