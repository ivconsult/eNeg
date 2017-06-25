CREATE PROCEDURE [dbo].[uspCountryDelete]
@CountryID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Country]
	WHERE  [CountryID] = @CountryID

	COMMIT