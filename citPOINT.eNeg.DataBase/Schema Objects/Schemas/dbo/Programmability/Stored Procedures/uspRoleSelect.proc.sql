CREATE PROCEDURE [dbo].[uspRoleSelect]
 @RoleID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [RoleID], [RoleName], [RoleDescription] 
	FROM   [dbo].[Role] 
	WHERE  ([RoleID] = @RoleID OR @RoleID IS NULL) 

	COMMIT