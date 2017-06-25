create PROCEDURE [dbo].[uspUserReset]
    @UserID uniqueidentifier
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[User]
	SET     [Reset] =1
	WHERE  [UserID] = @UserID
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset] ,[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID	
	-- End Return Select <- do not remove

	COMMIT TRAN

ROLLBACK