CREATE PROC [dbo].[uspUserSelect] 
    @UserID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset],[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
	FROM   [dbo].[User] 
	WHERE  ([UserID] = @UserID OR @UserID IS NULL) 

	COMMIT
GO