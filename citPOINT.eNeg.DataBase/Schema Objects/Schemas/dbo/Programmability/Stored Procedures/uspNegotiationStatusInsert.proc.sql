CREATE PROCEDURE [dbo].[uspNegotiationStatusInsert]
  @StatusID uniqueidentifier,
    @StatusDescription nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[NegotiationStatus] ([StatusID], [StatusDescription])
	SELECT @StatusID, @StatusDescription
	
	-- Begin Return Select <- do not remove
	SELECT [StatusID], [StatusDescription]
	FROM   [dbo].[NegotiationStatus]
	WHERE  [StatusID] = @StatusID
	-- End Return Select <- do not remove
               
	COMMIT