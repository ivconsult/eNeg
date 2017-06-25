CREATE PROC [dbo].[uspApplicationUpdate] 
    @ApplicationID uniqueidentifier,
    @ApplicationRank int,
    @ApplicationTitle nvarchar(100),
    @ApplicationBaseAddress nvarchar(200),
    @ApplicationMainServicePath nvarchar(255),
    @AssemblyName nvarchar(150),
    @ModuleName nvarchar(100),
    @XapFilePath nvarchar(255)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Application]
	SET    [ApplicationID] = @ApplicationID, [ApplicationRank] = @ApplicationRank, [ApplicationTitle] = @ApplicationTitle, [ApplicationBaseAddress] = @ApplicationBaseAddress, [ApplicationMainServicePath] = @ApplicationMainServicePath, [AssemblyName] = @AssemblyName, [ModuleName] = @ModuleName, [XapFilePath] = @XapFilePath
	WHERE  [ApplicationID] = @ApplicationID
	
	-- Begin Return Select <- do not remove
	SELECT [ApplicationID], [ApplicationRank], [ApplicationTitle], [ApplicationBaseAddress], [ApplicationMainServicePath], [AssemblyName], [ModuleName], [XapFilePath]
	FROM   [dbo].[Application]
	WHERE  [ApplicationID] = @ApplicationID	
	-- End Return Select <- do not remove

	COMMIT TRAN