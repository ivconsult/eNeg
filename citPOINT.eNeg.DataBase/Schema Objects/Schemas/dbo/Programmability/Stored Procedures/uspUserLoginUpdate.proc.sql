CREATE PROCEDURE [dbo].[uspUserLoginUpdate]
	@UserID uniqueidentifier,
	@IPAddress nvarchar(50)
AS
	Begin Tran

		update [dbo].[User]
		set [Online] = 1,[LastLoginDate] = GETDATE(),IPAddress=@IPAddress
		where [UserID]=@UserID

		-- Begin Return Select <- do not remove
	SELECT *
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID	
	-- End Return Select <- do not remove

	commit Tran
go