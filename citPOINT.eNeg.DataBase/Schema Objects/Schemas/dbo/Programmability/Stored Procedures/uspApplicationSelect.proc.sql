CREATE PROC [dbo].[uspApplicationSelect] 
    @ApplicationID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ApplicationID], [ApplicationRank], [ApplicationTitle], [ApplicationBaseAddress], [ApplicationMainServicePath], [AssemblyName], [ModuleName], [XapFilePath] 
	FROM   [dbo].[Application] 
	WHERE  ([ApplicationID] = @ApplicationID OR @ApplicationID IS NULL) 

	COMMIT