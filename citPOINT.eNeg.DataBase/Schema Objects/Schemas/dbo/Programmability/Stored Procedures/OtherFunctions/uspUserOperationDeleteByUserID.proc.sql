Create PROCEDURE [dbo].[uspUserOperationDeleteByUserID]
 @UserID  uniqueidentifier
AS 
	
	BEGIN TRAN
	--Delete User Operations
	Delete [dbo].UserOperation
	Where  [dbo].UserOperation.[UserID] = @UserID;
	--Delete User Operations
	
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset],[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID	
	-- End Return Select <- do not remove
	
	COMMIT TRAN
	