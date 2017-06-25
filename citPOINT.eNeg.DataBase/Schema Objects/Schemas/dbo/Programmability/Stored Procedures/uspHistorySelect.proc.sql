CREATE PROCEDURE [dbo].[uspHistorySelect]
 @HistoryID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [HistoryID], [OldValue], [NewValue], [TableName], [ActionTypeID], [ActionDate], [UserID] 
	FROM   [dbo].[History] 
	WHERE  ([HistoryID] = @HistoryID OR @HistoryID IS NULL) 

	COMMIT