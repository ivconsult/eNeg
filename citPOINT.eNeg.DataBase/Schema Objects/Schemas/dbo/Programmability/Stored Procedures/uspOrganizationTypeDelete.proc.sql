CREATE PROC [dbo].[uspOrganizationTypeDelete] 
    @OrganizationTypeID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[OrganizationType]
	WHERE  [OrganizationTypeID] = @OrganizationTypeID

	COMMIT
GO