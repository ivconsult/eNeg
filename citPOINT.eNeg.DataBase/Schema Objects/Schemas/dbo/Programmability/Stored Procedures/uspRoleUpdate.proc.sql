CREATE PROCEDURE [dbo].[uspRoleUpdate]
 @RoleID uniqueidentifier,
    @RoleName nvarchar(100),
    @RoleDescription nvarchar(300)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Role]
	SET    [RoleID] = @RoleID, [RoleName] = @RoleName, [RoleDescription] = @RoleDescription
	WHERE  [RoleID] = @RoleID
	
	-- Begin Return Select <- do not remove
	SELECT [RoleID], [RoleName], [RoleDescription]
	FROM   [dbo].[Role]
	WHERE  [RoleID] = @RoleID	
	-- End Return Select <- do not remove

	COMMIT TRAN