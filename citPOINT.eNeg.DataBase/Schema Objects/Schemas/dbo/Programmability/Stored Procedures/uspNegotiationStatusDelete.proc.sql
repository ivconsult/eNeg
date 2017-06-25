CREATE PROCEDURE [dbo].[uspNegotiationStatusDelete]
  @StatusID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[NegotiationStatus]
	WHERE  [StatusID] = @StatusID

	COMMIT