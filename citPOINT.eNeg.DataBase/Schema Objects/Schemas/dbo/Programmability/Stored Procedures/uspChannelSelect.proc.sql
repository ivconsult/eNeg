CREATE PROCEDURE [dbo].[uspChannelSelect]
  @ChannelID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ChannelID], [ChannelName], [ChannelTypeID] 
	FROM   [dbo].[Channel] 
	WHERE  ([ChannelID] = @ChannelID OR @ChannelID IS NULL) 

	COMMIT