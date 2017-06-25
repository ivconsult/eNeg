CREATE PROCEDURE [dbo].[uspRoleRightSelect]
 @RoleRightID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [RoleRightID], [RightID], [RoleID] 
	FROM   [dbo].[RoleRight] 
	WHERE  ([RoleRightID] = @RoleRightID OR @RoleRightID IS NULL) 

	COMMIT