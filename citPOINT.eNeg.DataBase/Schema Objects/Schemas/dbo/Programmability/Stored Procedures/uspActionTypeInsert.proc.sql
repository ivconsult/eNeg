CREATE PROCEDURE [dbo].[uspActionTypeInsert]
 @ActionTypeID uniqueidentifier,
    @ActionDescription nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ActionType] ([ActionTypeID], [ActionDescription])
	SELECT @ActionTypeID, @ActionDescription
	
	-- Begin Return Select <- do not remove
	SELECT [ActionTypeID], [ActionDescription]
	FROM   [dbo].[ActionType]
	WHERE  [ActionTypeID] = @ActionTypeID
	-- End Return Select <- do not remove
               
	COMMIT