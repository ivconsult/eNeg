CREATE PROC [dbo].[uspOrganizationSelect] 
    @OrganizationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [OrganizationID], [OrganizationName], [OrganizationTypeID], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[Organization] 
	WHERE  ([OrganizationID] = @OrganizationID OR @OrganizationID IS NULL) 

	COMMIT
GO