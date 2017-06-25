CREATE PROCEDURE [dbo].[uspNegotiationUpdate]
 @NegotiationID uniqueidentifier,
    @NegotiationName nvarchar(150),
    @CreatedDate datetime,
    @StatusID uniqueidentifier,
    @Deleted bit,
    @DeletedDate datetime,
    @UserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Negotiation]
	SET    [NegotiationID] = @NegotiationID, [NegotiationName] = @NegotiationName, [CreatedDate] = @CreatedDate, [StatusID] = @StatusID, [Deleted] = @Deleted, [DeletedDate] = @DeletedDate, [UserID] = @UserID
	WHERE  [NegotiationID] = @NegotiationID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationID], [NegotiationName], [CreatedDate], [StatusID], [Deleted], [DeletedDate], [UserID]
	FROM   [dbo].[Negotiation]
	WHERE  [NegotiationID] = @NegotiationID	
	-- End Return Select <- do not remove

	COMMIT TRAN