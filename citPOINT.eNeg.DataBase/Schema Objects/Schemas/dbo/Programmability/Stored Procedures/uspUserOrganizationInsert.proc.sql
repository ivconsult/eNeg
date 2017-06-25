CREATE PROC [dbo].[uspUserOrganizationInsert] 
    @UserOrganizationID uniqueidentifier,
    @UserID uniqueidentifier,
    @OrganizationID uniqueidentifier,
    @MemberStatus tinyint,
    @Deleted bit,
    @DeletedBy uniqueidentifier,
    @DeletedOn datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
		
	-- Flag in case of one Became Owner (ALternative Owner).
	-- Leave member is Owner and thier are One Member Oly so He Will be the Owner.
	IF(@Deleted=1) 
	 Begin
		SET @UserOrganizationID=(Select UserOrganizationID 
		                         FROM UserOrganization 
		                         WHERE  Deleted=0 AND 
		                                UserID=@UserID AND 
		                                OrganizationID=@OrganizationID);

		EXEC	[dbo].[uspUserOrganizationUpdate]
				@UserOrganizationID = @UserOrganizationID,
				@UserID = @UserID,
				@OrganizationID = @OrganizationID,
				@MemberStatus = @MemberStatus,
				@Deleted = 0,
				@DeletedBy = @DeletedBy,
				@DeletedOn = @DeletedOn;
		RETURN;
	 End


	--Normal Insertion Flow 
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UserOrganization] ([UserOrganizationID], [UserID], [OrganizationID], [MemberStatus], [Deleted], [DeletedBy], [DeletedOn])
	SELECT @UserOrganizationID, @UserID, @OrganizationID, @MemberStatus, @Deleted, @DeletedBy, @DeletedOn
	
	-- Begin Return Select <- do not remove
	SELECT [UserOrganizationID], [UserID], [OrganizationID], [MemberStatus], [Deleted], [DeletedBy], [DeletedOn]
	FROM   [dbo].[UserOrganization]
	WHERE  [UserOrganizationID] = @UserOrganizationID
	-- End Return Select <- do not remove
               
	COMMIT
GO