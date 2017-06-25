CREATE PROCEDURE [dbo].[uspPreferedLanguageSelect]
  @LCID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [LCID], [PreferedLanguage] 
	FROM   [dbo].[PreferedLanguage] 
	WHERE  ([LCID] = @LCID OR @LCID IS NULL) 

	COMMIT