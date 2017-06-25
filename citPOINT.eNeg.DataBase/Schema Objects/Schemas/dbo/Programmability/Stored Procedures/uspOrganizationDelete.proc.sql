CREATE PROC [dbo].[uspOrganizationDelete] 
    @OrganizationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Organization]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [OrganizationID] = @OrganizationID;

 

	COMMIT
GO