CREATE PROCEDURE [dbo].[uspNegotiationSelect]
 @NegotiationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationID], [NegotiationName], [CreatedDate], [StatusID], [Deleted], [DeletedDate], [UserID] 
	FROM   [dbo].[Negotiation] 
	WHERE  ([NegotiationID] = @NegotiationID OR @NegotiationID IS NULL) 

	COMMIT