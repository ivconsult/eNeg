CREATE PROCEDURE [dbo].[uspChannelUpdate]
 @ChannelID uniqueidentifier,
    @ChannelName nvarchar(100),
    @ChannelTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Channel]
	SET    [ChannelID] = @ChannelID, [ChannelName] = @ChannelName, [ChannelTypeID] = @ChannelTypeID
	WHERE  [ChannelID] = @ChannelID
	
	-- Begin Return Select <- do not remove
	SELECT [ChannelID], [ChannelName], [ChannelTypeID]
	FROM   [dbo].[Channel]
	WHERE  [ChannelID] = @ChannelID	
	-- End Return Select <- do not remove

	COMMIT TRAN