CREATE PROC [dbo].[uspNegotiationOrganizationDelete] 
    @NegotiationOrganizationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[NegotiationOrganization]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [NegotiationOrganizationID] = @NegotiationOrganizationID;


	COMMIT
GO