CREATE PROCEDURE [dbo].[uspCountryUpdate]
 @CountryID uniqueidentifier,
    @CountryName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Country]
	SET    [CountryID] = @CountryID, [CountryName] = @CountryName
	WHERE  [CountryID] = @CountryID
	
	-- Begin Return Select <- do not remove
	SELECT [CountryID], [CountryName]
	FROM   [dbo].[Country]
	WHERE  [CountryID] = @CountryID	
	-- End Return Select <- do not remove

	COMMIT TRAN