CREATE PROCEDURE [dbo].[uspRoleDelete]
 @RoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Role]
	WHERE  [RoleID] = @RoleID

	COMMIT