Create PROCEDURE [dbo].[uspNegotiationSelectArchiveClosed]
    @UserID UNIQUEIDENTIFIER
AS 

	BEGIN TRAN

	SELECT ((YEAR(CreatedDate)*12)+Month(CreatedDate)) ArchiveID,
	         YEAR(CreatedDate) ArchiveYear,
	         Month(CreatedDate) ArchiveMonth
	FROM   [dbo].[Negotiation]
	WHERE  Deleted = 0 AND
	       StatusID='dfcea50d-18a2-4e41-9be8-1673e88101c4' AND
	       UserID = @UserID 
			
	GROUP BY  YEAR(CreatedDate),
	         Month(CreatedDate)
	         
	Order By  YEAR(CreatedDate) desc,
	         Month(CreatedDate) desc

	COMMIT	