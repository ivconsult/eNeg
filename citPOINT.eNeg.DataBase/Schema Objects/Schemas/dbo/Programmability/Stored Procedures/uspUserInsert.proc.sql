CREATE PROC [dbo].[uspUserInsert] 
    @UserID uniqueidentifier,
    @EmailAddress nvarchar(300),
    @PasswordHash nvarchar(150),
    @PasswordSalt nvarchar(150),
    @Locked bit,
    @LockedDate datetime,
    @LastLoginDate datetime,
    @CreateDate datetime,
    @AccountTypeID uniqueidentifier,
    @IPAddress nvarchar(50),
    @SecurityQuestionID uniqueidentifier,
    @AnswerHash nvarchar(150),
    @AnswerSalt nvarchar(150),
    @Online bit,
    @Disabled bit,
    @FirstName nvarchar(100),
    @LastName nvarchar(100),
    @CompanyName nvarchar(100),
    @LCID int,
    @Address nvarchar(300),
    @City nvarchar(100),
    @CountryID uniqueidentifier,
    @Phone nvarchar(100),
    @Mobile nvarchar(100),
    @Gender bit,
    @Reset bit,
	@CultureID int,
	@HasPublicProfile bit,
	@ProfileDescription NVarchar(300),
	@PostalCode NVarchar(150)
	
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[User] ([UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset],[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode])
	SELECT @UserID, @EmailAddress, @PasswordHash, @PasswordSalt, @Locked, @LockedDate, @LastLoginDate, @CreateDate, @AccountTypeID, @IPAddress, @SecurityQuestionID, @AnswerHash, @AnswerSalt, @Online, @Disabled, @FirstName, @LastName, @CompanyName, @LCID, @Address, @City, @CountryID, @Phone, @Mobile, @Gender, @Reset,@CultureID,@HasPublicProfile,@ProfileDescription,@PostalCode
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset],[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID
	-- End Return Select <- do not remove
               
	COMMIT
GO