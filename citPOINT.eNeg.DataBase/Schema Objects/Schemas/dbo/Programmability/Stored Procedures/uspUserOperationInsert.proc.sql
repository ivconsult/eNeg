CREATE PROC [dbo].[uspUserOperationInsert] 
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
	Delete [dbo].[UserOperation] 
	WHERE UserID=@UserID;
	
	INSERT INTO [dbo].[UserOperation] ([OperationID], [UserID], [OperationString], [Type],[NewEmailAddress],[RequestUserID],[OrganizationID])
	SELECT @OperationID, @UserID, @OperationString, @Type,@NewEmailAddress,@RequestUserID,@OrganizationID
	
	-- Begin Return Select <- do not remove
	SELECT [OperationID], [UserID], [OperationString], [Type],[NewEmailAddress],[RequestUserID],[OrganizationID]
	FROM   [dbo].[UserOperation]
	WHERE  [OperationID] = @OperationID
	-- End Return Select <- do not remove
               
	COMMIT
 