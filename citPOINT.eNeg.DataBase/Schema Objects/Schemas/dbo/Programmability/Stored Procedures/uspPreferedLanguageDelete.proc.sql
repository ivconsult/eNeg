CREATE PROCEDURE [dbo].[uspPreferedLanguageDelete]
 @LCID int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[PreferedLanguage]
	WHERE  [LCID] = @LCID

	COMMIT