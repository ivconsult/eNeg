CREATE PROCEDURE [dbo].[uspUserRoleUpdate]
 @UserRoleID uniqueidentifier,
    @UserID uniqueidentifier,
    @RoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserRole]
	SET    [UserRoleID] = @UserRoleID, [UserID] = @UserID, [RoleID] = @RoleID
	WHERE  [UserRoleID] = @UserRoleID
	
	-- Begin Return Select <- do not remove
	SELECT [UserRoleID], [UserID], [RoleID]
	FROM   [dbo].[UserRole]
	WHERE  [UserRoleID] = @UserRoleID	
	-- End Return Select <- do not remove

	COMMIT TRAN