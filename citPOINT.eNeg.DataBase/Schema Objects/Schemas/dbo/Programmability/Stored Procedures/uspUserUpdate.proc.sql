CREATE PROC [dbo].[uspUserUpdate] 
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
    @FirstName          nvarchar(100),
    @LastName           nvarchar(100),
    @CompanyName        nvarchar(100),
    @LCID               int,
    @Address            nvarchar(300),
    @City               nvarchar(100),
    @CountryID          uniqueidentifier,
    @Phone              nvarchar(100),
    @Mobile             nvarchar(100),
    @Gender             bit,
    @Reset              bit,
	@CultureID          int,
	@HasPublicProfile   bit,
	@ProfileDescription Nvarchar(300),
	@PostalCode         Nvarchar(150)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[User]
	SET    [UserID]				= @UserID,
		   [EmailAddress]		= @EmailAddress, 
		   [PasswordHash]		= (case when len(isnull(@PasswordHash,' '))<=6/*Flag mean no changes in password*/ then [PasswordHash] else @PasswordHash end ) , 
		   [PasswordSalt]		= (case when len(isnull(@PasswordHash,' '))<=6/*Flag mean no changes in password*/ then [PasswordSalt] else @PasswordSalt end ) ,  
		   [Locked]				= @Locked, 
		   [LockedDate]			= @LockedDate, 
		   [LastLoginDate]		= @LastLoginDate, 
		   [CreateDate]			= @CreateDate, 
		   [AccountTypeID]		= @AccountTypeID, 
		   [IPAddress]			= @IPAddress, 
		   [SecurityQuestionID] = @SecurityQuestionID, 
		   [AnswerHash]			= (case when len(isnull(@AnswerHash,' '))<=6/*Flag mean no changes in password*/ then [AnswerHash] else @AnswerHash end ) , 
		   [AnswerSalt]			= (case when len(isnull(@AnswerHash,' '))<=6/*Flag mean no changes in password*/ then [AnswerSalt] else @AnswerSalt end ) ,  
		   [Online]				= @Online, 
		   [Disabled]			= @Disabled, 
		   [FirstName]			= @FirstName, 
		   [LastName]			= @LastName, 
		   [CompanyName]		= @CompanyName, 
		   [LCID]				= @LCID, 
		   [Address]			= @Address, 
		   [City]				= @City, 
		   [CountryID]			= @CountryID, 
		   [Phone]				= @Phone, 
		   [Mobile]				= @Mobile, 
		   [Gender]				= @Gender, 
		   [Reset] 				= @Reset,
		   [CultureID]          = @CultureID,
		   [HasPublicProfile]   = @HasPublicProfile,
		   [ProfileDescription] = @ProfileDescription,
		   [PostalCode]         = @PostalCode
	WHERE  [UserID]				= @UserID
	
	-- Begin Return Select <- do not remove
	SELECT [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset],[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
	FROM   [dbo].[User]
	WHERE  [UserID] = @UserID	
	-- End Return Select <- do not remove

	COMMIT TRAN