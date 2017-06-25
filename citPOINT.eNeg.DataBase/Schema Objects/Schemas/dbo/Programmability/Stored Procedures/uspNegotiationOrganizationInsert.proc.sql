 
CREATE PROC [dbo].[uspNegotiationOrganizationInsert] 
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
	
	INSERT INTO [dbo].[NegotiationOrganization] ([NegotiationOrganizationID], [NegotiationID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @NegotiationOrganizationID, @NegotiationID, @OrganizationID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [NegotiationOrganizationID], [NegotiationID], [OrganizationID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[NegotiationOrganization]
	WHERE  [NegotiationOrganizationID] = @NegotiationOrganizationID
	-- End Return Select <- do not remove
               
	COMMIT
GO