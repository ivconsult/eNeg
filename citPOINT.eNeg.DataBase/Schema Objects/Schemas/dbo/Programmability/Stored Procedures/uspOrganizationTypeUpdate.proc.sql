CREATE PROC [dbo].[uspOrganizationTypeUpdate] 
    @OrganizationTypeID int,
    @OrganizationTypeName Nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[OrganizationType]
	SET    [OrganizationTypeID] = @OrganizationTypeID, [OrganizationTypeName] = @OrganizationTypeName
	WHERE  [OrganizationTypeID] = @OrganizationTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [OrganizationTypeID], [OrganizationTypeName]
	FROM   [dbo].[OrganizationType]
	WHERE  [OrganizationTypeID] = @OrganizationTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO