CREATE PROC [dbo].[uspOrganizationTypeInsert] 
    @OrganizationTypeID int,
    @OrganizationTypeName Nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[OrganizationType] ([OrganizationTypeID], [OrganizationTypeName])
	SELECT @OrganizationTypeID, @OrganizationTypeName
	
	-- Begin Return Select <- do not remove
	SELECT [OrganizationTypeID], [OrganizationTypeName]
	FROM   [dbo].[OrganizationType]
	WHERE  [OrganizationTypeID] = @OrganizationTypeID
	-- End Return Select <- do not remove
               
	COMMIT
GO