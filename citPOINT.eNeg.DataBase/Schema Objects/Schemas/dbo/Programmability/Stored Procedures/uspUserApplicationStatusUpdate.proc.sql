CREATE PROC [dbo].[uspUserApplicationStatusUpdate] 
    @ApplicationTitle nvarchar(100),
    @UserID uniqueidentifier,
    @IsDMActive bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserApplicationStatus]
	SET    [IsDMActive] = @IsDMActive
	WHERE  [UserID] = @UserID and [ApplicationID] = (select ApplicationID
													 from [Application]
													 where ApplicationTitle = @ApplicationTitle)
	
	-- Begin Return Select <- do not remove
	SELECT [UserAppStatusID],[ApplicationID],[UserID],[IsDMActive]  
	FROM   [dbo].[UserApplicationStatus]
	WHERE  [UserID] = @UserID and [ApplicationID] = (select ApplicationID
													 from [Application]
													 where ApplicationTitle = @ApplicationTitle)
	-- End Return Select <- do not remove
	COMMIT TRAN