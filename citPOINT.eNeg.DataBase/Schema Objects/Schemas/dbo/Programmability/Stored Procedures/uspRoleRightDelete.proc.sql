CREATE PROCEDURE [dbo].[uspRoleRightDelete]
@RoleRightID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[RoleRight]
	WHERE  [RoleRightID] = @RoleRightID

	COMMIT