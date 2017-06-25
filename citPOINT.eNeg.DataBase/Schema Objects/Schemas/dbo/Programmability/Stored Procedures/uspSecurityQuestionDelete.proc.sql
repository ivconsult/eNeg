CREATE PROCEDURE [dbo].[uspSecurityQuestionDelete]
@SecurityQuestionID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[SecurityQuestion]
	WHERE  [SecurityQuestionID] = @SecurityQuestionID

	COMMIT