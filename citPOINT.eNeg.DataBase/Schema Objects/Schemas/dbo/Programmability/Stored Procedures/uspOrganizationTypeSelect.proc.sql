CREATE PROC [dbo].[uspOrganizationTypeSelect] 
    @OrganizationTypeID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [OrganizationTypeID], [OrganizationTypeName] 
	FROM   [dbo].[OrganizationType] 
	WHERE  ([OrganizationTypeID] = @OrganizationTypeID OR @OrganizationTypeID IS NULL) 

	COMMIT
GO