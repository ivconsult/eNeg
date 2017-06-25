CREATE PROCEDURE [dbo].[uspNegotiationApplicationStatusUpdate]
	@NegotiationApplicationStatusID uniqueidentifier,
    @ApplicationID uniqueidentifier,
    @NegotiationID uniqueidentifier,
    @UserID uniqueidentifier,
	@Active bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationApplicationStatus]
	SET    [NegotiationApplicationStatusID] = @NegotiationApplicationStatusID, [ApplicationID] = @ApplicationID, [NegotiationID] = @NegotiationID, [UserID] = @UserID, [Active] = @Active
	WHERE  [NegotiationApplicationStatusID] = @NegotiationApplicationStatusID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationApplicationStatusID], [ApplicationID], [NegotiationID], [UserID],[Active]
	FROM   [dbo].[NegotiationApplicationStatus]
	WHERE  [NegotiationApplicationStatusID] = @NegotiationApplicationStatusID	
	-- End Return Select <- do not remove

	COMMIT TRAN