CREATE PROCEDURE [dbo].[uspNegotiationSelectClosed]
 @ArchiveYear int,
 @ArchiveMonth int,
 @UserID UNIQUEIDENTIFIER
AS 

	BEGIN TRAN
  
	SELECT  [NegotiationID], [NegotiationName], [CreatedDate], [StatusID], [Deleted], [DeletedDate], [UserID] 
	FROM   [dbo].[Negotiation] 
	WHERE  Deleted = 0 AND
	       StatusID='dfcea50d-18a2-4e41-9be8-1673e88101c4' /*Closed*/ AND
	       UserID  = @UserID AND
	       (YEAR(CreatedDate)=@ArchiveYear) AND
		   (Month(CreatedDate)=@ArchiveMonth)
	order by NegotiationName

	COMMIT TRAN