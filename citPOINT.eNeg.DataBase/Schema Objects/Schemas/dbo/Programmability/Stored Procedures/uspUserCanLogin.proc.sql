CREATE PROCEDURE [dbo].[uspUserCanLogin]
	@EmailAddress nvarchar(300)
AS
	BEGIN TRAN

		SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset] ,[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
		FROM   [dbo].[User] 
		WHERE  [EmailAddress] = @EmailAddress 

	COMMIT