CREATE PROC [dbo].[uspChannelTypeUpdate] 
    @ChannelTypeID uniqueidentifier,
    @TypeName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[ChannelType]
	SET    [ChannelTypeID] = @ChannelTypeID, [TypeName] = @TypeName
	WHERE  [ChannelTypeID] = @ChannelTypeID
	
	-- Begin Return Select <- do not remove
	SELECT [ChannelTypeID], [TypeName]
	FROM   [dbo].[ChannelType]
	WHERE  [ChannelTypeID] = @ChannelTypeID	
	-- End Return Select <- do not remove

	COMMIT TRAN
 

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
