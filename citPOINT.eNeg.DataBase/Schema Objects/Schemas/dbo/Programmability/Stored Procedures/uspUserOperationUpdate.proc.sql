CREATE PROC [dbo].[uspUserOperationUpdate] 
    @OperationID uniqueidentifier,
    @UserID uniqueidentifier,
    @OperationString  nvarchar(200),
    @Type tinyint,
	@NewEmailAddress nvarchar(300),
	@RequestUserID uniqueidentifier,
	@OrganizationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserOperation]
	SET    [OperationID] = @OperationID, [UserID] = @UserID, [OperationString] = @OperationString, [Type] = @Type,[NewEmailAddress]=@NewEmailAddress,[RequestUserID] =@RequestUserID,[OrganizationID]=@OrganizationID
	WHERE  [OperationID] = @OperationID
	
	-- Begin Return Select <- do not remove
	SELECT [OperationID], [UserID], [OperationString], [Type],[NewEmailAddress],[RequestUserID],[OrganizationID]
	FROM   [dbo].[UserOperation]
	WHERE  [OperationID] = @OperationID	
	-- End Return Select <- do not remove

	COMMIT TRAN
