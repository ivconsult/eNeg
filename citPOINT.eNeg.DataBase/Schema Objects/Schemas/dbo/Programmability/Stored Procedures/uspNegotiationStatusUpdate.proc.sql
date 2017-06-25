CREATE PROCEDURE [dbo].[uspNegotiationStatusUpdate]
 @StatusID uniqueidentifier,
    @StatusDescription nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationStatus]
	SET    [StatusID] = @StatusID, [StatusDescription] = @StatusDescription
	WHERE  [StatusID] = @StatusID
	
	-- Begin Return Select <- do not remove
	SELECT [StatusID], [StatusDescription]
	FROM   [dbo].[NegotiationStatus]
	WHERE  [StatusID] = @StatusID	
	-- End Return Select <- do not remove

	COMMIT TRAN