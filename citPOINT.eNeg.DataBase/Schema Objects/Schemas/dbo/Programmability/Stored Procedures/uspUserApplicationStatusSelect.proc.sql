CREATE PROC [dbo].[uspUserApplicationStatusSelect] 
    @ApplicationTitle nvarchar(100),
    @UserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [UserAppStatusID], [ApplicationID], [UserID], [IsDMActive] 
	FROM   [dbo].[UserApplicationStatus] 
	WHERE  [UserID] = @UserID and [ApplicationID] = (select ApplicationID
													 from [Application]
													 where ApplicationTitle = @ApplicationTitle)
	COMMIT