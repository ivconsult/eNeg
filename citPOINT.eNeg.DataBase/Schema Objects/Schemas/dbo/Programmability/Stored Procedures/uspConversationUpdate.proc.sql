CREATE PROCEDURE [dbo].[uspConversationUpdate]
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

	UPDATE [dbo].[Conversation]
	SET    [ConversationID] = @ConversationID, [ConversationName] = @ConversationName, [NegotiationID] = @NegotiationID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [ConversationID] = @ConversationID
	
	-- Begin Return Select <- do not remove
	SELECT [ConversationID], [ConversationName], [NegotiationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[Conversation]
	WHERE  [ConversationID] = @ConversationID	
	-- End Return Select <- do not remove

	COMMIT TRAN