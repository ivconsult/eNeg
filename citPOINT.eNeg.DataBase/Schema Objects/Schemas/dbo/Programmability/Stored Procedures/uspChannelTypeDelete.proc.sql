CREATE PROC [dbo].[uspChannelTypeDelete] 
    @ChannelTypeID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[ChannelType]
	WHERE  [ChannelTypeID] = @ChannelTypeID

	COMMIT