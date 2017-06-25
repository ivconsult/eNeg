CREATE PROCEDURE [dbo].[uspUserRoleDelete]
  @UserRoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UserRole]
	WHERE  [UserRoleID] = @UserRoleID

	COMMIT