CREATE PROCEDURE [dbo].[uspUserLogoutUpdate]
	@UserID uniqueidentifier
AS
	Begin Tran

		update [dbo].[User]
		set [Online] = 0
		where [UserID]=@UserID

		-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID	
	-- End Return Select <- do not remove

	commit Tran
go