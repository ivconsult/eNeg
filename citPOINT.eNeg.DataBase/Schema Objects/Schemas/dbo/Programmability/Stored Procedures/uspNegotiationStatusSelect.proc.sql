CREATE PROCEDURE [dbo].[uspNegotiationStatusSelect]
@StatusID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [StatusID], [StatusDescription] 
	FROM   [dbo].[NegotiationStatus] 
	WHERE  ([StatusID] = @StatusID OR @StatusID IS NULL) 

	COMMIT