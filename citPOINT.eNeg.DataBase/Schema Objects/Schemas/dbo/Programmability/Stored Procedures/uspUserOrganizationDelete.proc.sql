CREATE PROC [dbo].[uspUserOrganizationDelete] 
    @UserOrganizationID uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	DECLARE @OrganizationID uniqueidentifier
	DECLARE @UserID uniqueidentifier
	
	
	BEGIN TRAN
	
	(SELECT TOP 1 @OrganizationID=OrganizationID,
				  @UserID=UserID
	 FROM UserOrganization
	 WHERE UserOrganizationID=@UserOrganizationID and Deleted=0);
	 
	

	UPDATE  [dbo].[UserOrganization]
	SET 
		Deleted=1,
		DeletedOn=GETDATE()
	WHERE  [UserOrganizationID] = @UserOrganizationID;
 
  
    /*----------------------------------------------------
	- Delete All links related to the Current user in case
	- of he was Pending owner and then deleted by another owner
	----------------------------------------------------*/
	
	DELETE [UserOperation]
	WHERE [UserID]=@UserID AND 
			  [OrganizationID]=@OrganizationID AND 
			  [Type] in (2/*Accept*/,3/*Refused*/,4/*Accept and Delete*/);
	
	
	COMMIT
