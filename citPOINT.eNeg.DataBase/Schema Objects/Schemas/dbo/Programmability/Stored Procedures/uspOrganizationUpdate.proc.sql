CREATE PROC [dbo].[uspOrganizationUpdate] 
    @OrganizationID uniqueidentifier,
    @OrganizationName NvarChar(150),
    @OrganizationTypeID int,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Organization]
	SET    [OrganizationID] = @OrganizationID, [OrganizationName] = @OrganizationName, [OrganizationTypeID] = @OrganizationTypeID, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [OrganizationID] = @OrganizationID
	
	-- Begin Return Select <- do not remove
	SELECT [OrganizationID], [OrganizationName], [OrganizationTypeID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[Organization]
	WHERE  [OrganizationID] = @OrganizationID	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO