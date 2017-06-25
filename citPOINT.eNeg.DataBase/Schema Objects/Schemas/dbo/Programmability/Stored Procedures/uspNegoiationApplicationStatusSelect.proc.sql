CREATE PROCEDURE [dbo].[uspNegotiationApplicationStatusSelect]
 @NegotiationApplicationStatusID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationApplicationStatusID], [ApplicationID], [NegotiationID], [UserID] ,[Active]
	FROM   [dbo].[NegotiationApplicationStatus] 
	WHERE  ([NegotiationApplicationStatusID] = @NegotiationApplicationStatusID OR @NegotiationApplicationStatusID IS NULL) 

	COMMIT