CREATE PROCEDURE [dbo].[uspChannelDelete]
  @ChannelID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Channel]
	WHERE  [ChannelID] = @ChannelID

	COMMIT