Create PROCEDURE [dbo].[uspUserSelectRights]
 @UserID UNIQUEIDENTIFIER
AS 

	SELECT   (Replace(  dbo.[Right].RightName,' ','_') + '_' + dbo.[Right].RightDescription) RightName
	FROM         dbo.[Right] INNER JOIN
                      dbo.RoleRight ON dbo.[Right].RightID = dbo.RoleRight.RightID INNER JOIN
                      dbo.Role ON dbo.RoleRight.RoleID = dbo.Role.RoleID INNER JOIN
                      dbo.UserRole ON dbo.Role.RoleID = dbo.UserRole.RoleID INNER JOIN
                      dbo.[User] ON dbo.UserRole.UserID = dbo.[User].UserID
	WHERE  ( dbo.[User].[UserID] = @UserID OR @UserID IS NULL) 

	COMMIT
