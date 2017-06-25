CREATE PROCEDURE [dbo].[uspUserRoleSelect]
	 @UserRoleID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UserRoleID], [UserID], [RoleID] 
	FROM   [dbo].[UserRole] 
	WHERE  ([UserRoleID] = @UserRoleID OR @UserRoleID IS NULL) 

	COMMIT