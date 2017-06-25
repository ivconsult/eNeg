CREATE PROC [dbo].[uspUserOrganizationSelect] 
    @UserOrganizationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UserOrganizationID], [UserID], [OrganizationID], [MemberStatus], [Deleted], [DeletedBy], [DeletedOn] 
	FROM   [dbo].[UserOrganization] 
	WHERE  ([UserOrganizationID] = @UserOrganizationID OR @UserOrganizationID IS NULL) 

	COMMIT
GO