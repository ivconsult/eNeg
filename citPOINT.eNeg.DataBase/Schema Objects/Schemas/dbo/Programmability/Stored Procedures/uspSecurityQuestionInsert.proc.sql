CREATE PROCEDURE [dbo].[uspSecurityQuestionInsert]
  @SecurityQuestionID uniqueidentifier,
    @Question nvarchar(200)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[SecurityQuestion] ([SecurityQuestionID], [Question])
	SELECT @SecurityQuestionID, @Question
	
	-- Begin Return Select <- do not remove
	SELECT [SecurityQuestionID], [Question]
	FROM   [dbo].[SecurityQuestion]
	WHERE  [SecurityQuestionID] = @SecurityQuestionID
	-- End Return Select <- do not remove
               
	COMMIT