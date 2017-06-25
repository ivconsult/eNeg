CREATE PROCEDURE [dbo].[uspSecurityQuestionUpdate]
 @SecurityQuestionID uniqueidentifier,
    @Question nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[SecurityQuestion]
	SET    [SecurityQuestionID] = @SecurityQuestionID, [Question] = @Question
	WHERE  [SecurityQuestionID] = @SecurityQuestionID
	
	-- Begin Return Select <- do not remove
	SELECT [SecurityQuestionID], [Question]
	FROM   [dbo].[SecurityQuestion]
	WHERE  [SecurityQuestionID] = @SecurityQuestionID	
	-- End Return Select <- do not remove

	COMMIT TRAN