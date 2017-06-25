CREATE PROCEDURE [dbo].[uspPreferedLanguageUpdate]
 @LCID int,
    @PreferedLanguage nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[PreferedLanguage]
	SET    [LCID] = @LCID, [PreferedLanguage] = @PreferedLanguage
	WHERE  [LCID] = @LCID
	
	-- Begin Return Select <- do not remove
	SELECT [LCID], [PreferedLanguage]
	FROM   [dbo].[PreferedLanguage]
	WHERE  [LCID] = @LCID	
	-- End Return Select <- do not remove

	COMMIT TRAN