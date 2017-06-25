CREATE PROCEDURE [dbo].[uspNegotiationInsert]
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
	
	INSERT INTO [dbo].[Negotiation] ([NegotiationID], [NegotiationName], [CreatedDate], [StatusID], [Deleted], [DeletedDate], [UserID])
	SELECT @NegotiationID, @NegotiationName, @CreatedDate, @StatusID, @Deleted, @DeletedDate, @UserID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationID], [NegotiationName], [CreatedDate], [StatusID], [Deleted], [DeletedDate], [UserID]
	FROM   [dbo].[Negotiation]
	WHERE  [NegotiationID] = @NegotiationID
	-- End Return Select <- do not remove
               
	COMMIT