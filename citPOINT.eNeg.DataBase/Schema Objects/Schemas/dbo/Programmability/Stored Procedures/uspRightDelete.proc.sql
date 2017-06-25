CREATE PROCEDURE [dbo].[uspRightDelete]
   @RightID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Right]
	WHERE  [RightID] = @RightID

	COMMIT