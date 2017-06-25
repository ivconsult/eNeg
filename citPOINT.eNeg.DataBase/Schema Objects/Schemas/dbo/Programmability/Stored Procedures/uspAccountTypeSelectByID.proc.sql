CREATE PROCEDURE [dbo].[uspAccountTypeSelect]
	 @AccountTypeID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [AccountTypeID], [TypeName], [TypeDescription] 
	FROM   [dbo].[AccountType] 
	WHERE  ([AccountTypeID] = @AccountTypeID OR @AccountTypeID IS NULL) 

	COMMIT
