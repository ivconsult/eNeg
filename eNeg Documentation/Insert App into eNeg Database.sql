
INSERT INTO [dbo].[Application]
     (
	  [ApplicationID],
	  [ApplicationRank],
	  [ApplicationTitle],
	  [ApplicationBaseAddress],
	  [ApplicationMainServicePath],
	  [AssemblyName],
	  [ModuleName],
	  [XapFilePath]
	 )
SELECT  '1276b329-6e41-4b5d-88d5-cebdb027c602' [ApplicationID],
		8                                      [ApplicationRank],
		N'Test App'                            [ApplicationTitle],
		N'http://localhost:7000/'              [ApplicationBaseAddress],
		N'I have not Services in this Examlpe' [ApplicationMainServicePath],
		N'TestApp,TestAppModule'               [AssemblyName],
		N'TestAppModule'                       [ModuleName],
		N'http://localhost:7000/ClientBin/TestApp.xap' [XapFilePath]

Go


 INSERT
 INTO   [NegotiationApplicationStatus]
        (
               [NegotiationApplicationStatusID] ,
               [ApplicationID]                  ,
               [NegotiationID]                  ,
               [UserID]                         ,
               [Active]
        )
 SELECT NEWID() NegotiationApplicationStatusID,
        [Application].ApplicationID           ,
        Negotiation.NegotiationID             ,
        Negotiation.UserID                    ,
        0 Active
 FROM   Negotiation,
        [Application]
 WHERE  CAST(Negotiation.NegotiationID AS nvarchar(200))+CAST([Application].ApplicationID AS nvarchar(200)) NOT IN
        (SELECT CAST(NegotiationID AS nvarchar(200))    +CAST(ApplicationID AS nvarchar(200))
        FROM    [NegotiationApplicationStatus]
        )
        
GO

