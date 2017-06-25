CREATE PROCEDURE [dbo].[uspSecurityQuestionSelect]
@SecurityQuestionID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [SecurityQuestionID], [Question] 
	FROM   [dbo].[SecurityQuestion] 
	WHERE  ([SecurityQuestionID] = @SecurityQuestionID OR @SecurityQuestionID IS NULL) 

	COMMIT