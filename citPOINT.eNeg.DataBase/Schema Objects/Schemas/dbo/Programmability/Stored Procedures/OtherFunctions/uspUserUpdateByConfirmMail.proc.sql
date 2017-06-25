CREATE PROCEDURE [dbo].[uspUserUpdateByConfirmMail]
       @OperationString  nvarchar(200),
	   @Type tinyint
AS 

/*

 ♦ All possible value for parameter @Type.

╔════════════╦══════════════════╗
╟ Type       ║ Description      ║
╠════════════╬══════════════════╣
║  # 0       │ Confirmation Mail║  
╟────────────┼──────────────────╢
║  # 1		 │ Reset Request    ║
╟────────────┼──────────────────╢
║  # 2       │ Accept be Owner  ║
╟────────────┼──────────────────╢
║  # 3       │ Refused be Owner ║
╟────────────┼──────────────────╢
║  # 4       │ Accept be owner  ║
║            │ and Orginal owner║
║            │ leave org.       ║
╚════════════╧══════════════════╝
 ♦ Member status for user in his organization

	▄ O-Owner M-Member
	▄ M (Pending Member - Member - Pending Owner) OK
	▄ MM =Member or Pending Owner
	
	-- PendingMember = 0;
    -- Member        = 1;
    -- PendingOwner  = 2;
    -- Owner         = 3;
*/
	/*User Id Of The OperationString*/
	Declare  @UserID uniqueidentifier;
	Declare  @NewEmailAddress nvarchar(300);
	Declare  @IsEmailExists nvarchar(300);
	Declare  @RequestUserID uniqueidentifier;
	Declare  @OrganizationID uniqueidentifier;
	

	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	

	/*------------------------------------------------------------------
	Getting the Value of UserID and If New Email Address exists.
	------------------------------------------------------------------*/
	
	--Select User ID
	(Select Top 1 @UserID=[UserID],
	              @NewEmailAddress=[NewEmailAddress] ,
	              @RequestUserID =[RequestUserID],
	              @OrganizationID=[OrganizationID]
	 from UserOperation
     where UserOperation.OperationString=@OperationString AND 
		   UserOperation.[Type]=@Type /*According to the operation string type*/);
		
	/*------------------------------------------------------------------
	In case confirmation mail is for accepting role owner for organization.
	------------------------------------------------------------------*/
	
	if(@Type = 2)/*eNegConstant.UserOperation = AcceptToBeOwner*/
		begin
			update [dbo].[UserOrganization] 
			set [MemberStatus] = 3 /*eNegConstant.OrganizationMemberStatus = Owner*/
			where [UserID]= @UserID AND 
			      OrganizationID= @OrganizationID;
			      
			--User can not be owner for two oragnization so if he accept to be
			-- owner for one all other requests (Pending owner) must be cancelled
			UPDATE [dbo].[UserOrganization] 
			SET [MemberStatus] = 1 /*Memeber*/
			where [UserID]= @UserID AND 
			      [MemberStatus] = 2 /*Pending Owner*/
		end
		
	/*------------------------------------------------------------------
	In case confirmation mail is for refusing role owner for organization.
	------------------------------------------------------------------*/
	
	if(@Type = 3)/*eNegConstant.UserOperation = RefuseToBeOwner*/
		begin
			update [dbo].[UserOrganization] 
			set [MemberStatus] = 1 /*eNegConstant.OrganizationMemberStatus = Member*/
			where [UserID]= @UserID AND 
			      OrganizationID= @OrganizationID;
		end

	/*------------------------------------------------------------------
	In case confirmation mail is for accepting role owner and removing original one for organization.
	------------------------------------------------------------------*/
	
	if(@Type = 4)/*eNegConstant.UserOperation = AcceptToBeOwner_RemoveOriginal*/
		begin
			update [dbo].[UserOrganization] 
			set [MemberStatus] = 3 /*eNegConstant.OrganizationMemberStatus = Owner*/
			where [UserID]= @UserID AND 
			      OrganizationID= @OrganizationID

			update [dbo].[UserOrganization]
			set [Deleted] = 1,[DeletedOn] = GetDate()
			where [UserID]= @RequestUserID AND 
			      OrganizationID= @OrganizationID
			      
			--User can not be owner for two oragnization so if he accept to be
			-- owner for one all other requests (Pending owner) must be cancelled
			UPDATE [dbo].[UserOrganization] 
			SET [MemberStatus] = 1 /*Memeber*/
			where [UserID]= @UserID AND 
			      [MemberStatus] = 2 /*Pending Owner*/
		end
	
	/*------------------------------------------------------------------
	In case it is normal confirmation mail.
	------------------------------------------------------------------*/
	if (ISNULL(@NewEmailAddress,'')='')
		begin
			--Confirm Ths User
			UPDATE [dbo].[User]
			SET     [Locked] =0
			WHERE  [UserID] = @UserID;
		end
		
	/*------------------------------------------------------------------
	In case if the user change his email.
	------------------------------------------------------------------*/
	if ((ISNULL(@NewEmailAddress,'')<>'') and 
	   (Not exists( Select * 
					from [User] 
					where UserID <> @UserID and
						EmailAddress=@NewEmailAddress and 
		                [User].Disabled=0)))
		begin

			--Confirm Ths User
			UPDATE [dbo].[User]
			SET     [Locked] =0,
					[EmailAddress]=@NewEmailAddress
			WHERE  [UserID] = @UserID;
		end
		
	/*------------------------------------------------------------------*/
		
		
	/*----------------------------------------------------------------
	- In case of Confirmation mail and reset request mail.
	----------------------------------------------------------------*/
		
	IF (@Type IN (0,1))
		BEGIN
			--Delete User Operations
			Delete [dbo].UserOperation
			Where  [dbo].UserOperation.[UserID] = @UserID AND 
				   [Type] in (0/*Confirmation*/,1/*Reset Request*/);
		END
		
		
		
    /*----------------------------------------------------------------
	- In case of Accept,refused and accept and delete owner mail .
	----------------------------------------------------------------*/
		
	IF (@Type IN (2/*Accept*/,3/*Refused*/,4/*Accept and Delete*/))
		BEGIN
			--Delete User Operations (Include in  you mind that 
			--if one is pending owner in two organization)
			Delete [dbo].UserOperation
			Where  [dbo].UserOperation.[UserID] = @UserID AND 
				   [Type] in (2/*Accept*/,3/*Refused*/,4/*Accept and Delete*/) AND
				   ( @Type in (2/*Accept*/,4/*Accept and Delete*/) OR 
				     (@Type=3/*Refused*/ and OrganizationID=@OrganizationID)
				   );
				   
		END
			
		
    -- Begin Return Select <- do not remove
    Select [UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset] ,[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
     FROM (
		SELECT 0 OrderID,[UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset] ,[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
		FROM   [dbo].[User]
		WHERE  [UserID] = @RequestUserID
	union
		SELECT 1 OrderID,[UserID], [EmailAddress], [PasswordHash], [PasswordSalt], [Locked], [LockedDate], [LastLoginDate], [CreateDate], [AccountTypeID], [IPAddress], [SecurityQuestionID], [AnswerHash], [AnswerSalt], [Online], [Disabled], [FirstName], [LastName], [CompanyName], [LCID], [Address], [City], [CountryID], [Phone], [Mobile], [Gender], [Reset] ,[CultureID],[HasPublicProfile],[ProfileDescription],[PostalCode]
		FROM   [dbo].[User]
		WHERE  [UserID] = @userID
    ) T
    order By [OrderID]
    
	-- End Return Select <- do not remove
	COMMIT TRAN
