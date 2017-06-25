CREATE PROCEDURE [dbo].[uspNegotiationApplicationStatusInsert]
 @NegotiationApplicationStatusID uniqueidentifier,
    @ApplicationID uniqueidentifier,
    @NegotiationID uniqueidentifier,
    @UserID uniqueidentifier,
	@Active bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[NegotiationApplicationStatus] ([NegotiationApplicationStatusID], [ApplicationID], [NegotiationID], [UserID],[Active])
	SELECT @NegotiationApplicationStatusID, @ApplicationID, @NegotiationID, @UserID,@Active
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationApplicationStatusID], [ApplicationID], [NegotiationID], [UserID],[Active]
	FROM   [dbo].[NegotiationApplicationStatus]
	WHERE  [NegotiationApplicationStatusID] = @NegotiationApplicationStatusID
	-- End Return Select <- do not remove
               
	COMMIT