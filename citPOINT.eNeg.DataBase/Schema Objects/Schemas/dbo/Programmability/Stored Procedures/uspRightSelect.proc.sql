CREATE PROCEDURE [dbo].[uspRightSelect]
   @RightID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [RightID], [RightName], [RightDescription] 
	FROM   [dbo].[Right] 
	WHERE  ([RightID] = @RightID OR @RightID IS NULL) 

	COMMIT