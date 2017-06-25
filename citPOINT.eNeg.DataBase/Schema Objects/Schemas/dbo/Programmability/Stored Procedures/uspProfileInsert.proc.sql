CREATE PROCEDURE [dbo].[uspProfileInsert]
 @ProfileID uniqueidentifier,
    @CurrentTheme nvarchar(100),
    @UserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Profile] ([ProfileID], [CurrentTheme], [UserID])
	SELECT @ProfileID, @CurrentTheme, @UserID
	
	-- Begin Return Select <- do not remove
	SELECT [ProfileID], [CurrentTheme], [UserID]
	FROM   [dbo].[Profile]
	WHERE  [ProfileID] = @ProfileID
	-- End Return Select <- do not remove
               
	COMMIT