CREATE PROCEDURE [dbo].[uspNegotiationSelectByName]
	@NegotiationID uniqueidentifier, 
	@NegotiationName nvarchar(150),
	@UserID uniqueidentifier
AS
	SELECT COUNT(NegotiationName) as NoOfNegotiationRepeat
	FROM Negotiation
	WHERE NegotiationName = @NegotiationName and 
	NegotiationID != @NegotiationID and 
	Deleted = 0 and [UserID] = @UserID
RETURN 0