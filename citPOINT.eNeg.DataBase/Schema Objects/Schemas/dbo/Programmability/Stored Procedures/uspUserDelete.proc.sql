 
CREATE PROC [dbo].[uspUserDelete] 
    @UserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
		
	UPDATE [DBO].[USER]
	SET		DISABLED=1
	WHERE [USERID] = @USERID;
	
		
	Update [UserOrganization]
	SET Deleted=1,
	    DeletedOn=GETDATE()
	WHERE UserID=@UserID;


	UPDATE  [Message]
		SET Deleted   = 1,
			DeletedOn = GETDATE()
	WHERE   Deleted = 0
			AND DeletedBy = @UserID;

	/*They Have not Delete Flag*/
	DELETE [UserRole]
	WHERE  [UserRole].UserID = @UserID;

	/*They Have not Delete Flag*/
	DELETE [UserOperation]
	WHERE  UserID = @UserID
		   OR RequestUserID = @UserID;

	/*They Have not Delete Flag*/
	DELETE [UserApplicationStatus]
	WHERE  [UserApplicationStatus].UserID = @UserID;

	/*They Have not Delete Flag*/
	DELETE [Profile]
	WHERE  UserID = @UserID;

	UPDATE  [NegotiationOrganization]
		SET Deleted   = 1,
			DeletedOn = GETDATE()
	WHERE   Deleted = 0
			AND DeletedBy = @UserID;

	/*They Have not Delete Flag*/
	DELETE [NegotiationApplicationStatus]
	WHERE  [NegotiationApplicationStatus].UserID = @UserID;

	UPDATE  [Conversation]
		SET Deleted   = 1,
			DeletedOn = GETDATE()
	WHERE   Deleted = 0
			AND DeletedBy = @UserID;

	UPDATE  [Negotiation]
		SET Deleted   = 1,
		    [Negotiation].DeletedDate = GETDATE()
	WHERE   Deleted = 0
			AND UserID = @UserID;
	
	COMMIT
GO