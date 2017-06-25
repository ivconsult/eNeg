CREATE PROCEDURE [dbo].[uspChannelInsert]
  @ChannelID uniqueidentifier,
    @ChannelName nvarchar(100),
    @ChannelTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Channel] ([ChannelID], [ChannelName], [ChannelTypeID])
	SELECT @ChannelID, @ChannelName, @ChannelTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [ChannelID], [ChannelName], [ChannelTypeID]
	FROM   [dbo].[Channel]
	WHERE  [ChannelID] = @ChannelID
	-- End Return Select <- do not remove
               
	COMMIT