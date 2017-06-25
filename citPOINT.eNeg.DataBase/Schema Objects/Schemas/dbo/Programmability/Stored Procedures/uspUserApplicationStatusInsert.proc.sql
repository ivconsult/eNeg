CREATE PROC [dbo].[uspUserApplicationStatusInsert] 
    @UserAppStatusID uniqueidentifier,
    @ApplicationID uniqueidentifier,
    @UserID uniqueidentifier,
    @IsDMActive bit
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UserApplicationStatus] ([UserAppStatusID], [ApplicationID], [UserID], [IsDMActive])
	SELECT @UserAppStatusID, @ApplicationID, @UserID, @IsDMActive
	
	-- Begin Return Select <- do not remove
	SELECT [UserAppStatusID], [ApplicationID], [UserID], [IsDMActive]
	FROM   [dbo].[UserApplicationStatus]
	WHERE  [UserAppStatusID] = @UserAppStatusID
	-- End Return Select <- do not remove
               
	COMMIT