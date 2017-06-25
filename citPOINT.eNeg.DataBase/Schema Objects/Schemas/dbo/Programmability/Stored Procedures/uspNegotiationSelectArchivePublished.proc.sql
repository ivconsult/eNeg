CREATE PROCEDURE [dbo].[uspNegotiationSelectArchivePublished]
	@NegOwner TINYINT,
    @NegStatus TINYINT,
    @UserID UNIQUEIDENTIFIER
AS 

	BEGIN TRAN

	SELECT ((YEAR(Neg.CreatedDate)*12)+Month(Neg.CreatedDate)) ArchiveID,
	         YEAR(Neg.CreatedDate) ArchiveYear,
	         Month(Neg.CreatedDate) ArchiveMonth
	FROM   [dbo].[Negotiation] as Neg
	WHERE  (Neg.Deleted = 0 and
		   Neg.NegotiationID in ( SELECT NegotiationID
								  FROM  UserOrganization, NegotiationOrganization
								  where UserOrganization.Deleted=0 AND 
								        NegotiationOrganization.Deleted=0 AND 
								        UserOrganization.MemberStatus != 0 /*Pending member*/ AND
								        UserOrganization.UserID = @UserID and 
								    	NegotiationOrganization.OrganizationID = UserOrganization.OrganizationID)
			and ((@NegStatus = 2 and Neg.StatusID='dfcea50d-18a2-4e41-9be8-1673e88101c4')or
			     (@NegStatus = 1 and Neg.StatusID='e3b0b130-133e-4c1d-957c-14c67869446c')or
			     (@NegStatus = 0))
			and ((@NegOwner = 2 and Neg.UserID != @UserID)or
			     (@NegOwner = 1 and Neg.UserID =  @UserID)or
			     (@NegOwner = 0))
			)
			
	GROUP BY  YEAR(Neg.CreatedDate),
	         Month(Neg.CreatedDate)
	         
	Order By  YEAR(Neg.CreatedDate) desc,
	         Month(Neg.CreatedDate) desc

	COMMIT
	