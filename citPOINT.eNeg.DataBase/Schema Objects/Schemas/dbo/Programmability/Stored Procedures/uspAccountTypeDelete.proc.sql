CREATE PROCEDURE [dbo].[uspAccountTypeDelete]
	 @AccountTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[AccountType]
	WHERE  [AccountTypeID] = @AccountTypeID

	COMMIT