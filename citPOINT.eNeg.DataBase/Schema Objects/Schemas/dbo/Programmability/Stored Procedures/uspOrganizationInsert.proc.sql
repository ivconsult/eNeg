CREATE PROC [dbo].[uspOrganizationInsert] 
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
	
	INSERT INTO [dbo].[Organization] ([OrganizationID], [OrganizationName], [OrganizationTypeID], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @OrganizationID, @OrganizationName, @OrganizationTypeID, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [OrganizationID], [OrganizationName], [OrganizationTypeID], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[Organization]
	WHERE  [OrganizationID] = @OrganizationID
	-- End Return Select <- do not remove
               
	COMMIT
GO