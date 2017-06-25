CREATE PROCEDURE [dbo].[uspUserRoleInsert]
 @UserRoleID uniqueidentifier,
    @UserID uniqueidentifier,
    @RoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UserRole] ([UserRoleID], [UserID], [RoleID])
	SELECT @UserRoleID, @UserID, @RoleID
	
	-- Begin Return Select <- do not remove
	SELECT [UserRoleID], [UserID], [RoleID]
	FROM   [dbo].[UserRole]
	WHERE  [UserRoleID] = @UserRoleID
	-- End Return Select <- do not remove
               
	COMMIT