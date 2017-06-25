CREATE PROCEDURE [dbo].[uspRightUpdate]
 @RightID uniqueidentifier,
    @RightName nvarchar(100),
    @RightDescription nvarchar(300)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Right]
	SET    [RightID] = @RightID, [RightName] = @RightName, [RightDescription] = @RightDescription
	WHERE  [RightID] = @RightID
	
	-- Begin Return Select <- do not remove
	SELECT [RightID], [RightName], [RightDescription]
	FROM   [dbo].[Right]
	WHERE  [RightID] = @RightID	
	-- End Return Select <- do not remove

	COMMIT TRAN