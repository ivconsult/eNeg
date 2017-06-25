CREATE PROCEDURE [dbo].[uspRoleRightInsert]
  @RoleRightID uniqueidentifier,
    @RightID uniqueidentifier,
    @RoleID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[RoleRight] ([RoleRightID], [RightID], [RoleID])
	SELECT @RoleRightID, @RightID, @RoleID
	
	-- Begin Return Select <- do not remove
	SELECT [RoleRightID], [RightID], [RoleID]
	FROM   [dbo].[RoleRight]
	WHERE  [RoleRightID] = @RoleRightID
	-- End Return Select <- do not remove
               
	COMMIT