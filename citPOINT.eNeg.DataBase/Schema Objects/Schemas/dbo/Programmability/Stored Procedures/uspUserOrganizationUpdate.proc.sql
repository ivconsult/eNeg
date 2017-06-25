CREATE PROCEDURE [dbo].[uspUserOrganizationUpdate] 
    @UserOrganizationID UNIQUEIDENTIFIER,
    @UserID             UNIQUEIDENTIFIER,
    @OrganizationID     UNIQUEIDENTIFIER,
    @MemberStatus       TINYINT,
    @Deleted            BIT,
    @DeletedBy          UNIQUEIDENTIFIER,
    @DeletedOn          DATETIME
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserOrganization]
	SET    [UserOrganizationID] = @UserOrganizationID, [UserID] = @UserID, [OrganizationID] = @OrganizationID, [MemberStatus] = @MemberStatus, [Deleted] = @Deleted, [DeletedBy] = @DeletedBy, [DeletedOn] = @DeletedOn
	WHERE  [UserOrganizationID] = @UserOrganizationID;
	
	
	/*----------------------------------------------------
	- Delete All links related to the Current user in case
	- of he was Pending owner and be Normal member. PM -> M
	----------------------------------------------------*/
	IF (@MemberStatus!=2 /*Pending owner*/)
	BEGIN
		DELETE [UserOperation]
		WHERE UserID=@UserID AND 
			  [Type] in (2/*Accept*/,3/*Refused*/,4/*Accept and Delete*/) AND 
			  OrganizationID=@OrganizationID;
	END
	/*-----------------------------------------------------*/
	
	-- Begin Return Select <- do not remove
	SELECT [UserOrganizationID], [UserID], [OrganizationID], [MemberStatus], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[UserOrganization]
	WHERE  [UserOrganizationID] = @UserOrganizationID	
	-- End Return Select <- do not remove

	COMMIT TRAN