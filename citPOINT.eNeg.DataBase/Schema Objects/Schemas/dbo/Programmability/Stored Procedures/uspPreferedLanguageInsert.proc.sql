CREATE PROCEDURE [dbo].[uspPreferedLanguageInsert]
 @LCID int,
    @PreferedLanguage nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[PreferedLanguage] ([LCID], [PreferedLanguage])
	SELECT @LCID, @PreferedLanguage
	
	-- Begin Return Select <- do not remove
	SELECT [LCID], [PreferedLanguage]
	FROM   [dbo].[PreferedLanguage]
	WHERE  [LCID] = @LCID
	-- End Return Select <- do not remove
               
	COMMIT