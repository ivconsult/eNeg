CREATE PROCEDURE [dbo].[uspAccountTypeInsert]
	@AccountTypeID uniqueidentifier,
    @TypeName nvarchar(100),
    @TypeDescription nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[AccountType] ([AccountTypeID], [TypeName], [TypeDescription])
	SELECT @AccountTypeID, @TypeName, @TypeDescription
	
	-- Begin Return Select <- do not remove
	SELECT [AccountTypeID], [TypeName], [TypeDescription]
	FROM   [dbo].[AccountType]
	WHERE  [AccountTypeID] = @AccountTypeID
	-- End Return Select <- do not remove
               
	COMMIT