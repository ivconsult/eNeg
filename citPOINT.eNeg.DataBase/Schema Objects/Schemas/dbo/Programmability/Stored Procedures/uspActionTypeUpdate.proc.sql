CREATE PROCEDURE [dbo].[uspActionTypeUpdate]
 @ActionTypeID uniqueidentifier,
    @ActionDescription nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ActionType]
	SET    [ActionTypeID] = @ActionTypeID, [ActionDescription] = @ActionDescription
	WHERE  [ActionTypeID] = @ActionTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [ActionTypeID], [ActionDescription]
	FROM   [dbo].[ActionType]
	WHERE  [ActionTypeID] = @ActionTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN