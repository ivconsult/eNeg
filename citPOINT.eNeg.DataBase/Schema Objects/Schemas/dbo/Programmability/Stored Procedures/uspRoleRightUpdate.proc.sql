CREATE PROCEDURE [dbo].[uspRoleRightUpdate]
 @RoleRightID uniqueidentifier,
    @RightID uniqueidentifier,
    @RoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[RoleRight]
	SET    [RoleRightID] = @RoleRightID, [RightID] = @RightID, [RoleID] = @RoleID
	WHERE  [RoleRightID] = @RoleRightID
	
	-- Begin Return Select <- do not remove
	SELECT [RoleRightID], [RightID], [RoleID]
	FROM   [dbo].[RoleRight]
	WHERE  [RoleRightID] = @RoleRightID	
	-- End Return Select <- do not remove

	COMMIT TRAN