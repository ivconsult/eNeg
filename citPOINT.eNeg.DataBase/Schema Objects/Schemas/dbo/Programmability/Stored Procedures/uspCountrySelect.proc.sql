CREATE PROCEDURE [dbo].[uspCountrySelect]
 @CountryID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [CountryID], [CountryName] 
	FROM   [dbo].[Country] 
	WHERE  ([CountryID] = @CountryID OR @CountryID IS NULL) 

	COMMIT