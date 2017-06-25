CREATE PROCEDURE [dbo].[uspUserProfileStatisticals]
    @UserID uniqueidentifier
AS 

	declare @NegotiationCount decimal;
	declare @ConversationCount decimal;
	declare @AppsAvg decimal;
	
	
	declare @ActiveAppsCount decimal;
	declare @AllAppsCount decimal;
		
	/*-----------------------------------------------------------------	
	 - Negotiation Count
	-----------------------------------------------------------------*/
		
	SET @NegotiationCount=ISNULL((SELECT COUNT(*)
								  FROM    Negotiation
								  WHERE   UserID  =@UserID AND     
								          Deleted =0),0);
								          
								          
	/*-----------------------------------------------------------------	
	 - Conversation Count
	-----------------------------------------------------------------*/
		
	SET @ConversationCount=ISNULL((SELECT COUNT(*)
	 							   FROM    [Conversation]
								   WHERE   DeletedBy=@UserID AND     
								           Deleted =0),0);
								      
	/*-----------------------------------------------------------------	
	 - Active Apps Avg Count
	-----------------------------------------------------------------*/
	
	SET @ActiveAppsCount =ISNULL((SELECT COUNT(*) 
								  FROM   Negotiation
								        INNER JOIN NegotiationApplicationStatus
								        ON Negotiation.NegotiationID = NegotiationApplicationStatus.NegotiationID
						          WHERE  Negotiation.Deleted = 0 AND 
									     NegotiationApplicationStatus.Active = 1 AND
										 Negotiation.UserID=@UserID),0);							

	SET @AppsAvg=0;

	IF(@NegotiationCount>0)
	   BEGIN
		SET @AppsAvg=(@ActiveAppsCount/@NegotiationCount);
	   END
									

	SELECT 'Negotiation'  StatisticalName,@NegotiationCount  StatisticalValue Union all
	SELECT 'Conversation' StatisticalName,@ConversationCount StatisticalValue Union all
	SELECT 'Apps.Avg'     StatisticalName,@AppsAvg           StatisticalValue 