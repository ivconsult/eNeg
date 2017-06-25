CREATE PROCEDURE [dbo].[uspUserSelectRoles]
	@UserID Uniqueidentifier
AS
	SELECT dbo.[Role].RoleID,dbo.[Role].RoleName,dbo.[Role].RoleDescription
	from dbo.[UserRole],dbo.[Role],dbo.[User]
	where dbo.[Role].RoleID = dbo.[UserRole].RoleID and dbo.[User].UserID = dbo.[UserRole] .UserID
			and dbo.[User].UserID = @UserID
RETURN 0