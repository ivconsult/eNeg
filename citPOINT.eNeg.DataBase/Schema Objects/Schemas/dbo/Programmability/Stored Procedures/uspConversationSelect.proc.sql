CREATE PROCEDURE [dbo].[uspConversationSelect]
  @ConversationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ConversationID], [ConversationName], [NegotiationID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[Conversation] 
	WHERE  ([ConversationID] = @ConversationID OR @ConversationID IS NULL) 

	COMMIT