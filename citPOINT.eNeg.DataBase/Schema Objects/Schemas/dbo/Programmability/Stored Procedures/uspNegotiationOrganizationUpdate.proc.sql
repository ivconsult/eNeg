 
CREATE PROC [dbo].[uspNegotiationOrganizationUpdate] 
    @NegotiationOrganizationID uniqueidentifier,
    @NegotiationID uniqueidentifier,
    @OrganizationID uniqueidentifier,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationOrganization]
	SET    [NegotiationOrganizationID] = @NegotiationOrganizationID, [NegotiationID] = @NegotiationID, [OrganizationID] = @OrganizationID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [NegotiationOrganizationID] = @NegotiationOrganizationID
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationOrganizationID], [NegotiationID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationOrganization]
	WHERE  [NegotiationOrganizationID] = @NegotiationOrganizationID	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO