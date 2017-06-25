CREATE PROCEDURE [dbo].[uspCountryInsert]
 @CountryID uniqueidentifier,
    @CountryName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Country] ([CountryID], [CountryName])
	SELECT @CountryID, @CountryName
	
	-- Begin Return Select <- do not remove
	SELECT [CountryID], [CountryName]
	FROM   [dbo].[Country]
	WHERE  [CountryID] = @CountryID
	-- End Return Select <- do not remove
               
	COMMIT