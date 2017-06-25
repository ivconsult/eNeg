CREATE PROC [dbo].[uspChannelTypeInsert] 
    @ChannelTypeID uniqueidentifier,
    @TypeName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[ChannelType] ([ChannelTypeID], [TypeName])
	SELECT @ChannelTypeID, @TypeName
	
	-- Begin Return Select <- do not remove
	SELECT [ChannelTypeID], [TypeName]
	FROM   [dbo].[ChannelType]
	WHERE  [ChannelTypeID] = @ChannelTypeID
	-- End Return Select <- do not remove
               
	COMMIT