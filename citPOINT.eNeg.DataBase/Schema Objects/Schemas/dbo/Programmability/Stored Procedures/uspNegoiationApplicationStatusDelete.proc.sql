CREATE PROCEDURE [dbo].[uspNegotiationApplicationStatusDelete]
 @NegotiationApplicationStatusID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[NegotiationApplicationStatus]
	WHERE  [NegotiationApplicationStatusID] = @NegotiationApplicationStatusID

	COMMIT