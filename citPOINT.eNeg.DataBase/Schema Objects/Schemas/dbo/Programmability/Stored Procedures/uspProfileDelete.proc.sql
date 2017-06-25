CREATE PROCEDURE [dbo].[uspProfileDelete]
 @ProfileID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Profile]
	WHERE  [ProfileID] = @ProfileID

	COMMIT