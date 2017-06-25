CREATE PROCEDURE [dbo].[uspRightInsert]
 @RightID uniqueidentifier,
    @RightName nvarchar(100),
    @RightDescription nvarchar(300)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Right] ([RightID], [RightName], [RightDescription])
	SELECT @RightID, @RightName, @RightDescription
	
	-- Begin Return Select <- do not remove
	SELECT [RightID], [RightName], [RightDescription]
	FROM   [dbo].[Right]
	WHERE  [RightID] = @RightID
	-- End Return Select <- do not remove
               
	COMMIT