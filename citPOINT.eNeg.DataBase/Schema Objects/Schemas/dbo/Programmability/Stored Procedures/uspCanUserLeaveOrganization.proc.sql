CREATE PROCEDURE [dbo].[uspCanUserLeaveOrganization]
    @UserID uniqueidentifier,
    @OrganizationID uniqueidentifier
AS 
/*

╔════════════╦═══════════════╦═══════════════╦════════╗
╟ Case #     ║ Member Status ║ Other Member  ║ Leave  ║
╠════════════╬═══════════════╬═══════════════╬════════╣
║  # 1       │ Member        │ any case      │ OK     ║  
╟────────────┼───────────────┼───────────────┼────────╢
║  # 2		 │ Owner         │ 1..n Owners   │ OK     ║
╟────────────┼───────────────┼───────────────┼────────╢
║  # 3       │ Owner         │ 0 Owners 1 MM │ OK     ║
╟────────────┼───────────────┼───────────────┼────────╢
║  # 4       │ Owner         │ 0 Owners 1 M  │        ║
║            │               │But he is Owner│  Not   ║
║            │               │in other Org.  │  OK    ║
╟────────────┼───────────────┼───────────────┼────────╢
║  # 5       │ Owner         │ 0 Owners n M  │ Not OK ║
╟────────────┼───────────────┼───────────────┼────────╢
║  # 6       │ Owner         │ 0 Owners 0 M  │     OK ║
║            │               │ Delete Org.   │        ║
╚════════════╧═══════════════╧═══════════════╧════════╝
 
	▄ O-Owner M-Member
	▄ M (Pending Member - Member - Pending Owner) OK
	▄ MM =Member or Pending Owner
	
	-- PendingMember = 0;
    -- Member        = 1;
    -- PendingOwner  = 2;
    -- Owner         = 3;
*/
	
	declare @MemberStatus int;
	declare @OwnersCount int;
	declare @MembersCount int;
	Declare @AlternativeOwnerID uniqueidentifier;
		
	-- If he is a member then Leave Ok 
	-- Get user Role or Status in Organization e.g. Member or Owner.
	
		Set @MemberStatus=(Select MemberStatus from UserOrganization
		  WHERE Deleted=0 AND
				UserID=@UserID AND
				OrganizationID=@OrganizationID);
		    
		SET @MemberStatus=ISNULL(@MemberStatus,0);
    
	
	/*-----------------------------------------------------------------------------
	- Case # 1  if current user is not Owner then he can Leave without Condition 
	-----------------------------------------------------------------------------*/

		if (@MemberStatus!=3 /*Owner*/) 
			Begin
			   Select Cast(1 as bit) CanLeave,
					  Cast(0 as int) OwnersCount,
					  cast(NULL as uniqueidentifier) AlternativeOwnerID,
					  cast(NULL as nvarchar(300)) EmailAddress,
					  cast(NULL as nvarchar(150)) FirstName,
					  cast(NULL as nvarchar(150)) LastName;
			   Return;
			END;

	/*-----------------------------------------------------------------------------
	- Case # 2 Check if thier is another Owner
	-----------------------------------------------------------------------------*/
		
		SET @OwnersCount=(SELECT COUNT(*) From UserOrganization
						  WHERE Deleted=0 AND
							    UserID!=@UserID AND
								OrganizationID=@OrganizationID AND
								MemberStatus=3 /*Owner*/);


		IF (@OwnersCount>0) 
			Begin
			  -- Case # 2
			   Select CAST(1 as bit) CanLeave,
					  0 OwnersCount,
					  NULL AlternativeOwnerID,
					  NULL EmailAddress,
					  NULL FirstName,
					  NULL LastName;
			   Return;
			END;
		  

	/*-----------------------------------------------------------------------------
	-  Check if thier is any other member than 1 or 
	-----------------------------------------------------------------------------*/

		SET @MembersCount=(Select COUNT(*) From UserOrganization
						   WHERE Deleted=0 AND
						         UserID!=@UserID AND
								 OrganizationID=@OrganizationID);


	   

		IF (@MembersCount>1) 
			Begin
			  -- Case # 5
			   Select CAST(0 as bit) CanLeave,
					  0 OwnersCount,
					  NULL AlternativeOwnerID,
					  NULL EmailAddress,
					  NULL FirstName,
					  NULL LastName;
			   Return;
			END;
			
		--Case # 6	
		IF (@MembersCount=0) /*He is the only member in the Organization*/
		Begin
		   --Case # 6
		   Select CAST(1 as bit) CanLeave,
				  0 OwnersCount,
				  @UserID AlternativeOwnerID,
				  NULL EmailAddress,
				  NULL FirstName,
				  NULL LastName;
		   Return;
		END;
	
	/*-----------------------------------------------------------------------------
	- Check if thier is a member and only One member	 
	-----------------------------------------------------------------------------*/

		  SET @AlternativeOwnerID=(Select UserID From UserOrganization
        					   WHERE Deleted=0 AND
 						             UserID!=@UserID AND
								     OrganizationID=@OrganizationID AND
								     MemberStatus !=0 /*PendingMember*/);

	    -- case  # 5
		IF(@AlternativeOwnerID IS NULL)
			Begin
			   Select CAST(0 as bit) CanLeave,
					  0 OwnersCount,
					  NULL AlternativeOwnerID,
					  NULL EmailAddress,
					  NULL FirstName,
					  NULL LastName;
			   Return;
			END;
			
	    
		IF(@AlternativeOwnerID IS NOT NULL)
		    Begin
		
			SET @OwnersCount=(SELECT COUNT(*) From UserOrganization
						  WHERE Deleted=0 AND
							    UserID=@AlternativeOwnerID AND
								OrganizationID!=@OrganizationID AND
								MemberStatus =3 /*Owner*/);
			
			--So he is owner in other organization so he can not play this role in this organization also.
		    IF(@OwnersCount>0) 
			  Begin
			    --Case # 4
			    Select CAST(0 as bit) CanLeave,
					  0 OwnersCount,
					  NULL AlternativeOwnerID,
					  NULL EmailAddress,
					  NULL FirstName,
					  NULL LastName;
				return;
			  END

	        --case # 3
			SELECT CAST(1 as bit) CanLeave,
					1 OwnersCount,
					[User].UserID AlternativeOwnerID,
					[User].EmailAddress,
					[User].FirstName,
					[User].LastName	
			FROM [User]
			WHERE UserID=@AlternativeOwnerID;
			   
			Return;

			END;