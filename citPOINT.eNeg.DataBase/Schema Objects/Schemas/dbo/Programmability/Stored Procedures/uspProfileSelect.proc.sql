CREATE PROCEDURE [dbo].[uspProfileSelect]
 @ProfileID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ProfileID], [CurrentTheme], [UserID] 
	FROM   [dbo].[Profile] 
	WHERE  ([ProfileID] = @ProfileID OR @ProfileID IS NULL) 

	COMMIT