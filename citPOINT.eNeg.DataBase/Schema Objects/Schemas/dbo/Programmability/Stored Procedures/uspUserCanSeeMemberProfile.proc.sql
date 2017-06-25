CREATE PROCEDURE [dbo].[uspUserCanSeeMemberProfile] 
	@CurrentUserID UNIQUEIDENTIFIER, 
	@MemberUserID  UNIQUEIDENTIFIER 
AS 

	/*

	╔════════════╦═══════════════╦════════════════════════╦════════╗
	╟ Case #     ║Current Member ║ Member Status Profile  ║ See    ║
	║            ║     Status    ║                        ║        ║
	╠════════════╬═══════════════╬════════════════════════╬════════╣
	║  # 1       │ Owner         │ Member (Same.org.)     │ OK     ║  
	╟────────────┼───────────────┼────────────────────────┼────────╢
	║  # 2		 │ -             │ Public Profile         │ OK     ║
	╟────────────┼───────────────┼────────────────────────┼────────╢
	║  # 3       │ Owner         │ Member (Not Same.org.) │ Not OK ║
	╟────────────┼───────────────┼────────────────────────┼────────╢
	║  # 4       │ Member        │ - Same.org.            │ Not OK ║
	╚════════════╧═══════════════╧════════════════════════╧════════╝
	
	*/
  DECLARE @IsMemberProfilePublic BIT; 
  DECLARE @OrganizationOwnedID UNIQUEIDENTIFIER; 
  DECLARE @SharedCount INT; 

  SET @IsMemberProfilePublic=Isnull((SELECT [User].haspublicprofile 
                                     FROM   [User] 
                                     WHERE  userid = @MemberUserID), 0); 

  IF( @IsMemberProfilePublic = 1 ) 
    BEGIN 
        SELECT CAST(1 AS BIT) result; 

        RETURN; 
    END 

  --Get The Oragization which current user owned 
  SET @OrganizationOwnedID=(SELECT TOP 1 userorganization.organizationid 
                            FROM   userorganization 
                            WHERE  userorganization.userid = @CurrentUserID 
                                   AND memberstatus = 3 /*Owner*/ 
                                   AND deleted = 0) 

  --he is not owner 
  IF( @OrganizationOwnedID IS NULL ) 
    BEGIN 
        SELECT CAST(0 AS BIT) result; 
        RETURN; 
    END 

  SET @SharedCount=(SELECT COUNT(*) 
                    FROM   userorganization 
                    WHERE  userorganization.userid = @MemberUserID 
                           AND organizationid = @OrganizationOwnedID 
                           AND deleted = 0) 


 IF( @SharedCount >0 )  --they are members in the same org.
    BEGIN 
        SELECT CAST(1 AS BIT) result; 

        RETURN; 
    END 
    
    
  SELECT CAST(0 AS BIT) result; 