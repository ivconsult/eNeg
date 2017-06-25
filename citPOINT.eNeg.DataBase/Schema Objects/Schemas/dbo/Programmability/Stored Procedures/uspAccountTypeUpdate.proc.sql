CREATE PROCEDURE [dbo].[uspAccountTypeUpdate]
  @AccountTypeID uniqueidentifier,
    @TypeName nvarchar(100),
    @TypeDescription nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[AccountType]
	SET    [AccountTypeID] = @AccountTypeID, [TypeName] = @TypeName, [TypeDescription] = @TypeDescription
	WHERE  [AccountTypeID] = @AccountTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [AccountTypeID], [TypeName], [TypeDescription]
	FROM   [dbo].[AccountType]
	WHERE  [AccountTypeID] = @AccountTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN