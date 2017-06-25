CREATE PROCEDURE [dbo].[uspNegotiationDelete]
 @NegotiationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	/*------------------------------------------------------
	Delete Negotiation
--------------------------------------------------------*/
	UPDATE [dbo].[Negotiation]
	SET 
		Deleted=1,
		DeletedDate=GETDATE()
	WHERE  [NegotiationID] = @NegotiationID;
/*------------------------------------------------------
	Delete Related Conversation
--------------------------------------------------------*/
	UPDATE [dbo].[Conversation]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [NegotiationID] = @NegotiationID;
/*------------------------------------------------------
	Delete Related Messages
--------------------------------------------------------*/
	UPDATE [Message]
	SET 
	Deleted=1,
	DeletedOn=GETDATE()
	WHERE  [ConversationID] in  
	  (Select [ConversationID] from [Conversation] where NegotiationID=@NegotiationID);
	
	COMMIT
