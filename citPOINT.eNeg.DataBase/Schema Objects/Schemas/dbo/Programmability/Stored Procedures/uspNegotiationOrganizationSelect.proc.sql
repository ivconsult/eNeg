CREATE PROC [dbo].[uspNegotiationOrganizationSelect] 
    @NegotiationOrganizationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [NegotiationOrganizationID], [NegotiationID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[NegotiationOrganization] 
	WHERE  ([NegotiationOrganizationID] = @NegotiationOrganizationID OR @NegotiationOrganizationID IS NULL) 

	COMMIT
GO