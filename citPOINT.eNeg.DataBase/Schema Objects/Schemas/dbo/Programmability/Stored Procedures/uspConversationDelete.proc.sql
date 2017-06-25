CREATE PROCEDURE [dbo].[uspConversationDelete]
 @ConversationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Conversation]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [ConversationID] = @ConversationID;

	/*Updating Message As Deleted Also*/
	UPDATE [Message]
	SET 
	Deleted=1,
	DeletedOn=GETDATE()
	WHERE  [ConversationID] = @ConversationID;
	

	COMMIT