CREATE PROCEDURE [dbo].[uspProfileUpdate]
 @ProfileID uniqueidentifier,
    @CurrentTheme nvarchar(100),
    @UserID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Profile]
	SET    [ProfileID] = @ProfileID, [CurrentTheme] = @CurrentTheme, [UserID] = @UserID
	WHERE  [ProfileID] = @ProfileID
	
	-- Begin Return Select <- do not remove
	SELECT [ProfileID], [CurrentTheme], [UserID]
	FROM   [dbo].[Profile]
	WHERE  [ProfileID] = @ProfileID	
	-- End Return Select <- do not remove

	COMMIT TRAN